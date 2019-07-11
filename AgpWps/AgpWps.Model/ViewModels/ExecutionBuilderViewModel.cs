using AgpWps.Model.Enums;
using AgpWps.Model.Exceptions;
using AgpWps.Model.Factories;
using AgpWps.Model.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
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

        private ICommand _executeCommand;
        public ICommand ExecuteCommand
        {
            get => _executeCommand;
            set => Set(ref _executeCommand, value);
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

        public ExecutionBuilderViewModel(string wpsUri, string processId, IWpsClient wpsClient, IContext context, IViewModelFactory viewModelFactory, IRequestFactory requestFactory, IDialogService dialogService)
        {
            _wpsUri = wpsUri ?? throw new ArgumentNullException(nameof(wpsUri));
            _processId = processId ?? throw new ArgumentNullException(nameof(processId));
            _wpsClient = wpsClient ?? throw new ArgumentNullException(nameof(wpsClient));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _viewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));
            _requestFactory = requestFactory ?? throw new ArgumentNullException(nameof(requestFactory));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            Title = _processId;

            WindowLoadedCommand = new RelayCommand(OnWindowsLoaded);
            ExecuteCommand = new RelayCommand(ExecuteProcess);
        }

        private void ExecuteProcess()
        {
            try
            {
                var request = _requestFactory.CreateExecuteRequest(_processId, DataInputViewModels.ToList(),
                    DataOutputViewModels.ToList());
            }
            catch (NoOutputSelectedException)
            {
                _dialogService.ShowMessageDialog("Error",
                    "You haven't selected any output, please select at least one.", DialogMessageType.Error);
            }
            catch (NullInputException e)
            {
                _dialogService.ShowMessageDialog("Error", $"You must provide a value to {e.InputName}.",
                    DialogMessageType.Error);
            }
            catch (Exception)
            {
                _dialogService.ShowMessageDialog("Error", "An unexpected error has occured, please try again.");
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
