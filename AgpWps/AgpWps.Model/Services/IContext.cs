using System;

namespace AgpWps.Model.Services
{
    /// <summary>
    /// Usually represents the UI context of the view framework. It allows to execute code on the UI thread.
    /// </summary>
    public interface IContext
    {

        /// <summary>
        /// Invoke an action on the UI thread.
        /// </summary>
        /// <param name="action">The action to be executed</param>
        void Invoke(Action action);

    }
}
