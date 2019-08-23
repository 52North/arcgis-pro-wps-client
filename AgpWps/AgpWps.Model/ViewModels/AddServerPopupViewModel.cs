using AgpWps.Model.Messages;
using AgpWps.Model.Repositories;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using AgpWps.Model.Enums;
using AgpWps.Model.Services;

namespace AgpWps.Model.ViewModels
{
    public class AddServerPopupViewModel : ViewModelBase
    {
        private readonly IServerRepository _serverRepo;
        private readonly IDialogService _dialogService;

        private RelayCommand<string> _addConnectionCommand;
        public RelayCommand<string> AddConnectionCommand
        {
            get => _addConnectionCommand;
            set => Set(ref _addConnectionCommand, value);
        }

        private RelayCommand _clearCacheCommand;
        public RelayCommand ClearCacheCommand
        {
            get => _clearCacheCommand;
            set => Set(ref _clearCacheCommand, value);
        }

        private ObservableCollection<string> _servers = new ObservableCollection<string>();
        public ObservableCollection<string> Servers
        {
            get => _servers;
            set => Set(ref _servers, value);
        }

        public AddServerPopupViewModel(IServerRepository serverRepo, IDialogService dialogService)
        {
            _serverRepo = serverRepo ?? throw new ArgumentNullException(nameof(serverRepo));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            AddConnectionCommand = new RelayCommand<string>(AddServer);
            ClearCacheCommand = new RelayCommand(ClearCache);
            Servers = new ObservableCollection<string>(_serverRepo.GetServersUrl());
        }

        private void AddServer(string serverUrl)
        {
            var formattedUrl = serverUrl.Replace(" ", "");
            Servers.Add(formattedUrl);
            Messenger.Default.Send(new ServerAddedMessage(formattedUrl));
        }

        private void ClearCache()
        {
            _serverRepo.RemoveAll();
            Servers.Clear();

            _dialogService.ShowMessageDialog("Cleared!", "Your server history is now empty!", DialogMessageType.Informational);
        }

    }
}
