using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ItSys.AOP
{
    public class SysUserAOP : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            string dataIntercept = $"{DateTime.Now.ToString()} " +
                $"当前执行方法：{invocation.TargetType.FullName}.{invocation.Method.Name} " +
                $"参数：{string.Join(",", invocation.Arguments.Select(a => (a ?? "").ToString()))} \r\n";
            invocation.Proceed();
            dataIntercept += $"返回结果：{invocation.ReturnValue}";

            string path = Directory.GetCurrentDirectory() + @"\Log";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = path + $@"\InterceptLog-{DateTime.Now.ToString("yyyyMMdd")}.log";
            StreamWriter sw = File.AppendText(fileName);
            sw.WriteLine(dataIntercept);
            sw.Close();
        }
    }
}
