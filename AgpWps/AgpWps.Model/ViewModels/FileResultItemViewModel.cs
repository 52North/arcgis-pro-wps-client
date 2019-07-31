using System;
using System.Windows.Input;

namespace AgpWps.Model.ViewModels
{
    public class FileResultItemViewModel : ResultItemViewModel
    {
        public string DirectoryPath { get; }

        private ICommand _openFolderCommand;
        public ICommand OpenFolderCommand
        {
            get => _openFolderCommand;
            set => Set(ref _openFolderCommand, value);
        }

        public FileResultItemViewModel(string directoryPath)
        {
            DirectoryPath = directoryPath ?? throw new ArgumentNullException(nameof(directoryPath));
        }

    }
}
