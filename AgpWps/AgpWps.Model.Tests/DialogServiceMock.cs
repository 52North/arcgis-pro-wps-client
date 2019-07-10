using AgpWps.Model.Enums;
using AgpWps.Model.Services;
using AgpWps.Model.ViewModels;
using System;

namespace AgpWps.Model.Tests
{
    public class DialogServiceMock : IDialogService
    {
        public void ShowMessageDialog(string title, string message)
        {
            throw new NotImplementedException();
        }

        public void ShowMessageDialog(string title, string message, DialogMessageType messageType)
        {
            throw new NotImplementedException();
        }

        public void ShowExecutionBuilderDialog(ExecutionBuilderViewModel vm)
        {
            throw new NotImplementedException();
        }

        public void ShowAddServerDialog()
        {
            throw new NotImplementedException();
        }

        public string ShowFileSelectionDialog(string title, string filter = null)
        {
            throw new NotImplementedException();
        }

        public string ShowFileSaveDialog(string title, string filter = null)
        {
            throw new NotImplementedException();
        }
    }
}
