using GalaSoft.MvvmLight.Command;
using System;
using System.Diagnostics;
using System.IO;

namespace AgpWps.Model.ViewModels
{
    public class FileResultItemViewModel : ResultItemViewModel
    {
        public string FilePath { get; }
        public string DirectoryPath => Path.GetDirectoryName(FilePath);

        public RelayCommand<string> OpenFolderCommand { get; }

        public FileResultItemViewModel(string filePath)
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));

            OpenFolderCommand = new RelayCommand<string>(OpenFileExplorerToDirectory);
        }

        // TODO: Inform the user when the path is invalid and add an abstraction layer by creating a service which will allow to call Process.Start somewhere else.
        public void OpenFileExplorerToDirectory(string path)
        {
            if(Directory.Exists(path))
            {
                Process.Start("explorer.exe", path);
            }
        }

    }
}
