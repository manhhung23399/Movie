using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movie.Core;
using Movie.Core.Entities;
using Movie.Infrastructure;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Movie.ApiIntegration.Cache;
using Microsoft.AspNetCore.Routing;
using Movie.ApiIntegration.HubContainer;
using Movie.ApiIntegration.BusinessException;

namespace Movie
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment _env;
        public Startup(IWebHostEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile($"appsetting.${env.EnvironmentName}.json",  true)
                .AddEnvironmentVariables();

            configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var configSettings = configuration.GetSection("Firebase");
            AppSettings settings = configSettings.Get<AppSettings>();

            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1",
                new OpenApiInfo { Title = "Movie API Integration", Version = "v1" })
            );
            services.AddMemoryCache();
            services.AddResponseCaching();

            services.AddSignalR();

            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("My API", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });
            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = settings.JwtAuthFirebase;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = settings.JwtAuthFirebase,
                    ValidateAudience = true,
                    ValidAudience = settings.ProjectId,
                    ValidateLifetime = true
                };
                options.SaveToken = true;
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<AppSettings>(configSettings);
            services.Configure<RouteOptions>(
                options =>
                {
                    options.LowercaseUrls = true;
                    options.LowercaseQueryStrings = true;
                });
            services.RegisterDIContainer(_env.WebRootPath);
            services.AddScoped<ISetCacheMemory, SetCacheMemory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSwagger();
            if (env.IsDevelopment())
            {
                app.ConfigureCustomExceptionMiddleware();
            }
            else
            {
                app.ConfigureCustomExceptionMiddleware();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("My API");
            app.UseRouting();

            //middleware authentication: cookies auth, token auth
            app.UseAuthentication();
            //middleware authorization: policy base, role base, claim
            app.UseAuthorization();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie API Integration V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<CommentHub>("/movieHub", options =>
                {
                    options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
                });
            });
        }
    }
}
