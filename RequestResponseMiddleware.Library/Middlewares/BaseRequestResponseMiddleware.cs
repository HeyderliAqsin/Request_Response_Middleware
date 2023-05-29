﻿using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseMiddleware.Library.Middlewares
{
    public class BaseRequestResponseMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogWriter logWriter;
        private readonly RecyclableMemoryStreamManager recyclableMemoryStream;


        public BaseRequestResponseMiddleware( RequestDelegate next,ILogWriter logWriter)
        {
            this.next = next;
            this.logWriter = logWriter;
        }

        protected async Task<RequestReponseContext> BaseMiddlewareInvoke(HttpContext context)
        {
            var requestBody = await GetRequsetBody(context);

            var originalBodyStream = context.Response.Body;

            await using var responseBody = recyclableMemoryStream.GetStream();
            context.Response.Body = responseBody;

            var sw = Stopwatch.StartNew();

            await next(context);
            sw.Stop();


            context.Request.Body.Seek(0, SeekOrigin.Begin);
            string responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();

            context.Request.Body.Seek(0, SeekOrigin.Begin);


            var result= new RequestReponseContext(context)
            {
                ResponseCreationTime = TimeSpan.FromTicks(sw.ElapsedTicks),
                RequestBody = requestBody,
                ResponseBody = responseBodyText,

            };
            await logWriter.Write(result);
            return result;
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;

            stream.Seek(0, SeekOrigin.Begin);

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream, Encoding.UTF8);

            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;

            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                    0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return textWriter.ToString();

        }

        private async Task<string> GetRequsetBody(HttpContext context)
        {
            context.Request.EnableBuffering();

            await using var requestStream = recyclableMemoryStream.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);

            string reqBody = ReadStreamInChunks(requestStream);

            context.Request.Body.Seek(0, SeekOrigin.Begin);

            return reqBody;
        }
    }
}
