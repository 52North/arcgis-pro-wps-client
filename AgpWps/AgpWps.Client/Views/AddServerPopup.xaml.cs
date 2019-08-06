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
            ServerInputComboBox.Focus();

            AddConnectionButton.Click += (sender, args) => Close();
        }

        private void AddServerPopupKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (AddConnectionButton.Command != null && AddConnectionButton.Command.CanExecute(ServerInputComboBox.Text))
                {
                    AddConnectionButton.Command.Execute(ServerInputComboBox.Text);
                    Close();
                }
            }
        }
    }
}
