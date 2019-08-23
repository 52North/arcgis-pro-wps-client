using GalaSoft.MvvmLight.Command;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace AgpWps.Model.ViewModels
{
    public class FileResultItemViewModel : ResultItemViewModel
    {

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => Set(ref _filePath, value);
        }

        [XmlIgnore]
        public string DirectoryPath => Path.GetDirectoryName(FilePath);

        [XmlIgnore]
        public RelayCommand<string> OpenFolderCommand { get; }

        public FileResultItemViewModel()
        {
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
