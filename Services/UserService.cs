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
                var result = await _userManager.CreateAsync(identityUser, user.Password);
                System.Console.WriteLine(result);
            }

        }

        public IEnumerable<UserResult> Search(string search)
        {
            return _userManager.Users
                .Where(user => user.NormalizedUserName.Contains(search.ToUpper()) || user.NormalizedEmail.Contains(search.ToUpper()))
                .Select(user => new UserResult(user.UserName, user.Email, user.Id))
                .ToList();
        }

    }
}