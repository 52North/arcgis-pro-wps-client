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

        private AddServerPopup _popup;
        protected override void OnClick()
        {
            if (_popup != null)
                return;

            _popup = new AddServerPopup
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = new AddServerPopupViewModel()
            };
            _popup.Closed += (o, e) => { _popup = null; };
            _popup.ShowDialog();
        }
    }

    internal class CapabilitiesClearProvidersButton : Button
    {
        protected override void OnClick()
        {
        }
    }
}
