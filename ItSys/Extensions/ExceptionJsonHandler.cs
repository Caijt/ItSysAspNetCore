using ItSys.Dto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ItSys.Common;

namespace ItSys.Extensions
{
    public static class ExceptionJsonHandlerExtensions
    {
        public static IApplicationBuilder UseExceptionJsonHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionJsonHandlerMiddleware>();
        }
    }

    public class ExceptionJsonHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionJsonHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                ResultDto resultDto;
                //检测是否是业务抛出的异常
                if (ex is ResultException)
                {
                    resultDto = (ex as ResultException).result;
                }
                else
                {
                    //异常处理
                    var exceptionGuid = Guid.NewGuid();
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "Log");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    string fileName = Path.Combine(path, $"ExceptionLog-{DateTime.Now.ToString("yyyyMMdd")}.log");
                    using (FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            string exceptionContent =
                                $"{DateTime.Now.ToString()}\r\n" +
                                $"异常Id：{exceptionGuid.ToString()}\r\n" +
                                $"请求Url：{context.Request.Path}\r\n" +
                                $"发生异常的方法：{ex.TargetSite.DeclaringType?.FullName}.{ex.TargetSite.Name}\r\n" +
                                //$"参数：{string.Join(",", ex.TargetSite..Select(a => (a ?? "").ToString()))} " +
                                $"异常信息：{ex.Message}\r\n" +
                                $"{ex.StackTrace}\r\n";
                            sw.WriteLine(exceptionContent);
                        }

                    }

                    resultDto = new ResultDto<Guid>()
                    {
                        Code = -1,
                        Message = ex.Message,
                        Data = exceptionGuid
                    };
                }
                context.Response.ContentType = "application/json";
                var setting = new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                };
                string resultString = JsonConvert.SerializeObject(resultDto, setting);
                await context.Response.WriteAsync(resultString).ConfigureAwait(false);
            }
        }
    }
}
