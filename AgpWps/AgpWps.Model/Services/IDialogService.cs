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

        /// <summary>
        /// Shows a dialog where the user can select the file.
        /// </summary>
        /// <param name="title">The title shown on the dialog window</param>
        /// <param name="filter">The filter applied to the file selection, if null then every file is allowed. (ex: txt files (*.txt)|*.txt|All files (*.*)|*.*)</param>
        /// <returns>The path to the selected file, if null then no file was selected</returns>
        string ShowFileSelectionDialog(string title, string filter = null);

    }
}
