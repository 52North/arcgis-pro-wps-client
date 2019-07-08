using AgpWps.Model.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AgpWps.Model.ViewModels
{
    public class DataOutputViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;

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

        public ICommand ChooseFileCommand { get; }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => Set(ref _filePath, value);
        }

        public DataOutputViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            ChooseFileCommand = new RelayCommand(SelectFile);
        }

        private void SelectFile()
        {
            var path = _dialogService.ShowFileSelectionDialog("Select the file where you want to save this output");
            FilePath = path;
        }

    }
}
