using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Data;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Services.CharacterService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Http;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Services.WeaponService;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Services.FightService;

namespace CURSO_UDEMY_COGNIZANT_netcore31webapi
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

            addDoc(services);

            services.AddAutoMapper(typeof(Startup));

            addServices(services);
            addRepositories(services);

            addORM(services);

            addSecurity(services);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        private void addORM(IServiceCollection services)
        {
            //\\RAZORCREST
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        private void addDoc(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CURSO_UDEMY_COGNIZANT_netcore31webapi", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        private void addSecurity(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                      ValidateIssuer = false,
                      ValidateAudience = false
                  };
              });
        }

        private void addRepositories(IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
        }
        private void addServices(IServiceCollection services)
        {
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<IWeaponService, WeaponService>();
            services.AddScoped<IFightService, FightService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CURSO_UDEMY_COGNIZANT_netcore31webapi v1"));
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
