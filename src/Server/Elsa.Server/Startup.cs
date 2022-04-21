using Elsa.Extensions;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.SqlServer;
using Elsa.Rebus.RabbitMq;
using Elsa.Server.Activities;
using Elsa.Server.IServices;
using Elsa.Server.Middleware;
using Elsa.Server.Models.Sandbox;
using Elsa.Server.Services;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Rebus.Config;
using AuthLibrary;

namespace Elsa.Server
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
            services.Configure<SandboxSettings>(Configuration.GetSection("SandboxSettings"));
            services.AddTransient<ISandboxService, SandboxService>();
            var elsaSection = Configuration.GetSection("Elsa");

            // Elsa services.
            services.AddRedis("localhost:6379,abortConnect=false");
            services
                .AddElsa(elsa => elsa
                    .UseEntityFrameworkPersistence(ef => ef.UseSqlServer(Configuration.GetConnectionString("ElsaDb")))
                    .UseRabbitMq("amqp://localhost:5672")
                    .ConfigureDistributedLockProvider(options => options.UseSqlServerLockProvider(Configuration.GetConnectionString("ElsaDb")))
                    .UseRedisCacheSignal()
                    .AddHangfireTemporalActivities(hangfire => hangfire.UseSqlServerStorage(Configuration.GetConnectionString("ElsaDb")))

                    .AddConsoleActivities()
                    .AddHttpActivities(elsaSection.GetSection("Server").Bind)
                    .AddEmailActivities(elsaSection.GetSection("Smtp").Bind)
                    .AddQuartzTemporalActivities()
                    .AddWorkflowsFrom<Startup>()
                    .AddActivity<PanVerification>()
                    .AddActivity<BankAccountVerification>()
                );

            services.AddKeyCloakServices(Configuration);
            // Elsa API endpoints.
            services.AddElsaApiEndpoints();
            services.AddControllers();
            // For Dashboard.
            services.AddRazorPages();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Authority = Configuration["Jwt:Authority"];
                    options.Audience = Configuration["Jwt:Audience"];
                    options.IncludeErrorDetails = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        //ValidAudiences = new[] { "master-realm", "account" },
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["Jwt:Authority"],
                        ValidateLifetime = true,
                        RequireExpirationTime = true
                    };
                });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseMiddlewareExtensions();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseHttpActivities();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAuthMiddleware();
            app.UseEndpoints(endpoints =>
            {
                // Elsa API Endpoints are implemented as regular ASP.NET Core API controllers.
                endpoints.MapControllers().RequireAuthorization();
                //endpoints.MapControllers();
                // For Dashboard.
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
