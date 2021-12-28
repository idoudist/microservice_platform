using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProduction)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                if (isProduction)
                {
                    ApplyMigrations(serviceScope.ServiceProvider.GetService<AppDbContext>());
                } else
                {
                    SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
                }
                
            }

        }
        private static void SeedData(AppDbContext context)
        {
            if (!context.Platforms.Any())
            {
                Console.WriteLine(" --> Seeding Data ... ");
                context.Platforms.AddRange(
                    new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free"},
                    new Platform() { Name = "Dot Net Core", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Azure", Publisher = "Microsoft", Cost = "Premium" }
                );
                context.SaveChanges();
            } else
            {
                Console.WriteLine(" --> We Already Have Data ");
            }
        }

        private static void ApplyMigrations(AppDbContext context)
        {
            Console.WriteLine("--> Attempting to apply migrations ...");
            try
            {
                context.Database.Migrate();
            } catch (Exception e)
            {
                Console.WriteLine($"--> Error applying Migration : {e.Message}");
            }
            
        }
    }
}
