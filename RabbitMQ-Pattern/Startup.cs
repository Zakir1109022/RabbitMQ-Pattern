using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RabbitMQ.Application.Queries;
using RabbitMQ.Application.Services;
using RabbitMQ.Client;
using RabbitMQ.Common.Producer;
using RabbitMQ.Common.RabbitMQConnection;
using RabbitMQ.Common.Services;
using RabbitMQ.Common.TenantConfig;
using RabbitMQ.Extentions;
using RabbitMQ.Infrastructure;
using RabbitMQ.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RabbitMQ_Pattern", Version = "v1" });
            });


            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,// on production make it true
                    ValidateAudience = false,// on production make it true
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context => {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });


            #region Tenant Config Dependencies

            services.Configure<TenantSettings>(Configuration.GetSection("TenantSettings"));

            services.AddSingleton<ITenantSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<TenantSettings>>().Value);

            services.AddSingleton<IHttpContextAccessor>(serviceProvider =>
               serviceProvider.GetRequiredService<IOptions<HttpContextAccessor>>().Value);

            #endregion

            #region RabbitMQ Dependencies

            services.AddSingleton<IRabbitMQConnection>(sp =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = Configuration["EventBus:HostName"]
                };

                if (!string.IsNullOrEmpty(Configuration["EventBus:UserName"]))
                {
                    factory.UserName = Configuration["EventBus:UserName"];
                }

                if (!string.IsNullOrEmpty(Configuration["EventBus:Password"]))
                {
                    factory.Password = Configuration["EventBus:Password"];
                }

                return new RabbitMQConnection(factory);
            });

            services.AddSingleton<EventBusRabbitMQProducer>();
            services.AddSingleton<EventBusRabbitMQConsumer>();

            #endregion 


            //Add Automapper
            services.AddAutoMapper(typeof(Startup));

            // Add MediatR
            //services.AddMediatR(typeof(Startup));

            services.AddMediatR(typeof(GetAllOrderQuery).GetTypeInfo().Assembly);
            //services.AddMediatR(typeof(CreateOrderHandler).GetTypeInfo().Assembly);


            #region Project Dependencies

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IAccessTokenService, AccessTokenService>();
            services.AddScoped<IUserService, UserService>();

            #endregion


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RabbitMQ_Pattern v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Initilize Rabbit Listener in ApplicationBuilderExtentions
            app.UseRabbitListener();

        }
    }
}
