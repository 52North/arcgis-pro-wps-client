using AgpWps.Model.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace AgpWps.Model.ViewModels
{
    public class AddServerPopupViewModel : ViewModelBase
    {

        private RelayCommand<string> _addConnectionCommand;
        public RelayCommand<string> AddConnectionCommand
        {
            get => _addConnectionCommand;
            set => Set(ref _addConnectionCommand, value);
        }

        public AddServerPopupViewModel()
        {
            AddConnectionCommand = new RelayCommand<string>(AddServer);
        }

        private void AddServer(string serverUrl)
        {
            Messenger.Default.Send(new ServerAddedMessage(serverUrl));
        }

    }
}
