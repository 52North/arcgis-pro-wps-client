using AgpWps.Model.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;


namespace AgpWps.Client.Views
{
    /// <summary>
    /// Interaction logic for ResultsView.xaml
    /// </summary>
    public partial class ResultsView : UserControl
    {
        public ResultsView()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is ResultsPane pane)
            {
                DataContext = pane.ViewModel;
            }
            else if (e.NewValue is ResultsViewModel vm)
            {
                ProcessesTreeView.ItemsSource = vm.Results;
            }
            else
            {
                throw new Exception("Wrong DataContext provided.");
            }
        }
    }
}
