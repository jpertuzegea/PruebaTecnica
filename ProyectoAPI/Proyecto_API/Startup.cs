using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Proyecto_BLL.Interfaces;
using Proyecto_DAO;
using Proyecto_DAO.Interfaces;
using Proyecto_DAO.Models;
using Proyecto_DAO.Repositories;
using System;
using System.IO;
using System.Text;

namespace Proyecto_BLL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ContextDB>(opt => opt.UseSqlServer(Configuration.GetConnectionString("BDConnetion")));

            JWTAuthentication JWTAuthenticationSection = Configuration.GetSection("JWTAuthentication").Get<JWTAuthentication>();




            //services.AddSingleton<IFileProvider>(
            //new PhysicalFileProvider(
            //    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));



            var key = Encoding.ASCII.GetBytes(JWTAuthenticationSection.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTAuthenticationSection.Secret)),

                    ValidateIssuer = false,
                    ValidIssuer = "",

                    ValidateAudience = false,
                    ValidAudience = "",

                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.FromMilliseconds(2)
                };
            });


            services.AddScoped<I_Bll_Users, Bll_Users>();
            services.AddScoped<I_Bll_Books, Bll_Books>();
            services.AddScoped<I_Bll_Authors, Bll_Authors>();


            //  
            services.AddSingleton<IMemoryCache, MemoryCache>();


            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddControllers().AddNewtonsoftJson(options => { options.UseMemberCasing(); });// Convierte Json Salida en CamellCAse -- Microsoft.AspNetCore.Mvc.NewtonsoftJson

            services.AddCors(setupAction =>
            {
                setupAction.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                   .AllowAnyMethod()
                     .AllowAnyHeader();
                });
            });

            services.AddControllers();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
