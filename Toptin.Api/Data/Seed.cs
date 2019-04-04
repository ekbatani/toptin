using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Toptin.Api.Models;

namespace Toptin.Api.Data
{
    public class Seed
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly DataContext _context;
        public Seed(UserManager<User> userManager, RoleManager<Role> roleManager, DataContext context)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void SeedUsers()
        {
            if (!_userManager.Users.Any())
            {
                // var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                // var users = JsonConvert.DeserializeObject<List<User>>(userData);

                var roles = new List<Role>
                {
                    new Role{Name = "Admin"},
                    new Role{Name = "Customer"},
                    new Role{Name = "Store"},
                    new Role{Name = "Kharid"}
                };

                foreach (var role in roles)
                {
                    _roleManager.CreateAsync(role).Wait();
                }

                // foreach (var user in users)
                // {
                //     // user.Photos.SingleOrDefault().IsApproved = true;
                //     _userManager.CreateAsync(user, "password").Wait();
                //     _userManager.AddToRoleAsync(user, "Member").Wait();
                // }

                var adminUser = new User
                {
                    UserName = "09129318706",
                    PhoneNumber = "09129318706"
                };

                IdentityResult result = _userManager.CreateAsync(adminUser, "123456").Result;

                if (result.Succeeded)
                {
                    var admin = _userManager.FindByNameAsync("09129318706").Result;
                    _userManager.AddToRolesAsync(admin, new[] { "Admin" }).Wait();
                }
            }

            if (!_context.Brand.Any())
            {
                string[] brands = { "KIA", "BENZ", "IKCO", "SAIPA", "HONDA", "FERRARI", "AUDI", "JEEP", "JAC", "FIAT", "FORD", "ASTON MARTIN", "LANCIA", "BMW", "Pars Khodro" };

                foreach (string brand in brands)
                {
                    _context.Brand.Add(new Brand{ Title = brand });
                }

                _context.SaveChanges();
            }
        }
    }
}