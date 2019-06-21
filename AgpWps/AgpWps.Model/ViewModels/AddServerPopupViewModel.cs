using System.Windows.Input;

namespace AgpWps.Model.ViewModels
{
    public class AddServerPopupViewModel : ViewModelBase
    {

        private ICommand _addConnectionCommand;
        public ICommand AddConnectionCommand
        {
            get => _addConnectionCommand;
            set => Set(ref _addConnectionCommand, value);
        }

        private void AddServer(string serverUrl)
        {
            
        }

    }
}
