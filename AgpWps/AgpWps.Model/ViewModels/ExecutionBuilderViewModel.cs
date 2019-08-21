using AgpWps.Model.Enums;
using AgpWps.Model.Exceptions;
using AgpWps.Model.Factories;
using AgpWps.Model.Messages;
using AgpWps.Model.Repositories;
using AgpWps.Model.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Wps.Client.Models.Execution;
using Wps.Client.Services;

namespace AgpWps.Model.ViewModels
{
    public class ExecutionBuilderViewModel : ViewModelBase
    {

        private string _title;
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private ICommand _windowsLoadedCommand;
        public ICommand WindowLoadedCommand
        {
            get => _windowsLoadedCommand;
            set => Set(ref _windowsLoadedCommand, value);
        }

        private RelayCommand _executeCommand;
        public RelayCommand ExecuteCommand
        {
            get => _executeCommand;
            set => Set(ref _executeCommand, value);
        }

        private bool _isExecutingProcess;
        public bool IsExecutingProcess
        {
            get => _isExecutingProcess;
            set
            {
                Set(ref _isExecutingProcess, value);
                ExecuteCommand.RaiseCanExecuteChanged();
            }
        }

        private ObservableCollection<DataInputViewModel> _dataInputViewModels = new ObservableCollection<DataInputViewModel>();
        public ObservableCollection<DataInputViewModel> DataInputViewModels
        {
            get => _dataInputViewModels;
            set => Set(ref _dataInputViewModels, value);
        }

        private ObservableCollection<DataOutputViewModel> _dataOutputViewModels = new ObservableCollection<DataOutputViewModel>();
        public ObservableCollection<DataOutputViewModel> DataOutputViewModels
        {
            get => _dataOutputViewModels;
            set => Set(ref _dataOutputViewModels, value);
        }

        private readonly string _wpsUri;
        private readonly string _processId;
        private readonly IWpsClient _wpsClient;
        private readonly IContext _context;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IRequestFactory _requestFactory;
        private readonly IDialogService _dialogService;
        private readonly ILoggerRepository _loggerRepository;

        public ExecutionBuilderViewModel(string wpsUri,
            string processId,
            IWpsClient wpsClient,
            IContext context,
            IViewModelFactory viewModelFactory,
            IRequestFactory requestFactory,
            IDialogService dialogService,
            ILoggerRepository loggerRepository)
        {
            _wpsUri = wpsUri ?? throw new ArgumentNullException(nameof(wpsUri));
            _processId = processId ?? throw new ArgumentNullException(nameof(processId));
            _wpsClient = wpsClient ?? throw new ArgumentNullException(nameof(wpsClient));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _viewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));
            _requestFactory = requestFactory ?? throw new ArgumentNullException(nameof(requestFactory));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _loggerRepository = loggerRepository ?? throw new ArgumentNullException(nameof(loggerRepository));

            Title = _processId;

            WindowLoadedCommand = new RelayCommand(OnWindowsLoaded);
            ExecuteCommand = new RelayCommand(ExecuteProcess, () => !IsExecutingProcess);
        }

        private void ExecuteProcess()
        {
            try
            {
                IsExecutingProcess = true;

                var request = _requestFactory.CreateExecuteRequest(_processId, DataInputViewModels.ToList(),
                    DataOutputViewModels.ToList());

                _wpsClient.AsyncGetDocumentResultAs<string>(_wpsUri, request).ContinueWith((task) =>
                {
                    var session = task.Result;
                    var stopWatch = new Stopwatch();
                    stopWatch.Start();

                    session.Polled += (sender, args) =>
                    {
                        // TODO
                    };

                    session.Failed += (sender, args) =>
                    {
                        _dialogService.ShowMessageDialog("Failed",
                            "Something went wrong when executing the process. Please check the server or verify that your input is valid.");
                    };

                    session.Finished += (sender, args) =>
                    {
                        stopWatch.Stop();
                        _dialogService.ShowMessageDialog("Finished", $"The job {args.Result.JobId} has finished its execution. You can now access the outputs. ");
                        var outputs = new List<ResultItemViewModel>();

                        foreach (var output in args.Result.Outputs)
                        {
                            var outputVm = DataOutputViewModels.FirstOrDefault(ovm => ovm.Identifier.Equals(output.Id));
                            if (outputVm != null)
                            {
                                ResultItemViewModel resultItemVm = null;

                                if (outputVm is FileOutputViewModel fileVm)
                                {
                                    var filePath = fileVm.FilePath;
                                    var outputFileExists = File.Exists(fileVm.FilePath);
                                    if (outputFileExists)
                                    {
                                        var outputDirectory = Path.GetDirectoryName(fileVm.FilePath);
                                        if (outputDirectory != null)
                                        {
                                            var random = new Random();
                                            var newFileName = $"{output.Id}_{random.Next()}";
                                            var fileName = Path.GetFileName(fileVm.FilePath);

                                            var newFilePath = filePath = Path.Combine(outputDirectory, newFileName);
                                            File.WriteAllText(newFilePath, output.Data);

                                            _dialogService.ShowMessageDialog("Existing file", $"A file under the name {fileName} existed already, the output {output.Id} has been saved under {newFileName}.");
                                        }
                                    }
                                    else
                                    {
                                        File.WriteAllText(fileVm.FilePath, output.Data);
                                    }

                                    resultItemVm = new FileResultItemViewModel
                                    {
                                        FilePath = filePath,
                                        ResultId = outputVm.Identifier,
                                    };
                                }
                                else if (outputVm is LiteralDataOutputViewModel ldVm)
                                {
                                    var resultVm = new LiteralResultItemViewModel();

                                    try
                                    {
                                        var serializer = new XmlSerializationService();
                                        var val = serializer.Deserialize<LiteralDataValue>(output.Data);
                                        resultVm.Value = val.Value;
                                    }
                                    catch (Exception)
                                    {
                                        resultVm.Value = output.Data;
                                    }

                                    resultItemVm = resultVm;
                                }
                                else
                                {
                                    throw new InvalidOperationException("The output is of different type than the accepted ones.");
                                }

                                resultItemVm.ResultId = outputVm.Identifier;
                                outputs.Add(resultItemVm);
                            }
                        }

                        MessengerInstance.Send(new ExecutionFinishedMessage(args.Result.JobId, outputs, args.Result.ExpirationDate, stopWatch.Elapsed));
                    };

                    try
                    {
                        session.StartPolling().Wait();
                    }
                    catch (Exception e)
                    {
                        var logPath = _loggerRepository.LogExceptionReport(session.JobId, request, e);
                        var openLog = _dialogService.ShowErrorDialog(session.JobId);
                        if (openLog)
                        {
                            Process.Start(logPath);
                        }
                    }

                    _context.Invoke(() => IsExecutingProcess = false);
                });
            }
            catch (NoOutputSelectedException)
            {
                _dialogService.ShowMessageDialog("Error",
                    "You haven't selected any output, please select at least one.", DialogMessageType.Error);
                IsExecutingProcess = false;
            }
            catch (NullInputException e)
            {
                _dialogService.ShowMessageDialog("Error", $"You must provide a value to {e.InputName}.",
                    DialogMessageType.Error);
                IsExecutingProcess = false;
            }
            catch (Exception)
            {
                _dialogService.ShowMessageDialog("Error", "An unexpected error has occured, please try again.");
                IsExecutingProcess = false;
            }
        }

        private void OnWindowsLoaded()
        {
            _wpsClient.DescribeProcess(_wpsUri, _processId).ContinueWith((task) =>
            {
                try
                {
                    var result = task.Result;
                    var processOffering = result.FirstOrDefault();
                    if (processOffering != null)
                    {
                        foreach (var input in processOffering.Process.Inputs)
                        {
                            var vm = _viewModelFactory.CreateDataInputViewModel(input);
                            _context.Invoke(() =>
                            {
                                DataInputViewModels.Add(vm);
                            });
                        }

                        foreach (var output in processOffering.Process.Outputs)
                        {
                            var vm = _viewModelFactory.CreateDataOutputViewModel(output);
                            _context.Invoke(() =>
                            {
                                DataOutputViewModels.Add(vm);
                            });
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            });
        }

    }
}
