using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;

namespace SiegeTournamentTracker.Web
{
	using Api;
	using Api.MetaData;

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
			services
				//Add the STT API services
				.AddTransient<ITournamentApi, TournamentApi>()
				.AddTransient<IMetaDataService, MetaDataService>()
				.AddTransient<ISiegeService, SiegeService>()
				.AddTransient<IImageCacheService, ImageCacheService>()
				//Add Swagger/Swashbuckle UI / Generation for documentation
				.AddSwaggerGen(c =>
				{
					c.SwaggerDoc("v1", new OpenApiInfo
					{
						Version = "v1",
						Title = "Siege Tournament Tracker",
						Description = "Tracks seige matches for Pro League (PL) and Challenger League (CL)"
					});

					// Set the comments path for the Swagger JSON and UI.
					var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
					var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
					c.IncludeXmlComments(xmlPath);
				})
				//Add logging with serilog
				.AddLogging(_ =>
				{
					var logger = new LoggerConfiguration()
						.MinimumLevel.Debug()
						.WriteTo.Console()
						.WriteTo.File(Path.Combine("logs", "log.txt"), rollingInterval: RollingInterval.Day)
						.CreateLogger();

					_
						.AddSerilog(logger)
						.SetMinimumLevel(LogLevel.Debug);
				})
				//Add all of our controllers
				.AddControllersWithViews();
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
				app.UseExceptionHandler("/Home/Error");
			}
			//use static files from wwwroot for virtual files
			app.UseStaticFiles();
			//Use MVC and API routing for controllers
			app.UseRouting();

			//Add Swagger/Swashbuckle
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Siege Tournament Tracker");
			});

			//Allow for proxying headers (for use with nginx)
			app.UseForwardedHeaders(new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			});

			app.UseAuthorization();

			//Map the controller end points with the SPA fallback controller
			app.UseEndpoints(endpoints =>
			{
				//The MVC and API controllers
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");

				//The default fallback controller (for SPA routing)
				endpoints.MapFallbackToController("Index", "Home");
			});
		}
	}
}
