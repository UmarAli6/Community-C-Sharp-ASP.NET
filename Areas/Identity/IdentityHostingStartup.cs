using Community.Data;
using Community.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Community.Areas.Identity.IdentityHostingStartup))]
namespace Community.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<CommunityUserContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("CommunityContextConnection")));

                services.AddDefaultIdentity<CommunityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<CommunityUserContext>();
            });
        }
    }
}