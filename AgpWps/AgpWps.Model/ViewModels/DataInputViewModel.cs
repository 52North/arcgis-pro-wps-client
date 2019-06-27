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

        private bool _isReference;
        public bool IsReference
        {
            get => _isReference;
            set => Set(ref _isReference, value);
        }

        private string _selectedFormat;
        public string SelectedFormat
        {
            get => _selectedFormat;
            set => Set(ref _selectedFormat, value);
        }

        private ObservableCollection<string> _formats = new ObservableCollection<string>();
        public ObservableCollection<string> Formats
        {
            get => _formats;
            set => Set(ref _formats, value);
        }

    }
}
