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
    public abstract class RequestResponseLoggingMiddleware : BaseRequestResponseMiddleware
    {

        public RequestResponseLoggingMiddleware(RequestDelegate next,ILogWriter logWriter)
            : base(next,logWriter)
        {
        }
        public async Task Invoke(HttpContext context)
        {
           var reqResContex= await BaseMiddlewareInvoke(context);

        }

    }
}
