using System.Runtime.Serialization;

namespace TrelloClone.Exceptions
{
    public class BoardNotFoundException : Exception
    {
        public BoardNotFoundException()
        {
        }

        public BoardNotFoundException(string? message) : base(message)
        {
        }

        public BoardNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BoardNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    public class BoardBadRequestException : Exception
    {
        public BoardBadRequestException()
        {
        }

        public BoardBadRequestException(string? message) : base(message)
        {
        }

        public BoardBadRequestException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BoardBadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
