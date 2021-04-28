using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CodeCoolAPI.DAL.Context;
using CodeCoolAPI.DAL.UnitOfWork;
using CodeCoolAPI.Jwt;
using CodeCoolAPI.Middleware;
using CodeCoolAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace CodeCoolAPI
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
            var jwtSettings = new JwtSettings();
            Configuration.GetSection("jwtSettings").Bind(jwtSettings);
            services.AddSingleton(jwtSettings);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtSettings.Bearer;
                x.DefaultScheme = JwtSettings.Bearer;
                x.DefaultChallengeScheme = JwtSettings.Bearer;
            }).AddJwtBearer(cfg =>
            {
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            });
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            services.AddDbContext<CodecoolContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("CodeCoolConnection"),
                    optionsBuilder => optionsBuilder.MigrationsAssembly("CodeCoolAPI"));
            });
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<CodecoolContext>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "CodeCoolAPI", Version = "v1"});
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {JwtSettings.Bearer, new string[0]}
                };
                
                c.AddSecurityDefinition(JwtSettings.Bearer, new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddHttpContextAccessor();
            
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IMaterialTypeService, MaterialTypeService>();
            services.AddTransient<IMaterialService, MaterialService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowEveryOrigin", builder =>
                {
                    builder.AllowAnyOrigin();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCaching();
            app.UseCors("AllowEveryOrigin");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeCoolAPI v1"));
            }
            
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseAuthentication();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}