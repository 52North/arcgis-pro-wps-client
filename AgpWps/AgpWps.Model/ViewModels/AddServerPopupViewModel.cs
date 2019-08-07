using AgpWps.Model.Messages;
using AgpWps.Model.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;

namespace AgpWps.Model.ViewModels
{
    public class AddServerPopupViewModel : ViewModelBase
    {
        private readonly IServerRepository _serverRepo;

        private RelayCommand<string> _addConnectionCommand;
        public RelayCommand<string> AddConnectionCommand
        {
            get => _addConnectionCommand;
            set => Set(ref _addConnectionCommand, value);
        }

        private ObservableCollection<string> _servers = new ObservableCollection<string>();
        public ObservableCollection<string> Servers
        {
            get => _servers;
            set => Set(ref _servers, value);
        }

        public AddServerPopupViewModel(IServerRepository serverRepo)
        {
            _serverRepo = serverRepo ?? throw new ArgumentNullException(nameof(serverRepo));

            AddConnectionCommand = new RelayCommand<string>(AddServer);
            Servers = new ObservableCollection<string>(_serverRepo.GetServersUrl());
        }

        private void AddServer(string serverUrl)
        {
            var formattedUrl = serverUrl.Replace(" ", "").ToLower();
            Servers.Add(formattedUrl);
            Messenger.Default.Send(new ServerAddedMessage(formattedUrl));
        }

    }
}
