using System;


namespace TP3.Exceptions
{
    public class ArmoryException : Exception
    {
        public ArmoryException()
        {
        }
        public ArmoryException(string message) : base(message)
        {
        }
        public ArmoryException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}