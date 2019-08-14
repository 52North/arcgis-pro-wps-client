using AgpWps.Model.Messages;
using AgpWps.Model.Repositories;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AgpWps.Model.ViewModels
{
    public class ServerViewModel : ViewModelBase
    {

        private readonly IServerRepository _serverRepository;
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

        private ICommand _removeCommand;
        public ICommand RemoveCommand
        {
            get => _removeCommand;
            set => Set(ref _removeCommand, value);
        }

        public ServerViewModel(string serverUrl, IServerRepository serverRepository)
        {
            ServerUrl = serverUrl ?? throw new ArgumentNullException(nameof(serverUrl));
            _serverRepository = serverRepository ?? throw new ArgumentNullException(nameof(serverRepository));

            RemoveCommand = new RelayCommand(RemoveServer);
        }

        private void RemoveServer()
        {
            _serverRepository.RemoveServer(ServerUrl);
            MessengerInstance.Send(new ServerRemovedMessage(ServerUrl));
        }

    }
}
