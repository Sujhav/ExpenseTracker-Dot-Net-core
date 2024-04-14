using ExpenseTracker.Database;
using ExpenseTracker.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ExpenseTracker
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<ExpenseTrackerContext> (
                Options=> Options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")
                ));

            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddAntiforgery(Options =>
            {
               
            });
            services.AddScoped<CategoryRepository,CategoryRepository> ();
            services.AddScoped<TransactionRepository,TransactionRepository> ();
            services.AddScoped<DashboardRepository,DashboardRepository> ();
            services.AddScoped<DoughnutRepository,DoughnutRepository> ();   
            services.AddScoped<SplineChartRepository,SplineChartRepository> ();
        

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCekx3WmFZfVpgd19CZFZQRmYuP1ZhSXxXdkZhXH9ddHBURGhdVkw=");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


           

            app.UseStaticFiles();


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/sujhav", async context =>
                {
                    await context.Response.WriteAsync("hello from sujhav");
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
               
                endpoints.MapControllerRoute(
                    name:"Default",
                    pattern:"{controller=Dashboard}/{action=Index}/{id?}"
                    );
            });
        }
    }
   
       
        
        
        
    
}
