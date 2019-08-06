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

            AddConnectionButton.Click += (sender, args) => Close();
        }

        private void AddServerPopupKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (AddConnectionButton.Command != null && AddConnectionButton.Command.CanExecute(ServerInputBox.Text))
                {
                    AddConnectionButton.Command.Execute(ServerInputBox.Text);
                    Close();
                }
            }
        }
    }
}
