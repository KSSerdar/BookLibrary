using Core.Entities;
using Core.Static;
using DAL.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External.Initializer
{
    public class DbSeedInitializer
    {
        public static async Task SeedUserRole(IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var rManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await rManager.RoleExistsAsync(Roles.Admin))
                {
                    await rManager.CreateAsync(new IdentityRole(Roles.Admin));
                }
                if (!await rManager.RoleExistsAsync(Roles.User))
                {
                    await rManager.CreateAsync(new IdentityRole(Roles.User));
                }
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminMail = "admin@bookapi.com";
                var adminUser = await userManager.FindByEmailAsync(adminMail);
                if (adminUser == null)
                {
                    var newAdmin = new User
                    {
                       Name="Admin",
                        Email = adminMail,
                        UserName = "admin",
                        EmailConfirmed = true

                    };
                    await userManager.CreateAsync(newAdmin, "Admin@12!");
                    await userManager.AddToRoleAsync(newAdmin, Roles.Admin);

                }

                string userMail = "user@bookapi.com";
                var appUser = await userManager.FindByEmailAsync(userMail);
                if (appUser == null)
                {
                    var newUser = new User
                    {
                        Name="User",
                        Email = userMail,
                        UserName = "user",
                        EmailConfirmed = true

                    };
                    await userManager.CreateAsync(newUser, "User@12!");
                    await userManager.AddToRoleAsync(newUser, Roles.User);
                }
            }
        }
        public static async Task SeedAuthor(IApplicationBuilder builder)
        {
            // Path to the JSON file
            string jsonFilePath = "Author.json";

            // Read the JSON file
            string jsonData = File.ReadAllText(jsonFilePath);

            // Parse the JSON data
            List<Author> authors = JsonConvert.DeserializeObject<List<Author>>(jsonData);
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var manager = serviceScope.ServiceProvider.GetRequiredService<APIContext>();
                var getCount=manager.Authors.Count();
                if (getCount<=1)
                {
                   await manager.Authors.AddRangeAsync(authors);
                    await manager.SaveChangesAsync();
                    
                }
            }
            
        }
        public static async Task SeedBook(IApplicationBuilder builder)
        {
            string jsonFilePath = "Book.json";

            // Read the JSON file
            string jsonData = File.ReadAllText(jsonFilePath);

            // Parse the JSON data
            List<Book> books = JsonConvert.DeserializeObject<List<Book>>(jsonData);
            using (var serviceScope=builder.ApplicationServices.CreateScope())
            {
                var manager = serviceScope.ServiceProvider.GetRequiredService<APIContext>();
                var getCount=manager.Books.Count();
                if (getCount<=1)
                {
                    await manager.Books.AddRangeAsync(books);
                    await manager.SaveChangesAsync();
                }
            }
        }
    }
}
