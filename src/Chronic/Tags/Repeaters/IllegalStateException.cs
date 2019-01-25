using System;

namespace Naticron.Tags.Repeaters
{
    public class IllegalStateException : Exception
    {
        public IllegalStateException(string message) 
            : base(message)
        {
            
        }
    }
}