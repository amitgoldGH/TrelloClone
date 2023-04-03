using System.Runtime.Serialization;

namespace TrelloClone.Exceptions
{
    public class UnauthorizedAction : Exception
    {
        public UnauthorizedAction() { }

        public UnauthorizedAction(string? message) : base(message)
        {
        }

        public UnauthorizedAction(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnauthorizedAction(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
