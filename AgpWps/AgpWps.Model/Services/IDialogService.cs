using AgpWps.Model.Enums;
using AgpWps.Model.ViewModels;

namespace AgpWps.Model.Services
{
    public interface IDialogService
    {

        void ShowMessageDialog(string title, string message);
        void ShowMessageDialog(string title, string message, DialogMessageType messageType);
        void ShowExecutionBuilderDialog(ExecutionBuilderViewModel vm);

        void ShowAddServerDialog();

    }
}
