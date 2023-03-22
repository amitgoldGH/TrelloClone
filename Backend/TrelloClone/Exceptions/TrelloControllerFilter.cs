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

            if (exceptionType == typeof(NotImplementedException))
            {
                context.Result = new ContentResult

                {
                    Content = ExceptionMessages.NotImplemented,
                    StatusCode = 500
                };
            }
            else if (exceptionType == typeof(UserNotFoundException))
            {
                context.Result = new ContentResult
                {
                    Content = ExceptionMessages.UserNotFound,
                    StatusCode = 404,
                };
            }
            else if (exceptionType == typeof(UserBadRequestException))
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
            else if (exceptionType == typeof(BoardBadRequestException))
            {
                context.Result = new ContentResult
                {
                    Content = ExceptionMessages.BoardBadRequest,
                    StatusCode = 400,
                };
            }
            else if (exceptionType == typeof(BoardNotFoundException))
            {
                context.Result = new ContentResult
                {
                    Content = ExceptionMessages.BoardNotFound,
                    StatusCode = 404
                };
            }



        }
    }
}
