using AgpWps.Model.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace AgpWps.Client.TemplateSelectors
{
    public class DataInputTemplateSelector : DataTemplateSelector
    {

        public DataTemplate LiteralValueDataTemplate { get; set; }
        public DataTemplate BoundingBoxValueDataTemplate { get; set; }
        public DataTemplate ComplexValueDataTemplate { get; set; }
        public DataTemplate UnknownDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item)
            {
                case LiteralInputViewModel vm:
                    return LiteralValueDataTemplate;
                default:
                    return UnknownDataTemplate;
            }
        }

    }
}
