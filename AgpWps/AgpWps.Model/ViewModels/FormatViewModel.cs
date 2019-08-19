using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace AgpWps.Model.ViewModels
{
    public class FormatViewModel : ViewModelBase
    {

        private string _mimeType;
        public string MimeType
        {
            get => _mimeType;
            set => Set(ref _mimeType, value);
        }

        private string _selectedSchema;
        public string SelectedSchema
        {
            get => _selectedSchema;
            set => Set(ref _selectedSchema, value);
        }

        private ObservableCollection<string> _schemas = new ObservableCollection<string>();
        public ObservableCollection<string> Schemas
        {
            get => _schemas;
            set => Set(ref _schemas, value);
        }

        private string _selectedEncoding;
        public string SelectedEncoding
        {
            get => _selectedEncoding;
            set => Set(ref _selectedEncoding, value);
        }

        private ObservableCollection<string> _encodings = new ObservableCollection<string>();
        public ObservableCollection<string> Encodings
        {
            get => _encodings;
            set => Set(ref _encodings, value);
        }

    }
}
