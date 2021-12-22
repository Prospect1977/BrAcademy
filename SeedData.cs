﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy
{
    public static class SeedData
    {
        public static void Seed(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
           // SeedUsers(userManager);
        }

        //private static void SeedUsers(UserManager<IdentityUser> userManager)
        //{
        //    var users = userManager.GetUsersInRoleAsync("Employee").Result;

        //    if (userManager.FindByNameAsync("admin@localhost.com").Result == null)
        //    {
        //        var user = new IdentityUser
        //        {
        //            UserName = "admin@localhost.com",
        //            Email = "admin@localhost.com"
        //        };
        //        var result = userManager.CreateAsync(user, "P@ssword1").Result;
        //        if (result.Succeeded)
        //        {
        //            userManager.AddToRoleAsync(user, "Administrator").Wait();
        //        }
        //    }
        //}

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Admin"
                };
                var result = roleManager.CreateAsync(role).Result;
            }

            //if (!roleManager.RoleExistsAsync("Employee").Result)
            //{
            //    var role = new IdentityRole
            //    {
            //        Name = "Employee"
            //    };
            //    var result = roleManager.CreateAsync(role).Result;
            //}
        }
    }
}

