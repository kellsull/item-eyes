using System;
using ItemEyes.Areas.Identity.Data;
using ItemEyes.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ItemEyes.Areas.Identity.IdentityHostingStartup))]
namespace ItemEyes.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ItemEyesDbContext>(options =>
                     options.UseInMemoryDatabase("ItemEyesDB"));

            });
        }
    }
}