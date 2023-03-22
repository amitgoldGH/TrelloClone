using System.Runtime.Serialization;

namespace TrelloClone.Exceptions
{
    public class UserBadRequestException : Exception
    {
        public UserBadRequestException()
        {
        }

        public UserBadRequestException(string? message) : base(message)
        {
        }

        public UserBadRequestException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserBadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
        {
        }

        public UserNotFoundException(string? message) : base(message)
        {
        }

        public UserNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException()
        {
        }

        public UserAlreadyExistsException(string? message) : base(message)
        {
        }

        public UserAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
