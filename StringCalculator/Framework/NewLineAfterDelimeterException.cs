using System;

namespace StringCalculator.Framework
{
    public class NewLineAfterDelimeterException : Exception
    {
        public NewLineAfterDelimeterException(string message) : base(message)
        {
        }
    }
}
