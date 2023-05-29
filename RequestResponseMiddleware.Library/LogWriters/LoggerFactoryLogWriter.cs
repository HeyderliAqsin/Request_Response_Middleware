using Microsoft.Extensions.Logging;
using RequestResponseMiddleware.Library.Interfaces;
using RequestResponseMiddleware.Library.MessageCreators;
using RequestResponseMiddleware.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseMiddleware.Library.LogWriters
{
    internal class LoggerFactoryLogWriter : ILogWriter
    {

        public readonly ILogger Logger;
        public readonly LoggingOptions LoggingOptions;
        public ILogMessageCreator MessageCreator { get; }

        internal LoggerFactoryLogWriter(ILoggerFactory loggerFactory,LoggingOptions loggingOptions)
        {
            Logger = loggerFactory.CreateLogger(loggingOptions.LoggerCategoryName);
            this.LoggingOptions = loggingOptions;
            MessageCreator = new LoggerFactoryMessageCreator(loggingOptions);

        }


        public async Task Write(RequestReponseContext context)
        {
            var message=MessageCreator.Create(context);
            Logger.Log(LoggingOptions.LogLevel, message);

            await Task.CompletedTask;
        }
    }
}
