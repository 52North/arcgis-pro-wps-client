using ArcGIS.Desktop.Framework;
using System.Windows;
using System.Windows.Controls;


namespace AgpWps.Client.Views
{
    /// <summary>
    /// Interaction logic for CapabilitiesView.xaml
    /// </summary>
    public partial class CapabilitiesView : UserControl
    {

        private const string MenuId = "AgpWps_Client_Views_Capabilities_Menu";

        public CapabilitiesView()
        {
            InitializeComponent();
            MenuBurgerButton.PopupMenu = FrameworkApplication.CreateContextMenu(MenuId);
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is CapabilitiesPane pane)
            {
                DataContext = pane.ViewModel;
            }
        }
    }
}
