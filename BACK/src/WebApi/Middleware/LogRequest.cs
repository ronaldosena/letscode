using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace WebApi.Middleware
{
    public class RequestLoggingAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //<datetime> - Card <id> - <titulo> - <Remover|Alterar>

            string message = DateTime.Now + " - Card ";
            var result = (ObjectResult)context.Result;

            try
            {
                var card = (Application.Cards.Dtos.CardDto)result.Value;
                message += card.Id + " - " + card.Title + " - " + context.RouteData.Values["action"] + "\n";

            }
            catch
            {
                Debug.WriteLine("fail to get props");
            }

            Debug.WriteLine(message);
        }

        public void OnException(ExceptionContext context)
        {
            Debug.WriteLine(context.Exception.Message);
        }
    }
}
