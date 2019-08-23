using AgpWps.Model.Repositories;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Xml.Serialization;
using AgpWps.Model.Messages;

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
        [XmlIgnore] // TimeSpan isn't correctly serialized by the serializer and a custom serialization could be implemented.
        public TimeSpan ElapsedTime
        {
            get => _elapsedTme;
            set => Set(ref _elapsedTme, value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public long ElapsedTimeTicks
        {
            get => ElapsedTime.Ticks;
            set => ElapsedTime = new TimeSpan(value);
        }

        private ICommand _removeResultCommand;

        [XmlIgnore]
        public ICommand RemoveResultCommand
        {
            get => _removeResultCommand;
            set => Set(ref _removeResultCommand, value);
        }

        private ObservableCollection<ResultItemViewModel> _processes;
        public ObservableCollection<ResultItemViewModel> Processes
        {
            get => _processes;
            set => Set(ref _processes, value);
        }

        public ResultViewModel()
        {
            RemoveResultCommand = new RelayCommand(() =>
            {
                MessengerInstance.Send(new ResultRemovedMessage(JobId));
            });
        }

    }
}
