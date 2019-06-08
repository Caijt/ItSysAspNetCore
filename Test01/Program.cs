using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Dynamic;
using System.Reflection;

namespace Test01
{
    class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine("Hello World!");
            //string a = "123";
            //Type b = typeof(Person);
            //a.GetType();
            // Person p1 = new Superman();
            //Gtman p2= p1 as Gtman;
            // p1.Say(b:"222");
            // Console.WriteLine(p2);
            //DateTime a = DateTime.Now;
            //var b = a.AddMinutes(12);

            //Test t = new Test();
            //t.A = "123";
            //Console.WriteLine(t.A);
            //var s1 = default(int);
            //Console.WriteLine(s1)
            //Console.WriteLine(Person.Name);
            //Console.WriteLine(Person.Name);
            //Superman.SetName();
            //Superman.SetName();
            //Console.WriteLine(Superman.Name);

            //Superman p = new Superman();
            //object p1 = p;
            //Console.WriteLine(p1 is Gtman);
            //string type = "Sys_UserDTOModel";
            //string a = Regex.Replace(type,"([^_A-Z])([A-Z])","$1_$2");

            //string msg2 = "Hello 'welcome' to 'china'";
            //Expression.Lambda<int,int>()

            //msg2 = Regex.Replace(msg2, "'(.+?)'", "[$0]");
            //Dictionary<string,int> s = new Dictionary<string,int>();
            //Type t = s.GetType();
            //Type t2 = t.GetGenericTypeDefinition();
            //Type t3 = typeof(Dictionary<,>);
            //Console.WriteLine(t);
            //Console.WriteLine(t2);
            //Console.WriteLine(t3);

            //Expression<Func<Person, bool>> e = p => p.PersonName == "123";
            //List<Person> personList = new List<Person>();
            //personList.Where()
            //var linq = from p in personList
            //           where e.
            //           select p;
            //Expression<Func<Person, bool>> expression = p => p.PersonName == "1"&&p.PersonName=="3";
            //Expression<Func<Person, bool>> expression2 = p2 => p2.PersonName == "2";

            //Expression<Func<Person, object>> expression3 = p => p.PersonName;
            //Expression<Func<Person, object>> expression4 = p => p.Age;
            //var e = expression3.Body;
            //var e2 = Expression.Constant("hehe");
            //var e3 = Expression.Equal(e, e2);
            //var e4 = Expression.Or(expression.Body,expression2.Body);

            //MailMessage m = new MailMessage();
            //m.IsBodyHtml = true;
            //m.Subject = "test";
            //m.Body = "hehe";
            //m.From = new MailAddress("caijt@golden-glass.cn", "计算机中心");
            //m.To.Add("59001731@qq.com");
            //SmtpClient sc = new SmtpClient();
            //sc.Host = "mail.golden-glass.cn";
            //sc.UseDefaultCredentials = true;
            //sc.Credentials = new NetworkCredential("caijt@golden-glass.cn", "admi5ncai");
            //try
            //{
            //    sc.SendMailAsync(m);
            //    //sc.SendCompleted
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            //Console.WriteLine("hello");
            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine(Guid.NewGuid().ToString());
            //}
            //string path = Path.Combine("Uploads","Attach/Common");
            //Console.WriteLine(DateTime.Now.ToString("yyMM"));
            //string a = "IT1905001";
            //string a = "dd";
            //Console.WriteLine(a.Substring(6));
            //dynamic obj = 1;
            //bool a = IsPropertyExist(obj, "b");
            //bool  f= ((IDictionary<string, object>)obj).ContainsKey("c");
            //var dt = new DateTime(2019, 13, 5);
            //var m=Regex.Match("AB1", "[A-Z]+");
            //Console.WriteLine(dt.ToString("yyyy-M-d"));
            //Person p = new Person()
            //{
            //    Age = 1,
            //    PersonName = "caijt"
            //};
            //var prop = p.GetType().GetProperty("PersonName");
            //Console.WriteLine(prop.GetValue(p));
            List<int> companyIds = new List<int> { 1, 2, 3 };

            Expression<Func<Person, int>> exp = p => p.Age;
            var constant = Expression.Constant(companyIds, companyIds.GetType());
            var methodInfo = companyIds.GetType().GetMethod("Contains");
            var method = Expression.Call(constant, methodInfo, exp.Body);
            var a= Expression.Lambda<Func<Person, bool>>(method, exp.Parameters);

            Console.WriteLine(exp);


            Console.ReadKey();
        }
        public static bool IsPropertyExist(dynamic data, string propertyname)
        {
            if (data is ExpandoObject)
                return ((IDictionary<string, object>)data).ContainsKey(propertyname);
            return data.GetType().GetProperty(propertyname) != null;
        }
    }

    public struct Test
    {
        public string A;
    }
    public class Person
    {
        public string PersonName { get; set; }
        public int Age { get; set; }
        public DateTime CreateTime { get; set; }
        static Person()
        {
            Name = "这是在Person静态构造方法赋的值";
            Console.WriteLine("执行Person静态构造方法");
        }
        public static void Test(string a1, string a2)
        {
            Console.WriteLine("this is test func");
        }
        public static string Name = "最初始的值";
        public Person()
        {

            Name = "这是Person构造函数赋的值";
        }
        public void Say(string a = "1", string b = "2")
        {

        }
        public void SayTest<T>(T a)
        {
        }
    }

    public class Superman : Person
    {
        public static string Name2 = "";
        static Superman()
        {
            Name2 = "这是在Superman静态构造函数赋的值";
            Console.WriteLine("执行Superman静态构造函数");
        }
        public static void SetName()
        {
            Console.WriteLine("这是SetName方法");
            Name = "这是用SetName赋的值";
        }
    }

    public class Gtman : Person
    {
    }
}
