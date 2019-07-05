using System.Windows.Input;

namespace AgpWps.Client.Views
{
    /// <summary>
    /// Interaction logic for AddServerPopup.xaml
    /// </summary>
    public partial class AddServerPopup : ArcGIS.Desktop.Framework.Controls.ProWindow
    {
        public AddServerPopup()
        {
            InitializeComponent();
            ServerInputBox.Focus();
        }

        private void AddServerPopupKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddConnectionButton.Command?.Execute(ServerInputBox.Text);
            }
        }
    }
}
