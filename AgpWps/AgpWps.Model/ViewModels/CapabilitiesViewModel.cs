﻿using AgpWps.Model.Enums;
using AgpWps.Model.Factories;
using AgpWps.Model.Messages;
using AgpWps.Model.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Wps.Client.Services;

namespace AgpWps.Model.ViewModels
{
    public class CapabilitiesViewModel : ViewModelBase
    {

        private readonly IWpsClient _wpsClient;
        private readonly IContext _context;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IDialogService _dialogService;

        private ObservableCollection<ServerViewModel> _servers = new ObservableCollection<ServerViewModel>();

        private ICommand _clearServersCommand;
        public ICommand ClearServersCommand
        {
            get => _clearServersCommand;
            set => Set(ref _clearServersCommand, value);
        }

        public ObservableCollection<ServerViewModel> Servers
        {
            get => _servers;
            set => Set(ref _servers, value);
        }

        public CapabilitiesViewModel(IWpsClient wpsClient, IContext context, IViewModelFactory viewModelFactory, IDialogService dialogService)
        {
            _wpsClient = wpsClient ?? throw new ArgumentNullException(nameof(wpsClient));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _viewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            ClearServersCommand = new RelayCommand(Servers.Clear);

            Messenger.Default.Register<ServerAddedMessage>(this, OnAddedServer);
        }

        private void OnAddedServer(ServerAddedMessage msg)
        {
            if (msg == null) throw new ArgumentNullException(nameof(msg));

            var serverUrl = msg.ServerUrl;
            _wpsClient.GetCapabilities(serverUrl).ContinueWith(resp =>
            {
                try
                {
                    var response = resp.Result;
                    var serverVm = new ServerViewModel
                    {
                        ServerName = serverUrl
                    };

                    foreach (var sum in response.ProcessSummaries)
                    {
                        serverVm.ProcessOfferings.Add(_viewModelFactory.CreateProcessOfferingViewModel(serverUrl, sum));
                    }

                    _context.Invoke(() =>
                    {
                        Servers.Add(serverVm);
                    });
                }
                catch (Exception e)
                {
                    _dialogService.ShowMessageDialog("Error", "Something went wrong. Please check your url and connection.", DialogMessageType.Error);
                    Debug.Write(e);
                }
            });
        }
    }
}
