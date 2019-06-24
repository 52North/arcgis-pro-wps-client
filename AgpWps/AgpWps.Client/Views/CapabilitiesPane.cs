using AgpWps.Model.Services;
using AgpWps.Model.ViewModels;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using DryIoc;

namespace AgpWps.Client.Views
{
    internal class CapabilitiesPane : DockPane
    {
        private const string DockPaneId = "AgpWps_Client_Views_Capabilities";

        private CapabilitiesViewModel _viewModel;

        public CapabilitiesViewModel ViewModel
        {
            get => _viewModel;
            set => SetProperty(ref _viewModel, value);
        }

        protected CapabilitiesPane()
        {
            ViewModel = WpsModule.Current.Container.Resolve<CapabilitiesViewModel>();
        }

        /// <summary>
        /// Show the DockPane.
        /// </summary>
        internal static void Show()
        {
            var pane = FrameworkApplication.DockPaneManager.Find(DockPaneId);
            pane?.Activate();
        }
    }

    /// <summary>
    /// Button implementation to show the DockPane.
    /// </summary>
    internal class CapabilitiesShowCapabilities : Button
    {
        protected override void OnClick()
        {
            CapabilitiesPane.Show();
        }
    }

    internal class CapabilitiesAddProviderButton : Button
    {

        protected override void OnClick()
        {
            var dialogService = WpsModule.Current.Container.Resolve<IDialogService>();
            dialogService.ShowAddServerDialog();
        }
    }

    internal class CapabilitiesClearProvidersButton : Button
    {
        protected override void OnClick()
        {
            var dialogService = WpsModule.Current.Container.Resolve<IDialogService>();
            dialogService.ShowAddServerDialog();
        }
    }
}
