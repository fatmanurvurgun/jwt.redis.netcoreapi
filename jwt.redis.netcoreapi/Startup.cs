using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jwt.redis.netcoreapi.Data;
using jwt.redis.netcoreapi.Data.Imp;
using jwt.redis.netcoreapi.Domain;
using jwt.redis.netcoreapi.Filters;
using jwt.redis.netcoreapi.Service;
using jwt.redis.netcoreapi.Service.Imp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace jwt.redis.netcoreapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Jwt Redis", Version = "v1" });
                c.OperationFilter<MyHeaderFilter>();
            });
            services.AddCors();
            services.AddLogging();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IDataManager, DataManager>();
            services.AddTransient<ICacheManager, RedisCacheManager>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<ExcepitonHandlerMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Jwt Redis");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
