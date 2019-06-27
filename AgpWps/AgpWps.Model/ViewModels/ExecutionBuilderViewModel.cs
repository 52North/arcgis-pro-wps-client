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

        private ObservableCollection<DataInputViewModel> _dataInputViewModels = new ObservableCollection<DataInputViewModel>();
        public ObservableCollection<DataInputViewModel> DataInputViewModels
        {
            get => _dataInputViewModels;
            set => Set(ref _dataInputViewModels, value);
        }

        private readonly string _wpsUri;
        private readonly string _processId;
        private readonly IWpsClient _wpsClient;
        private readonly IContext _context;
        private readonly IViewModelFactory _viewModelFactory;

        public ExecutionBuilderViewModel(string wpsUri, string processId, IWpsClient wpsClient, IContext context, IViewModelFactory viewModelFactory)
        {
            _wpsUri = wpsUri ?? throw new ArgumentNullException(nameof(wpsUri));
            _processId = processId ?? throw new ArgumentNullException(nameof(processId));
            _wpsClient = wpsClient ?? throw new ArgumentNullException(nameof(wpsClient));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _viewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));

            Title = _processId;

            WindowLoadedCommand = new RelayCommand(OnWindowsLoaded);
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
                                _dataInputViewModels.Add(vm);
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
