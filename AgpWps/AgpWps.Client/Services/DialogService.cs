using AgpWps.Client.Views;
using AgpWps.Model.Enums;
using AgpWps.Model.Services;
using AgpWps.Model.ViewModels;
using DryIoc;
using Microsoft.Win32;
using System;
using System.Windows;
using Application = System.Windows.Application;
using MessageBox = ArcGIS.Desktop.Framework.Dialogs.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace AgpWps.Client.Services
{
    /// <summary>
    /// A service offering ArcGIS Pro specific implemented dialogues.
    /// </summary>
    public class DialogService : IDialogService
    {
        public void ShowMessageDialog(string title, string message)
        {
            ShowMessageDialog(title, message, DialogMessageType.Informational);
        }

        public void ShowMessageDialog(string title, string message, DialogMessageType messageType)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, GetIcon(messageType));
        }

        public void ShowAddServerDialog()
        {
            var vm = WpsModule.Current.Container.Resolve<AddServerPopupViewModel>();
            var popup = new AddServerPopup
            {
                DataContext = vm
            };
            popup.ShowDialog();
        }

        public string ShowFileSelectionDialog(string title, string filter = null)
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = false,
                CheckFileExists = true,
                CheckPathExists = true,
                Title = title,
                Filter = filter ?? "All files (*.*)|*.*"
            };

            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }

        public string ShowFileSaveDialog(string title, string filter = null)
        {
            var dialog = new SaveFileDialog
            {
                Filter = filter ?? "All files (*.*)|*.*",
                Title = title
            };

            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }

        public bool ShowErrorDialog(string jobId)
        {
            var result =
                MessageBox.Show(
                    $"An error has occured while executing the job {jobId}. Would you like to open the error report now?",
                    "Unexpected error", MessageBoxButton.YesNo, MessageBoxImage.Error);

            return result == MessageBoxResult.Yes;
        }

        public void ShowExecutionBuilderDialog(ExecutionBuilderViewModel vm)
        {
            if (vm == null) throw new ArgumentNullException(nameof(vm));

            var executionBuilder = new ExecutionBuilder
            {
                Owner = Application.Current.MainWindow,
                DataContext = vm
            };
            //executionBuilder.ShowDialog();
            executionBuilder.Show();
        }

        private static MessageBoxImage GetIcon(DialogMessageType type)
        {
            switch (type)
            {
                case DialogMessageType.Informational:
                    return MessageBoxImage.Information;
                case DialogMessageType.Warning:
                    return MessageBoxImage.Warning;
                case DialogMessageType.Error:
                    return MessageBoxImage.Error;
                default:
                    return MessageBoxImage.None;
            }
        }

    }
}
