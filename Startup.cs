using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi {
    public class Startup {
        public IConfiguration Configuration { get; }

        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        // add services to the DI container
        public void ConfigureServices (IServiceCollection services) {
            SetDatabaseConnection ();
            services.AddAutoMapper (typeof (Startup));
            services.AddControllers ();
            services.AddCors (opt => {
                opt.AddPolicy ("CorsPolicy", policy => {
                    //policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                    //policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200").AllowCredentials();
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    // policy.AllowAnyHeader ().AllowAnyMethod ().WithOrigins ("http://localhost:4200")
                    //     .WithExposedHeaders ("WWW-Authenticate").AllowCredentials ();
                });
            });

            // configure strongly typed settings object
            services.Configure<AppSettings> (Configuration.GetSection ("AppSettings"));

            // configure DI for application services
            services.AddScoped<IUserService, UserService> ();
        }

        // configure the HTTP request pipeline
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseHttpsRedirection ();
            app.UseRouting ();

            // global cors policy
            app.UseCors ("CorsPolicy");

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware> ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
        // public string TestSetDatabaseConnection () {
        //     ConnectionFactory.KprConnectionString = Configuration.GetConnectionString ("KprConnection");
        //     string i = Convert.ToString (ConnectionFactory.GetDatabaseInstanceByHost (DataBaseHostEnum.KPR));
        //     // Console.WriteLine(ConnectionFactory.KprConnectionString);
        //     // var conn = dataContext.callS
        //     return i;
        // }
        public void SetDatabaseConnection () {
            ConnectionFactory.KprConnectionString = Configuration.GetConnectionString ("KprConnection");
            ConnectionFactory.LapConnectionString = Configuration.GetConnectionString ("LapConnection");
            ConnectionFactory.KppConnectionString = Configuration.GetConnectionString ("KppConnection");
            ConnectionFactory.KprConnectionString = Configuration.GetConnectionString ("KprConnection");

            DataContextConfiguration.DEFAULT_DATABASE = ConnectionFactory.GetDatabaseHostByName (Configuration.GetSection ("DefaultDatabase").Value);
        }
    }
}