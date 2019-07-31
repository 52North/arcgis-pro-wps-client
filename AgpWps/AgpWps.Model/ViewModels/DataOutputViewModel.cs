using AgpWps.Model.Services;
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
