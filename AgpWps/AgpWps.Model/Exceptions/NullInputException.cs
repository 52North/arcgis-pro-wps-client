using System;

namespace AgpWps.Model.Exceptions
{
    public class NullInputException : Exception
    {
        public string InputName { get; }

        public NullInputException(string inputName) : base($"The provided input of name {inputName} cannot be null.")
        {
            InputName = inputName;
        }

    }
}
