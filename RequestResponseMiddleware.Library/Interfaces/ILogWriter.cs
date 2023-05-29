using RequestResponseMiddleware.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseMiddleware.Library.Interfaces
{
    public interface ILogWriter
    {
        ILogMessageCreator MessageCreator { get; }
        Task Write(RequestReponseContext context);
    }
}
