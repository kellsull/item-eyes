using ItemEyes.Areas.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemEyes.Areas.Identity.Data
{
    public static class UserDbInitializer
    {

        public static void Initialize(IServiceProvider services)
        {
            var context = services.GetRequiredService<ItemEyesDbContext>();
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            //This establishes a dummy account for testing purposes

            ItemEyesUser user = new ItemEyesUser();
            Guid guid = new Guid();
            string email = "user@itemeyes.com";
            string password = "userP@55";

            user.Id = guid.ToString();
            user.UserName = email;
            user.Email = email;
            user.NormalizedEmail = email;
            user.EmailConfirmed = true;


            user.SecurityStamp = Guid.NewGuid().ToString();
            var userManager = services.GetRequiredService<UserManager<ItemEyesUser>>();
            var result = userManager.CreateAsync(user, password);

        }
    }
}
