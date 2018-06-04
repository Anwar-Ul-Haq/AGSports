using AGSports.Contracts;
using AGSports.Models;
using AGSports.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace AGSports
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

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            
             //services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddMvc();
            //azure database first 
            var connection = "";

            services.AddDbContext<AGSportsContext>(options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
             //app.UseResponseCaching();
            app.UseMiddleware<StackifyMiddleware.RequestTracerMiddleware>(); // install nuget pacakge for stackifymiddleware ..https://stackify.com/
            app.UseMvc();
        }
    }
}
