using AgpWps.Model.Services;
using System;
using System.Windows;

namespace AgpWps.Client.Services
{
    public class ArcgisContext : IContext
    {
        public void Invoke(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }
    }
}
