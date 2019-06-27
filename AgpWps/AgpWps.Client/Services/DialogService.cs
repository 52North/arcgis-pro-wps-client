﻿using AgpWps.Client.Views;
using AgpWps.Model.Enums;
using AgpWps.Model.Services;
using AgpWps.Model.ViewModels;
using DryIoc;
using System;
using System.Windows;
using MessageBox = ArcGIS.Desktop.Framework.Dialogs.MessageBox;

namespace AgpWps.Client.Services
{
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

        public void ShowExecutionBuilderDialog(ExecutionBuilderViewModel vm)
        {
            if (vm == null) throw new ArgumentNullException(nameof(vm));

            var executionBuilder = new ExecutionBuilder
            {
                Owner = Application.Current.MainWindow,
                DataContext = vm
            };
            executionBuilder.ShowDialog();
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