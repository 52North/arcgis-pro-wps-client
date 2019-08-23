using AgpWps.Model.Enums;
using AgpWps.Model.ViewModels;

namespace AgpWps.Model.Services
{
    public interface IDialogService
    {

        /// <summary>
        /// Show a simple popup box to the user.
        /// </summary>
        /// <param name="title">The title of the popup</param>
        /// <param name="message">The message of the popup</param>
        void ShowMessageDialog(string title, string message);

        /// <summary>
        /// Show a simple and explanatory popup box to the user.
        /// </summary>
        /// <param name="title">The title of the popup</param>
        /// <param name="message">The message of the popup</param>
        /// <param name="messageType">The type of the message to be shown (ex: An alert, a warning etc.)</param>
        void ShowMessageDialog(string title, string message, DialogMessageType messageType);

        /// <summary>
        /// Show the execution panel for a given process.
        /// </summary>
        /// <param name="vm">The viewmodel of the execution context</param>
        void ShowExecutionBuilderDialog(ExecutionBuilderViewModel vm);
        
        /// <summary>
        /// Show the popup to add a server URL to the capabilities panel
        /// </summary>
        void ShowAddServerDialog();

        /// <summary>
        /// Shows a dialog where the user can select the file.
        /// </summary>
        /// <param name="title">The title shown on the dialog window</param>
        /// <param name="filter">The filter applied to the file selection, if null then every file is allowed. (ex: txt files (*.txt)|*.txt|All files (*.*)|*.*)</param>
        /// <returns>The path to the selected file, if null then no file was selected</returns>
        string ShowFileSelectionDialog(string title, string filter = null);

        /// <summary>
        /// Show a dialog where the user is prompted to save a file under a specific name.
        /// </summary>
        /// <param name="title">The title shown on the dialog window</param>
        /// <param name="filter">The filter applied to the file to be saved, if null then every file is allowed. (ex: txt files (*.txt)|*.txt|All files (*.*)|*.*)</param>
        /// <returns>The path to the file to be created</returns>
        string ShowFileSaveDialog(string title, string filter = null);

        /// <summary>
        /// Prompt a dialog to the user asking him if he wants to open the error report file.
        /// </summary>
        /// <param name="jobId">The job id of the error</param>
        /// <returns>Boolean indicating whether the person wants to open the error report or not</returns>
        bool ShowErrorDialog(string jobId);

    }
}
