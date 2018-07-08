namespace ProjectV4.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjectV4.myContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        private myContext db = new myContext();

        protected override void Seed(ProjectV4.myContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Users.AddOrUpdate(
                u => u.Id,
                new User
                {
                    Id = 1,
                    Username = "Admin",
                    Password = "12qw!@QW",
                    Role = Role.SuperAdmin
                },
                new User
                {
                    Id = 2,
                    Username = "Tier3",
                    Password = "12qw!@QW",
                    Role = Role.Tier3User
                },
                new User
                {
                    Id = 3,
                    Username = "Tier2",
                    Password = "12qw!@QW",
                    Role = Role.Tier2User
                },
                new User
                {
                    Id = 4,
                    Username = "Tier1",
                    Password = "12qw!@QW",
                    Role = Role.Tier1User
                },
                new User
                {
                    Id = 5,
                    Username = "Tier0",
                    Password = "12qw!@QW",
                    Role = Role.Tier0User
                }
            );
        }
    }
}
