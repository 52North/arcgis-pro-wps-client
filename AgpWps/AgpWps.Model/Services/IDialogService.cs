using AgpWps.Model.Enums;

namespace AgpWps.Model.Services
{
    public interface IDialogService
    {

        void ShowMessageDialog(string title, string message);
        void ShowMessageDialog(string title, string message, DialogMessageType messageType);

        void ShowAddServerDialog();

    }
}
