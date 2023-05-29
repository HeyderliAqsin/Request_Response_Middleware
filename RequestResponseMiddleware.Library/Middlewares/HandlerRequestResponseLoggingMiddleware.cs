using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using RequestResponseMiddleware.Library.Interfaces;
using RequestResponseMiddleware.Library.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseMiddleware.Library.Middlewares
{
    public class HandlerRequestResponseLoggingMiddleware : BaseRequestResponseMiddleware
    {
        private readonly RequestDelegate next;
        private readonly Func<RequestReponseContext, Task> reqResHandler;

        public HandlerRequestResponseLoggingMiddleware(RequestDelegate next, Func<RequestReponseContext, Task> reqResHandler,ILogWriter logWriter) : base(next,logWriter)
        {
            this.next = next;
            this.reqResHandler = reqResHandler;
        }
        public async Task Invoke(HttpContext context)
        {
            var reqResContext = await BaseMiddlewareInvoke(context);

           await reqResHandler.Invoke(reqResContext);
        }
    }
}
