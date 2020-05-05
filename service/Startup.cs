using System.Runtime.Serialization;
using System.Transactions;
using System.ComponentModel.Design;
using System.Text;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using service.Repositories.Interface;
using service.Repositories.Repository;

namespace service
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
            string _dbConnectionString = Configuration["ConnectionString:DbConnectionString"];
            services.AddTransient<IDbConnection>(p => new SqlConnection(_dbConnectionString));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(j =>
            {
                j.SaveToken = true;
                j.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwtProp:key"])),
                    ValidIssuer = Configuration["jwtProp:validIssuer"],
                    ValidAudience = Configuration["jwtProp:validAudience"],
                    ClockSkew = TimeSpan.Zero
                };
            });

            //Add Cookie
            services.AddDistributedMemoryCache();
            services.AddSession(p =>
            {
                p.IdleTimeout = TimeSpan.FromMinutes(60);
                p.Cookie.HttpOnly = true;
                p.Cookie.IsEssential = true;
            });
            services.AddScoped<IUser, UserRepository>();
            services.AddScoped<IRole, RoleRepository>();
            services.AddScoped<IUserRole, UserRoleRepository>();
            services.AddScoped<IMenu, MenuRepository>();
            services.AddScoped<ICategory, CategoryRepository>();
            services.AddScoped<IPost, PostRepository>();
            // services.AddScoped<IUserMenu,>();
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

            app.UseAuthentication();

            app.UseSession();

            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
