using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Shop.DAL.Core;
using Shop.Business.Implementation;
using Shop.Business.IServices;
using Shop.DAL.Core.Entities;
using Shop.Business;
using Shop.DAL.Core.Repositories.Implementation;
using Shop.DAL.Core.Repositories.Interfaces;
using Shop.DAL.Core.UnitOfWork;

namespace Shop.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ShopContext>()
                .AddDefaultTokenProviders();
            services.AddDbContext<ShopContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddAutoMapper(typeof(AutoMap));
            services.AddControllersWithViews();

            services.AddSwaggerGen();

            services.AddScoped((serviceProvider) =>
            {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                return new SmtpClient()
                {
                    Host = config.GetValue<string>("Email:Smtp:Host"),
                    Port = config.GetValue<int>("Email:Smtp:Port"),
                    Credentials = new NetworkCredential(
                        config.GetValue<string>("Email:Smtp:Username"),
                        config.GetValue<string>("Email:Smtp:Password")
                    ),
                    EnableSsl = config.GetValue<bool>("Email:Smtp:EnableSsl"),
                    Timeout = config.GetValue<int>("Email:Smtp:Timeout")
                };
            });
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop.WebAPI v1"));

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
