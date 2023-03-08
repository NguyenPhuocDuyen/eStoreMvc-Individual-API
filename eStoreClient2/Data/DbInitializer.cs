using BusinessObject;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System;
using System.Linq;
using eStoreClient2.Models;

namespace eStoreClient2.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<Member> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db,
            UserManager<Member> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
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
            //    _roleManager.CreateAsync(new IdentityRole(Role.Employee)).GetAwaiter().GetResult();
            //    _roleManager.CreateAsync(new IdentityRole(Role.Customer)).GetAwaiter().GetResult();

            //    _userManager.CreateAsync(new User
            //    {
            //        UserName = "admin@master.com",
            //        Email = "admin@master.com",
            //        EmailConfirmed = true,
            //        Name = "Hn",
            //        DOB = DateTime.Now,
            //    }, "Admin123*").GetAwaiter().GetResult();

            //    User user = _db.User.FirstOrDefault(u => u.Email == "admin@master.com");

            //    _userManager.AddToRoleAsync(user, Role.Admin).GetAwaiter().GetResult();
            //}

            if (_userManager.Users.Count() == 0)
            {
                _userManager.CreateAsync(new Member
                {
                    UserName = "admin@master.com",
                    Email = "admin@master.com",
                    EmailConfirmed = true,
                }, "Admin123*").GetAwaiter().GetResult();

                Member user = _db.Members.FirstOrDefault(u => u.Email == "admin@master.com");

                _userManager.AddToRoleAsync(user, ApplicationRole.Admim).GetAwaiter().GetResult();
            }
        }
    }
}
