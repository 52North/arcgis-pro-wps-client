using AgpWps.Model.Services;
using GalaSoft.MvvmLight.Command;
using System;

namespace AgpWps.Model.ViewModels
{
    public class ComplexDataViewModel : DataInputViewModel
    {
        private readonly IDialogService _dialogService;

        private bool _isFile;
        public bool IsFile
        {
            get => _isFile;
            set => Set(ref _isFile, value);
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => Set(ref _filePath, value);
        }

        private string _input;
        public string Input
        {
            get => _input;
            set => Set(ref _input, value);
        }

        private RelayCommand _selectFileCommand;
        public RelayCommand SelectFileCommand
        {
            get => _selectFileCommand;
            set => Set(ref _selectFileCommand, value);
        }

        public ComplexDataViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            SelectFileCommand = new RelayCommand(SelectFile);
        }

        private void SelectFile()
        {
            var path = _dialogService.ShowFileSelectionDialog("Select your input file");
            if (path == null)
            {
                IsFile = false;
            }
            else
            {
                IsFile = true;
                FilePath = path;
            }
        }

    }
}
