using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using WebApplication1.Model.Context;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Business.Implementations;
using Serilog;
using WebApplication1.Repository.Generic;
using Microsoft.Net.Http.Headers;
using WebApplication1.Hypermedia.Filters;
using WebApplication1.Hypermedia.Enricher;
using Microsoft.AspNetCore.Rewrite;

namespace WebApplication1
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            var connection = Configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connection));
            if (Environment.IsDevelopment())
            {
                MigrateDatabase(connection);
            }
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentReponseEnricherList.Add(new PersonEnricher());
            services.AddSingleton(filterOptions);
            services.AddApiVersioning();
            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "MyRestAPI",
                        Version = "v1",
                        Description = "Input some description here",
                        Contact = new OpenApiContact 
                        {
                            Name = "Higor Amorim",
                            Url = new Uri("https://github.com/HigorAM97/RestWithAsp.NET5Udemy")
                        }
                    });
            });
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));

            }).AddXmlSerializerFormatters();
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            services.AddScoped<IBookBusiness, BookBusinessImplementation>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyRestAPI - v1");
            });
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");
            });
        }
        private void MigrateDatabase(string connection)
        {
            try
            {
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" +
                    "" },
                    IsEraseDisabled = true
                };
                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed ", ex);
                throw;
            }
        }
    }
}
