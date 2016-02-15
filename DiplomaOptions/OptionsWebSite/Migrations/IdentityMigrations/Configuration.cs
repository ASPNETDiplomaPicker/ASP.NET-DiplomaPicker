namespace OptionsWebSite.Migrations.IdentityMigrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OptionsWebSite.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\IdentityMigrations";
        }

        protected override void Seed(OptionsWebSite.Models.ApplicationDbContext context)
        {

            var userStore = new UserStore<ApplicationUser>(context);
            var userManger = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(t => t.UserName == "A00111111"))
            {
                var user = new ApplicationUser { UserName = "A00111111", Email = "a@a.a" };
                userManger.Create(user, "P@$$w0rd");

                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Admin" });
                context.SaveChanges();

                userManger.AddToRole(user.Id, "Admin");
            }

            if(!context.Users.Any(t=> t.UserName == "A00222222"))
            {
                var user = new ApplicationUser { UserName = "A00222222", Email = "s@s.s" };
                userManger.Create(user, "P@$$w0rd");

                context.Roles.AddOrUpdate(c => c.Name, new IdentityRole { Name = "Student" });
                context.SaveChanges();

                userManger.AddToRole(user.Id, "Student");
            }

        }
    }
}
