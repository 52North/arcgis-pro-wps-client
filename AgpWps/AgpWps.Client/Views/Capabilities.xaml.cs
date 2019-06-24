using AgpWps.Model.ViewModels;
using ArcGIS.Desktop.Framework;
using System;
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
            else if (e.NewValue is CapabilitiesViewModel vm)
            {
                // The binding to the ServersTreeView has to be done here explicitely with the VM ... TODO: Make this cleaner and ditch the automatically generated "VM" from ArcGIS ...
                ServersTreeView.ItemsSource = vm.Servers;
            }
            else
            {
                throw new Exception("Wrong DataContext provided.");
            }
        }
    }
}
