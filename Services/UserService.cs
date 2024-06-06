using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using DominicoBus.DataTransfer;

namespace DominicoBus.Services
{
    public class UserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Create(UserDTO user)
        {
            var identityUser = new IdentityUser()
            {
                UserName = user.Username,
                Email = user.Email
            };

            if (user.Password is not null)
            {
                await _userManager.CreateAsync(identityUser, user.Password);
            }

        }

    }
}