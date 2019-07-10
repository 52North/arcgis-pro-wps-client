using AgpWps.Model.Services;
using System;

namespace AgpWps.Model.Tests
{
    public class ContextMock : IContext
    {
        public void Invoke(Action action)
        {
            action?.Invoke();
        }
    }
}
