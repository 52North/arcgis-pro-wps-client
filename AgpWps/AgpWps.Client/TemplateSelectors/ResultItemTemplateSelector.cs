using AgpWps.Model.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AgpWps.Client.TemplateSelectors
{
    public class ResultItemTemplateSelector : DataTemplateSelector
    {

        public DataTemplate LiteralResultTemplate { get; set; }
        public DataTemplate FileResultTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item)
            {
                case LiteralResultItemViewModel _:
                    return LiteralResultTemplate;
                case FileResultItemViewModel _:
                    return FileResultTemplate;
                default:
                    throw new InvalidOperationException($"There was no data template found for {nameof(item)}.");
            }
        }
    }
}
