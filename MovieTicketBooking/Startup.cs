using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MovieTicketBooking.Data;
using MovieTicketBooking.Helpers;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "CorsPolicy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            services.AddResponseCaching();
            // services.AddResponseCompression(options =>            // {
            //     options.Providers.Add<BrotliCompressionProvider>();
            //     options.Providers.Add<GzipCompressionProvider>();
            //     options.MimeTypes =
            //         ResponseCompressionDefaults.MimeTypes.Concat(
            //             new[] { "image/svg+xml" });
            // });
            services.AddResponseCompression();
            services.Configure<BrotliCompressionProviderOptions>(options => {
                options.Level = CompressionLevel.Fastest;
            });
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            services.AddCors(options => {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder => {
                        builder.WithOrigins(
                        
                                "http://localhost:3000")
                            .WithExposedHeaders("X-Total-Quiz")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            //  configure FluentValidation
            //services.AddMvc(opt => { opt.Filters.Add(typeof(ValidatorActionFilter)); })
                //   .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>())
                //.SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // jwt authentication
            //services.AddTokenAuthentication(Configuration);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerDocumentation();
            services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
            services.AddControllers();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            // added Dependency Injections
            services.AddDependencyService();
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseBlockingDetection();
          app.UseResponseCompression();
            app.Use((context, next) =>            {
                context.Response.Headers.Remove("Server");
            return next();
        });
            //app.UseHttpCacheHeaders();
            app.UseCors(MyAllowSpecificOrigins);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsProduction() || env.IsStaging() || env.IsEnvironment("Staging_2"))
            {
                app.UseExceptionHandler("/Error");
            }
            app.ConfigureExceptionHandler();
            app.ConfigureCustomExceptionMiddleware();
            app.UseHttpsRedirection();
            // Enable middleware to serve generated Swagger as a JSON endpoint.

            app.UseSwagger();

                app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
            app.UseSwaggerDocumentation();
            app.UseRouting();
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.RollingFile(Path.Combine(env.ContentRootPath, "Log/", "log-{Date}.txt"))
                .CreateLogger();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
        }
    }
}
