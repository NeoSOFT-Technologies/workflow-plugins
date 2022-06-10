using AuthLibrary;
using Elsa.Extensions;
using Elsa.Persistence.MongoDb;
using Elsa.Rebus.RabbitMq;
using Elsa.Server.Activities;
using Elsa.Server.Extensions;
using Elsa.Server.IServices;
using Elsa.Server.Middleware;
using Elsa.Server.Models.Sandbox;
using Elsa.Server.Services;
using HealthChecks.UI.Client;
using Medallion.Threading.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.IO;

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
            services.AddRedis(Configuration.GetConnectionString("Redis")); 
            var redis = ConnectionMultiplexer.Connect(Configuration.GetConnectionString("Redis"));
            // Added AddDataProtection
            services.AddDataProtection()
               .PersistKeysToStackExchangeRedis(redis, "DataProtection-Keys")
               .AddKeyManagementOptions(options =>
               {
                   options.NewKeyLifetime = new TimeSpan(3650, 0, 0, 0);
                   options.AutoGenerateKeys = true;
               });
            services
                .AddElsa(elsa => elsa
                    .UseMongoDbPersistence(options => options.ConnectionString = Configuration.GetConnectionString("ElsaDb"))
                    .UseRabbitMq(Configuration.GetConnectionString("RabbitMq")) // Service Bus Broker
                    .ConfigureDistributedLockProvider(options => options.UseProviderFactory(sp => name =>
                    {
                        var connection = sp.GetRequiredService<IConnectionMultiplexer>();
                        return new RedisDistributedLock(name, connection.GetDatabase());
                    })) // Distributed Lock Provider
                        .UseRedisCacheSignal()  // Redis Cache Signal for Distributed Cache Signal Provider
                    .AddQuartzTemporalActivities()  // Distributed Temporal Services
                    .AddConsoleActivities()
                    .AddHttpActivities(elsaSection.GetSection("Server").Bind)
                    .AddEmailActivities(elsaSection.GetSection("Smtp").Bind)
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

            services.AddHealthcheckExtensionService(Configuration);
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
                IdentityModelEventSource.ShowPII = true;
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

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "Pages")),
                    RequestPath = "/html"
            });

            app.UseHttpActivities();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseAuthMiddleware();

            app.UseEndpoints(endpoints =>
            {
                // Elsa API Endpoints are implemented as regular ASP.NET Core API controllers.
                endpoints.MapControllers();

                // For Dashboard.
                endpoints.MapFallbackToPage("/_Host");
            });

            app.UseEndpoints(endpoints =>
            {
                //adding endpoint of health check for the health check ui in UI format
                endpoints.MapHealthChecks("/healthz", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                //endpoints.MapControllers();
                endpoints.MapControllers().RequireAuthorization();

                //map healthcheck ui endpoing - default is /healthchecks-ui/
                endpoints.MapHealthChecksUI();

            });
        }
    }
}
