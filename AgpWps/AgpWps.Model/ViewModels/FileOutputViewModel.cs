using AgpWps.Model.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;

namespace AgpWps.Model.ViewModels
{
    public class FileOutputViewModel : DataOutputViewModel
    {

        private readonly IDialogService _dialogService;

        public ICommand ChooseFileCommand { get; }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => Set(ref _filePath, value);
        }

        public FileOutputViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            ChooseFileCommand = new RelayCommand(SelectFile);
        }

        private void SelectFile()
        {
            var path = _dialogService.ShowFileSaveDialog("Select the file where you want to save this output");
            FilePath = path;
        }

    }
}
