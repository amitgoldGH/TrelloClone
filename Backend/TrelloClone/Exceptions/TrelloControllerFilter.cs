using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace TrelloClone.Exceptions
{
    public class TrelloControllerFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {

            base.OnException(context);

            var exceptionType = context.Exception.GetType();

            if (exceptionType == typeof(UserNotFoundException))
            {
                context.Result = new ContentResult
                {
                    Content = ExceptionMessages.UserNotFound,
                    StatusCode = 404,
                };
            }
            else if (exceptionType == typeof(UserExceptions))
            {
                context.Result = new ContentResult
                {
                    Content = ExceptionMessages.UserBadRequest,
                    StatusCode = 400,
                };
            }
            else if (exceptionType == typeof(UserAlreadyExistsException))
            {
                context.Result = new ContentResult
                {
                    Content = ExceptionMessages.UserAlreadyExists,
                    StatusCode = 400,
                };
            }



        }
    }
}
