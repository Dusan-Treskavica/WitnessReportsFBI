using System;

namespace Common.Exceptions
{
    public class RequestProcessingException : Exception
    {
        public override string Message => base.Message;
        public int HttpStatusCode { get; set; }

        public RequestProcessingException() : base()
        {
        }

        public RequestProcessingException(string Message) : base(Message)
        {
        }

        public RequestProcessingException(string Message, int httpStatusCode) : base(Message)
        {
            this.HttpStatusCode = httpStatusCode;
        }
    }
}
