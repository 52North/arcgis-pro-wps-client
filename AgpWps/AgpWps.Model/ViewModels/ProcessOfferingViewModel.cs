using AgpWps.Model.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using Wps.Client.Services;

namespace AgpWps.Model.ViewModels
{
    public class ProcessOfferingViewModel : ViewModelBase
    {
        private readonly string _processId;
        private readonly IDialogService _dialogService;
        private readonly IWpsClient _wpsClient;

        private RelayCommand _openExecutionPanelCommand;
        public RelayCommand OpenExecutionPanelCommand
        {
            get => _openExecutionPanelCommand;
            set => Set(ref _openExecutionPanelCommand, value);
        }

        private string _processName;
        public string ProcessName
        {
            get => _processName;
            set => Set(ref _processName, value);
        }

        private string _jobControlOptions;

        public string JobControlOptions
        {
            get => _jobControlOptions;
            set => Set(ref _jobControlOptions, value);
        }

        private string _transmissionModes;
        public string TransmissionModes
        {
            get => _transmissionModes;
            set => Set(ref _transmissionModes, value);
        }

        private string _keywords;
        public string Keywords
        {
            get => _keywords;
            set => Set(ref _keywords, value);
        }

        private string _version;
        public string Version
        {
            get => _version;
            set => Set(ref _version, value);
        }

        private string _model;
        public string Model
        {
            get => _model;
            set => Set(ref _model, value);
        }

        private string _abstract;
        public string Abstract
        {
            get => _abstract;
            set => Set(ref _abstract, value);
        }

        public ProcessOfferingViewModel(string processId, IDialogService dialogService, IWpsClient wpsClient)
        {
            _processId = processId ?? throw new ArgumentNullException(nameof(processId));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _wpsClient = wpsClient ?? throw new ArgumentNullException(nameof(wpsClient));

            OpenExecutionPanelCommand = new RelayCommand(OpenExecutionPanel);
        }

        private void OpenExecutionPanel()
        {
            var executionVm = new ExecutionBuilderViewModel(_processId, _wpsClient);
            _dialogService.ShowExecutionBuilderDialog(executionVm);
        }

    }
}
