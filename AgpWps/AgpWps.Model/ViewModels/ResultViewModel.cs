using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;

namespace AgpWps.Model.ViewModels
{
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

        private ObservableCollection<Tuple<string, string>> _processes;
        public ObservableCollection<Tuple<string, string>> Processes
        {
            get => _processes;
            set => Set(ref _processes, value);
        }

    }
}
