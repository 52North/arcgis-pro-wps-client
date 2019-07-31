using AgpWps.Model.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AgpWps.Client.TemplateSelectors
{
    public class DataOutputTemplateSelector : DataTemplateSelector
    {

        public DataTemplate LiteralDataTemplate { get; set; }
        public DataTemplate FileDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item)
            {
                case LiteralDataOutputViewModel _:
                    return LiteralDataTemplate;
                case FileOutputViewModel _:
                    return FileDataTemplate;
                default:
                    throw new InvalidOperationException($"Could not find an output data template for {nameof(item)}.");
            }
        }

    }
}
