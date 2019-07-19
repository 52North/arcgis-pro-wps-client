using AgpWps.Model.ViewModels;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using DryIoc;


namespace AgpWps.Client.Views
{
    internal class ResultsPane : DockPane
    {
        private const string _dockPaneID = "AgpWps_Client_Views_Results";

        private ResultsViewModel _viewModel;

        public ResultsViewModel ViewModel
        {
            get => _viewModel;
            set => SetProperty(ref _viewModel, value);
        }

        internal static ResultsPane Instance { get; private set; }

        protected ResultsPane()
        {
            Instance = this;
            ViewModel = WpsModule.Current.Container.Resolve<ResultsViewModel>();
        }

        /// <summary>
        /// Show the DockPane.
        /// </summary>
        internal static void Show()
        {
            var pane = FrameworkApplication.DockPaneManager.Find(_dockPaneID);
            pane?.Activate();
        }

    }

    /// <summary>
    /// Button implementation to show the DockPane.
    /// </summary>
    internal class Results_ShowButton : Button
    {
        protected override void OnClick()
        {
            ResultsPane.Show();
        }
    }
}
