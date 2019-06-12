using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore;
using ItSys.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Swashbuckle.AspNetCore.Swagger;
using ItSys.Extensions;
using Microsoft.EntityFrameworkCore;
using ItSys.ApiGroup;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;
using ItSys.Service;
using Autofac.Extras.DynamicProxy;
using ItSys.AOP;
using AutoMapper;
using ItSys.EntityFramework;
using ItSys.Logger;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ItSys.Dto;
using ItSys.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace ItSys
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            
            #region 认证授权
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Permission", p =>
                {
                    p.Requirements.Add(new PermissionRequirement());
                });
            }).AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JwtSettings:Issuer"],
                    ValidAudience = Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:SecretKey"])),
                    ClockSkew = TimeSpan.Zero //token过期缓冲时间，默认5分钟
                };
                //options.Events = new JwtBearerEvents()
                //{

                //    //OnMessageReceived = context =>
                //    //{
                //    //    return Task.CompletedTask;
                //    //},
                //    OnTokenValidated = context =>
                //    {
                //        return Task.CompletedTask;
                //    },
                //    OnChallenge = async context =>
                //    {
                //        context.HandleResponse();
                //        context.Response.ContentType = "application/json";
                //        await context.Response.WriteAsync("{\"tes\":1}");
                //    },
                //    OnAuthenticationFailed = context =>
                //    {
                //        return Task.CompletedTask;
                //    }
                //};
            });
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            #endregion

            #region Swagger
            services.AddSwaggerGen(options =>
            {
                //遍历ApiGroupNames所有枚举值生成接口文档，Skip(1)是因为Enum第一个FieldInfo是内置的一个Int值
                typeof(ApiGroupNames).GetFields().Skip(1).ToList().ForEach(f =>
                {
                    //获取枚举值上的特性
                    var info = f.GetCustomAttributes(typeof(GroupInfoAttribute), false).OfType<GroupInfoAttribute>().FirstOrDefault();
                    options.SwaggerDoc(f.Name, new Swashbuckle.AspNetCore.Swagger.Info
                    {
                        Title = info?.Title,
                        Version = info?.Version,
                        Description = info?.Description
                    });
                });
                //没有加特性的分到这个NoGroup上
                options.SwaggerDoc("NoGroup", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "无分组"
                });
                //判断接口归于哪个分组
                options.DocInclusionPredicate((docName, apiDescription) =>
                {
                    if (docName == "NoGroup")
                    {
                        //当分组为NoGroup时，只要没加特性的都属于这个组
                        return string.IsNullOrEmpty(apiDescription.GroupName);
                    }
                    else
                    {
                        return apiDescription.GroupName == docName;
                    }
                });
                var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
                options.IncludeXmlComments(System.IO.Path.Combine(basePath, "ItSys.xml"), true);
                options.IncludeXmlComments(System.IO.Path.Combine(basePath, "ItSys.Dto.xml"), true);
                #region Token绑定到ConfigureServices
                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
                options.AddSecurityRequirement(
                    new Dictionary<string, IEnumerable<string>>
                    {
                { "Bearer", Enumerable.Empty<string>() }
                    });
                #endregion
            });
            #endregion

            #region Mvc
            services.AddMvc(options =>
            {
                //options.Conventions.Insert(0, new RouteConvention());
                options.Filters.Add(new AuthorizeFilter("Permission"));
                //可接收id[]=1&id[]=2的query参数
                //config.ValueProviderFactories.Add(new JQueryQueryStringValueProviderFactory());
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {

                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    foreach (var state in context.ModelState)
                    {
                        dict.Add(state.Key, string.Join(" | ", state.Value.Errors.Select(e => e.ErrorMessage)));
                    }
                    var resultDto = new ResultDto<Dictionary<string, string>>()
                    {
                        Code = -1,
                        Message = "输入参数有误",
                        Data = dict
                    };
                    return new JsonResult(resultDto);
                };
            });
            #endregion

            #region 跨域
            services.AddCors(options =>
            {
                options.AddPolicy("All", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials();
                });
            });
            #endregion


            #region 数据库
            services.AddDbContext<ItSysDbContext>(options =>
            {
                var loggerFactory = new LoggerFactory();
                loggerFactory.AddProvider(new EFLoggerProvider());
                options.UseMySql(Configuration.GetConnectionString("MySql"))
                    //.UseLazyLoadingProxies()
                    .UseLoggerFactory(loggerFactory);
            });
            #endregion

            #region 注册AutoMapper
            services.AddAutoMapper(typeof(EntityService<,,>));
            #endregion

            services.AddHttpContextAccessor();
            services.AddTransient<AuthContext>();

            #region Autofac
            var builder = new ContainerBuilder();
            //获取服务层程序集
            var assemblyService = Assembly.GetAssembly(typeof(EntityService<,,,,>));
            ////获取仓储层程序集
            //var assemblyRepository = Assembly.GetAssembly(typeof(BaseRepository<>));
            //注册拦截器

            //builder.RegisterType<SysUserAOP>();
            //注册服务层及仓储层服务及拦截器
            builder.RegisterAssemblyTypes(assemblyService)
                .InstancePerLifetimeScope();
            //.EnableClassInterceptors()
            //.InterceptedBy(typeof(SysUserAOP));
            builder.Populate(services);
            var container = builder.Build();
            return new AutofacServiceProvider(container);
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory)
        {
            //#region 跨域
            //app.UseCors("All");
            //#endregion
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region 异常捕捉
            app.UseExceptionJsonHandler();
            #endregion

            #region 认证
            app.UseAuthentication();
            #endregion

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                //遍历ApiGroupNames所有枚举值生成接口文档，Skip(1)是因为Enum第一个FieldInfo是内置的一个Int值
                typeof(ApiGroupNames).GetFields().Skip(1).ToList().ForEach(f =>
                {
                    //获取枚举值上的特性
                    var info = f.GetCustomAttributes(typeof(GroupInfoAttribute), false).OfType<GroupInfoAttribute>().FirstOrDefault();
                    options.SwaggerEndpoint($"/swagger/{f.Name}/swagger.json", info != null ? info.Title : f.Name);

                });
                options.SwaggerEndpoint("/swagger/NoGroup/swagger.json", "无分组");
            });
            #endregion

            app.UseMvc(routers =>
            {
                routers.MapRoute(
                    "area", "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
