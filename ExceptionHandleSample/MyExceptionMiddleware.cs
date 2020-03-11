using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ExceptionHandleSample
{
    public class MyExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public MyExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                Log(e);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.OK;

                var response = new ResultObj()
                {
                    Code = (int) HttpStatusCode.InternalServerError,
                    Message = HttpStatusCode.InternalServerError.ToString()
                };
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }

        private void Log(Exception exception)
        {
        }
    }

    public class ResultObj
    {
        public int Code { get; set; }

        public string Message { get; set; }
    }
}