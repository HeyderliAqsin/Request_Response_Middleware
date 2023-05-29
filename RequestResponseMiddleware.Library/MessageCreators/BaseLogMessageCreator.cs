using RequestResponseMiddleware.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseMiddleware.Library.MessageCreators
{
    public abstract class BaseLogMessageCreator
    {
     
        protected string GetValueByField(RequestReponseContext context,LogFields field)
        {
            return field switch
            {
                LogFields.Request => context.RequestBody,
                LogFields.Response => context.ResponseBody,
                LogFields.QueryString => context.context?.Request?.QueryString.Value,
                LogFields.Path => context.context?.Request?.Path.Value,
                LogFields.HostName => context.context?.Request?.Host.Value,
                LogFields.RequestLength => context.RequestLength.ToString(),
                LogFields.ResponseLength => context.ResponseLenght.ToString(),
                LogFields.ResponseTiming => context.FormattedCreationTime,

                _ => string.Empty

            };
        }
    }
}
