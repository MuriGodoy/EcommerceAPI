using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;
using UsuariosApi.Data;
using UsuariosApi.Interfaces;
using UsuariosApi.Models;
using UsuariosApi.Services;

namespace UsuariosApi
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
            services.AddDbContext<UserDbContext>(options => options.UseLazyLoadingProxies().UseMySQL(Configuration.GetConnectionString("UsuarioConnection")));
            services.AddTransient<IDbConnection>((sp) => new MySqlConnection(Configuration.GetConnectionString("UsuarioConnection")));

            services.AddScoped<CadastroUsuarioService, CadastroUsuarioService>();
            services.AddScoped<LoginService, LoginService>();
            services.AddScoped<LogoutService, LogoutService>();
            services.AddScoped<TokenService, TokenService>();
            services.AddScoped<TrocaSenhaService>();
            services.AddScoped<TrocaRoleService, TrocaRoleService>();
            //services.AddScoped<ITrocaSenhaService, TrocaSenhaService>();


            services.AddIdentity<CustomIdentityUser, IdentityRole<int>>(
                opt => opt.SignIn.RequireConfirmedEmail = true)
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UsuariosApi", Version = "v1" });
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn")),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UsuariosApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
