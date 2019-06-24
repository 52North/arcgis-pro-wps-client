using GalaSoft.MvvmLight;

namespace AgpWps.Model.ViewModels
{
    public class ProcessOfferingViewModel : ViewModelBase
    {

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

    }
}
