using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace AgpWps.Model.ViewModels
{
    public class DataOutputViewModel : ViewModelBase
    {

        private string _identifier;
        public string Identifier
        {
            get => _identifier;
            set => Set(ref _identifier, value);
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
