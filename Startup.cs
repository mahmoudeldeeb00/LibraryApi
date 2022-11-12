using Api_Project.BL.IRep;
using Api_Project.BL.Rep;
using Api_Project.DAL.DataBase;
using Api_Project.DAL.Entities;
using Api_Project.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_Project
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

            services.Configure<JWT>(Configuration.GetSection("JWT"));
            //configure cors
        //    services.AddCors();
            services.AddDbContext<DbContainer>(opt => opt.UseSqlServer(Configuration.GetConnectionString("LibraryDb")));
            //configure Identity 
            #region identity 
            services.AddIdentity<LibraryUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
            }
         ).AddEntityFrameworkStores<DbContainer>();
            #endregion

            //configure authentication
            #region ConfigureAuthentication
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    //  ValidIssuer = Configuration["JWT:Isser"],
                    ValidIssuer = Configuration["JWT:Isseur"],

                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
                };
            });
            #endregion

            #region addScoped

            services.AddScoped<DbContainer>();
            services.AddScoped<IAuthiService, AuthiService>();
            services.AddScoped<IBookRep, BookRep>();
            services.AddScoped<IAuthorRep, AuthorRep>();
            services.AddScoped<ILibraryRep, LibraryRep>();
            services.AddScoped<SelectHelper>();
           


            #endregion 

            services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api_Project", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // configure cors
            app.UseCors(options =>
            options.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin()

            );
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api_Project v1"));
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
