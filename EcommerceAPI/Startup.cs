using EcommerceAPI.Data;
using EcommerceAPI.Data.Dao;
using EcommerceAPI.Data.Repository;
using EcommerceAPI.Middleware;
using EcommerceAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace EcommerceAPI
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
            services.AddDbContext<EcommerceDbContext>(opts => opts.UseLazyLoadingProxies().UseMySQL(Configuration.GetConnectionString("EcommerceConnection")));
            services.AddTransient<IDbConnection>((sp) => new MySqlConnection(Configuration.GetConnectionString("EcommerceConnection")));

            services.AddScoped<ProdutoService>();
            services.AddScoped<CentroService>();
            services.AddScoped<CDRepository>();
            services.AddScoped<CategoriaService>();
            services.AddScoped<CategoriaRepository>();
            services.AddScoped<SubcategoriaService>();
            services.AddScoped<SubcategoriaRepository>();
            services.AddScoped<CarrinhoCompraService>();
            services.AddScoped<CarrinhoCompraRepository>();

            services.AddControllers().AddNewtonsoftJson(opts =>
            opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EcommerceAPI", Version = "v1" });
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EcommerceAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseMiddleware(typeof(ErrorMiddleware));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
