using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace AgpWps.Model.ViewModels
{
    public class DataInputViewModel : ViewModelBase
    {

        private string _processName;
        public string ProcessName
        {
            get => _processName;
            set => Set(ref _processName, value);
        }

        private bool _isOptional;
        public bool IsOptional
        {
            get => _isOptional;
            set => Set(ref _isOptional, value);
        }

        private bool _isReference;
        public bool IsReference
        {
            get => _isReference;
            set => Set(ref _isReference, value);
        }

        private string _referenceUrl;
        public string ReferenceUrl
        {
            get => _referenceUrl;
            set => Set(ref _referenceUrl, value);
        }

        private FormatViewModel _selectedFormat;
        public FormatViewModel SelectedFormat
        {
            get => _selectedFormat;
            set => Set(ref _selectedFormat, value);
        }

        private ObservableCollection<FormatViewModel> _formats = new ObservableCollection<FormatViewModel>();
        public ObservableCollection<FormatViewModel> Formats
        {
            get => _formats;
            set => Set(ref _formats, value);
        }

    }
}
