using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using EmployeeManagement.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace EmployeeManagement
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddDbContextPool<AppDbContext>(options=>options.UseSqlServer(config.GetConnectionString("EmployeeDbConection")));
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder().
                                    RequireAuthenticatedUser().
                                    Build();
                options.Filters.Add(new AuthorizeFilter(policy));
                options.EnableEndpointRouting = false;
            }
            );
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores< AppDbContext>();
            services.Configure<IdentityOptions>(o =>
                    {
                        o.Password.RequireLowercase = false;
                        o.Password.RequireNonAlphanumeric = false;
                        o.Password.RequiredLength = 5;
                        o.SignIn.RequireConfirmedEmail=true;
                    }
                );
            services.AddAuthorization(options =>
              {
                  options.AddPolicy("DeleteRolePolicy", policy => policy.RequireClaim("Delete Role","true"));
                  // options.AddPolicy("EditRolePolicy", policy => policy.RequireAssertion(context =>
                  //                                       context.User.IsInRole("Admin") &&
                  //                                       context.User.HasClaim(claim => claim.Type == "Edit Role"&&claim.Value=="true") ||
                  //                                       context.User.IsInRole("SuperAdmin")
                  // )) ;
                  options.AddPolicy("EditRolePolicy", policy => policy.AddRequirements(new ManageAdminRolesAndClaimRequirement()));
                  options.AddPolicy("AdminRolePolicy", policy => policy.RequireRole("Admin", "true"));
              }          
                );
            services.AddAuthentication()
                .AddGoogle(options=>
                {
                    options.ClientId = "524423355530-m0ssoaelprpr79jtdf349qbtag7eq0io.apps.googleusercontent.com";
                    options.ClientSecret = "Q0UhX6RN8OpF5ylaZIuiHS0O";
                })
                .AddFacebook(options=>
                {
                    options.AppId = "529730280979678";
                    options.AppSecret = "caa5a641c281f8ec057a02f0074e9c22";
                 });
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Administration/AccessDenied");
            });
            services.AddSingleton<IAuthorizationHandler, CanEditOnlyOtherClaimsAndRolesHandler>();
            services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();
            
        }
        public IConfiguration config;
        public Startup(IConfiguration config)
        {
            this.config = config;
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else 
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
           

            //app.UseRouting();
            //app.Use(async (context, next) =>
            //{
                //await context.Response.WriteAsync("azaza");
                //await next();
            //}
                //);
        }
    }
}
