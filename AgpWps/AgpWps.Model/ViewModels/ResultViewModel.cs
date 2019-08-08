using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace AgpWps.Model.ViewModels
{
    [XmlInclude(typeof(LiteralResultItemViewModel))]
    [XmlInclude(typeof(FileResultItemViewModel))]
    public class ResultViewModel : ViewModelBase
    {

        private string _jobId;
        public string JobId
        {
            get => _jobId;
            set => Set(ref _jobId, value);
        }

        private TimeSpan _elapsedTme;
        public TimeSpan ElapsedTime
        {
            get => _elapsedTme;
            set => Set(ref _elapsedTme, value);
        }

        private ObservableCollection<ResultItemViewModel> _processes;
        public ObservableCollection<ResultItemViewModel> Processes
        {
            get => _processes;
            set => Set(ref _processes, value);
        }

    }
}
