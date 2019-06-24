using System;

namespace AgpWps.Model.Services
{
    public interface IContext
    {

        void Invoke(Action action);

    }
}
