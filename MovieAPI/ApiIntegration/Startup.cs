using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movie.Core;
using Movie.Infrastructure;
using System;
using System.Linq;

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

            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1",
                new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Movie API Integration", Version = "v1" })
            ); 
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<AppSettings>(configSettings);
            services.Configure<RouteOptions>(
                options =>
                {
                    options.LowercaseUrls = true;
                    options.LowercaseQueryStrings = true;
                });
            services.RegisterDIContainer(_env.WebRootPath);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSwagger();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie API Integration V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
