using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace cw_8_ko_s23109.Middlewares
{
    public class ExceptionLoggerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly string filePath = "logs.txt";

        public ExceptionLoggerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync (HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await LogException(context,ex);
            }

        }

        public async Task LogException (HttpContext context, Exception exception)
        {
            using var stream = new StreamWriter(filePath, true);
            await stream.WriteLineAsync (DateTime.Now+"|"+context.TraceIdentifier+"|" + exception);
            await next(context);

        }

    }
}
