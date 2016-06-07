namespace IgraNaMe4ove.Exceptions
{
    using System;

    public class InvalidKeyException : Exception
    {
        public InvalidKeyException(string message) : base(message)
        {
        }
    }
}
