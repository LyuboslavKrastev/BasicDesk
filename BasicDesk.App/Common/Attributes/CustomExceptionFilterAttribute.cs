using BasicDesk.App.Common.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Text;

namespace BasicDesk.App.Common.Attributes
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger logger;

        public CustomExceptionFilterAttribute(ILogger logger)
        {
            this.logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(context.ActionDescriptor.DisplayName);
            builder.AppendLine(context.Exception.Message);
            this.logger.Log(builder.ToString());
        }
    }
}
