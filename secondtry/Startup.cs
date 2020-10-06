using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Elasticsearch;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace secondtry
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
            Log.Logger = new LoggerConfiguration()

            .ReadFrom.Configuration(Configuration)

            .Enrich.FromLogContext()

            .WriteTo.EventCollector("http://localhost:8088", "2b158431-bfde-4711-b727-3e6ec7500c62")

            .WriteTo.Console(new ElasticsearchJsonFormatter())
            

            .CreateLogger();


            services.AddSingleton(Log.Logger);
            services.AddAuthorization();
            services.AddControllers();
            services.AddLogging(configure => configure.AddSerilog());

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
