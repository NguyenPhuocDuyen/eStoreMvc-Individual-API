using eStoreClient.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace eStoreClient.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db,
            UserManager<IdentityUser> userManager)
        {
            _db = db;
            //_roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }

            //if (!_roleManager.RoleExistsAsync(Role.Admin).GetAwaiter().GetResult())
            //{
            //    _roleManager.CreateAsync(new IdentityRole(Role.Admin)).GetAwaiter().GetResult();
            //    _roleManager.CreateAsync(new IdentityRole(Role.Customer)).GetAwaiter().GetResult();
            if (!_db.Users.Any())
            {
                _userManager.CreateAsync(new IdentityUser
                {
                    UserName = "admin@master.com",
                    Email = "admin@master.com",
                    EmailConfirmed = true,
                }, "Admin123*").GetAwaiter().GetResult();

                //IdentityUser user = _db.Users.FirstOrDefault(u => u.Email == "admin@master.com");

                //_userManager.AddToRoleAsync(user, Role.Admin).GetAwaiter().GetResult();
            }
            //}
        }
    }
}
