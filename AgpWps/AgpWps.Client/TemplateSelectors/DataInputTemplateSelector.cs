using AgpWps.Model.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace AgpWps.Client.TemplateSelectors
{
    /// <summary>
    /// Template selector used to choose the appropriate data template depending on the <see cref="DataInputViewModel"/> given.
    /// </summary>
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
                case LiteralInputViewModel _:
                    return LiteralValueDataTemplate;
                case BoundingBoxInputViewModel _:
                    return BoundingBoxValueDataTemplate;
                case ComplexDataViewModel _:
                    return ComplexValueDataTemplate;
                default:
                    return UnknownDataTemplate;
            }
        }

    }
}
