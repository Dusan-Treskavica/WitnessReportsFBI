using System;

namespace Common.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException() : base()
        {
        }

        public DatabaseException(string message) : base(message)
        {
        }

        public DatabaseException(string message, Exception exception) : base(message, exception)
        {
        }

        public override string Message => base.Message;
    }
}
