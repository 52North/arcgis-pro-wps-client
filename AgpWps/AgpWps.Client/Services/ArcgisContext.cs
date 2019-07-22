using AgpWps.Model.Services;
using System;
using System.Windows;

namespace AgpWps.Client.Services
{
    /// <summary>
    /// The UI context of execution, use this to manipulate objects that are bound directly to the UI.
    /// </summary>
    public class ArcgisContext : IContext
    {
        /// <summary>
        /// Invoke an action on the UI thread.
        /// </summary>
        /// <param name="action">The action to be invoked.</param>
        public void Invoke(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            Application.Current.Dispatcher.Invoke(action);
        }
    }
}
