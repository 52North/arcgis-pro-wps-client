using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AgpWps.Client.Views
{
    /// <summary>
    /// Interaction logic for AddServerPopup.xaml
    /// </summary>
    public partial class AddServerPopup
    {

        private const string PlaceholderText = "Server URL";

        public AddServerPopup()
        {
            InitializeComponent();
            ServerInputComboBox.LostKeyboardFocus += ServerInputComboBoxOnLostKeyboardFocus;
            ServerInputComboBox.GotKeyboardFocus += ServerInputComboBoxOnGotKeyboardFocus;
            ServerInputComboBox.Text = PlaceholderText;
            ServerInputComboBox.Foreground = Brushes.Gray;

            AddConnectionButton.Click += (sender, args) => Close();
        }

        private void ServerInputComboBoxOnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is ComboBox box)
            {
                if (box.Text.Equals(PlaceholderText, StringComparison.InvariantCultureIgnoreCase))
                {
                    box.Text = string.Empty;
                    box.Foreground = Brushes.Black;
                }
            }
        }

        private void ServerInputComboBoxOnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is ComboBox box)
            {
                if (string.IsNullOrEmpty(box.Text) || box.Text.Replace(" ", "").Length == 0)
                {
                    box.Text = PlaceholderText;
                    box.Foreground = Brushes.Gray;
                }
            }
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
