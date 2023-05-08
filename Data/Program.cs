using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Data;

public class Program
{
    public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuild => webBuild.UseStartup<Startup>());
}

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        //var connectionString = $"Server=caffeine-tracker.database.windows.net;Database=Caffeine1;Trusted_Connection=True;MultipleActiveResultSets=true";
        var connectionString = $"Server=tcp:caffeine-tracker.database.windows.net,1433;Initial Catalog=Caffeine1;Persist Security Info=False;User ID=caffeineadmin;Password=Password!12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        services.AddDbContextFactory<ApplicationDbContext>();

    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) { }
}
