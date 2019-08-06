using AgpWps.Model.Enums;
using AgpWps.Model.Factories;
using AgpWps.Model.Messages;
using AgpWps.Model.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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
        private readonly IServerRepository _serverRepo;

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

        public CapabilitiesViewModel(IWpsClient wpsClient,
            IContext context,
            IViewModelFactory viewModelFactory,
            IDialogService dialogService,
            IServerRepository serverRepo)
        {
            _wpsClient = wpsClient ?? throw new ArgumentNullException(nameof(wpsClient));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _viewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _serverRepo = serverRepo ?? throw new ArgumentNullException(nameof(serverRepo));

            ClearServersCommand = new RelayCommand(Servers.Clear);

            Messenger.Default.Register<ServerAddedMessage>(this, OnAddedServer);
        }

        private void OnAddedServer(ServerAddedMessage msg)
        {
            if (msg == null) throw new ArgumentNullException(nameof(msg));

            var serverUrl = msg.ServerUrl;

            if (Servers.Any(svm => svm.ServerUrl.Equals(serverUrl)))
            {
                _dialogService.ShowMessageDialog("Server exists", $"The server '{serverUrl}' already exists in the capabilities panel.", DialogMessageType.Informational);
                return;
            }

            _serverRepo.AddServer(serverUrl);

            var serverVm = new ServerViewModel(serverUrl)
            {
                ServerName = "Loading..."
            };

            _context.Invoke(() =>
            {
                Servers.Add(serverVm);
            });

            _wpsClient.GetCapabilities(serverUrl).ContinueWith(resp =>
            {
                try
                {
                    var response = resp.Result;
                    
                    _context.Invoke(() =>
                    {
                        serverVm.ServerName =
                            $"{response.ServiceIdentification.Title} ({response.ServiceProvider.ProviderName})";

                        foreach (var sum in response.ProcessSummaries)
                        {
                            serverVm.ProcessOfferings.Add(_viewModelFactory.CreateProcessOfferingViewModel(serverUrl, sum));
                        }
                    });
                }
                catch (Exception)
                {
                    _dialogService.ShowMessageDialog("Error", "Something went wrong. Please check your url and connection.", DialogMessageType.Error);
                    _context.Invoke(() =>
                    {
                        Servers.Remove(serverVm);
                    });
                }
            });
        }
    }
}
