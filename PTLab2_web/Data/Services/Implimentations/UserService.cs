using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using PTLab2_web.Data.Models;
using PTLab2_web.Data.Services.Interfaces;
using System.Reflection.Metadata;

namespace PTLab2_web.Data.Services.Implimentations
{
    public class UserService : IUserService
    {
        private readonly ILocalStorageService _localStorageService;

        protected readonly string _userKey = "user";

        public UserService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task DeleteUser()
        {
            await _localStorageService.RemoveItemAsync(_userKey);
        }

        public async Task<User> GetUser()
        {
            if (await _localStorageService.ContainKeyAsync(_userKey))
            {
                return await _localStorageService.GetItemAsync<User>(_userKey);
            }

            return null;
        }

        public async Task SaveUser(User user)
        {
            await _localStorageService.SetItemAsync(_userKey, user);
        }
    }
}
