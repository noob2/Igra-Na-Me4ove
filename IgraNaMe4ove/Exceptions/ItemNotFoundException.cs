namespace IgraNaMe4ove.Exceptions
{
    using System;

    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message) : base(message)
        {
        }
    }
}
