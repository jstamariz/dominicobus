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

        public async Task Update(UserDTO user, string Id)
        {
            var userOnDb = await _userManager.FindByIdAsync(Id);
            if (userOnDb is not null)
            {
                userOnDb.Email = user.Email;
                userOnDb.UserName = user.Username;

                await _userManager.UpdateAsync(userOnDb);
            }
        }

        public async Task<UserDTO?> Get(string userId)
        {
            var userOnDb = await _userManager.FindByIdAsync(userId);
            var dumbPassword = Guid.NewGuid().ToString();

            if (userOnDb is not null)
            {
                return new UserDTO()
                {
                    Email = userOnDb.Email,
                    Username = userOnDb.UserName,
                    Password = dumbPassword,
                    ConfirmPassword = dumbPassword
                };
            }

            return default;
        }

        public async Task DeleteAsync(string? userId)
        {
            if (userId is null) return;
            var userOnDb = await _userManager.FindByIdAsync(userId);
            
            if (userOnDb is not null)
            {
                await _userManager.DeleteAsync(userOnDb);
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