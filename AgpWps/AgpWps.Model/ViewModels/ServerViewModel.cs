using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;

namespace AgpWps.Model.ViewModels
{
    public class ServerViewModel : ViewModelBase
    {
        public string ServerUrl { get; }

        private string _serverName;
        public string ServerName
        {
            get => _serverName;
            set => Set(ref _serverName, value);
        }

        private ObservableCollection<ProcessOfferingViewModel> _processOfferings = new ObservableCollection<ProcessOfferingViewModel>();
        public ObservableCollection<ProcessOfferingViewModel> ProcessOfferings
        {
            get => _processOfferings;
            set => Set(ref _processOfferings, value);
        }

        public ServerViewModel(string serverUrl)
        {
            ServerUrl = serverUrl ?? throw new ArgumentNullException(nameof(serverUrl));
        }

    }
}
