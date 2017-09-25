using System;

namespace StringCalculator.Framework
{
    public class NegativeNumberException : Exception
    {
        public NegativeNumberException(string message) : base(message)
        {
        }
    }
}
