using RequestResponseMiddleware.Library.Interfaces;
using RequestResponseMiddleware.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseMiddleware.Library.MessageCreators
{
    internal class LoggerFactoryMessageCreator :BaseLogMessageCreator, ILogMessageCreator
    {
        private readonly LoggingOptions loggingOptions;

        public LoggerFactoryMessageCreator(LoggingOptions loggingOptions)
        {
            this.loggingOptions = loggingOptions;
        }
        public string Create(RequestReponseContext context)
        {
           var sb=new StringBuilder();

            foreach (var field in loggingOptions.LoggingFields)
            {
                var value = GetValueByField(context,field);

                sb.AppendFormat("{0}:{1}\n",field,value);
            }

            return sb.ToString();
        }

  
    }
}
