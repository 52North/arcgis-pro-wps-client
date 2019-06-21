using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;


namespace AgpWps.Client.Views
{
    internal class CapabilitiesViewModel : DockPane
    {
        private const string DockPaneId = "AgpWps_Client_Views_Capabilities";
        private const string MenuId = "AgpWps_Client_Views_Capabilities_Menu";

        protected CapabilitiesViewModel()
        {
        }

        /// <summary>
        /// Show the DockPane.
        /// </summary>
        internal static void Show()
        {
            var pane = FrameworkApplication.DockPaneManager.Find(DockPaneId);
            pane?.Activate();
        }

        /// <summary>
        /// Text shown near the top of the DockPane.
        /// </summary>
        private string _heading = "Servers Capabilities";
        public string Heading
        {
            get => _heading;
            set
            {
                SetProperty(ref _heading, value, () => Heading);
            }
        }

        #region Burger Button

        /// <summary>
        /// Tooltip shown when hovering over the burger button.
        /// </summary>
        public string BurgerButtonTooltip => "Options";

        /// <summary>
        /// Menu shown when burger button is clicked.
        /// </summary>
        public System.Windows.Controls.ContextMenu BurgerButtonMenu => FrameworkApplication.CreateContextMenu(MenuId);

        #endregion
    }

    /// <summary>
    /// Button implementation to show the DockPane.
    /// </summary>
    internal class Capabilities_ShowButton : Button
    {
        protected override void OnClick()
        {
            CapabilitiesViewModel.Show();
        }
    }

    /// <summary>
    /// Button implementation for the button on the menu of the burger button.
    /// </summary>
    internal class Capabilities_MenuButton : Button
    {
        protected override void OnClick()
        {
        }
    }
}
