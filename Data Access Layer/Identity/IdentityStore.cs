using System;
using System.Threading.Tasks;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNet.Identity;

namespace Data_Access_Layer.Identity
{

    public class IdentityStore : IUserStore<User, int>, IUserPasswordStore<User, int>, IUserEmailStore<User, int>
    {
        private readonly IUserRepository _userRepository;

        public IdentityStore(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task CreateAsync(User user)
        {
            return Task.Run(() =>
            {
                _userRepository.Create(user);
            });
        }

        public Task DeleteAsync(User user)
        {
            return Task.Run(() =>
            {
                _userRepository.Delete(user.Id);
            });
        }

        void IDisposable.Dispose()
        {

        }

        public Task<User> FindByIdAsync(int userId)
        {
            return Task.Run(() => _userRepository.Get(userId));
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return Task.Run(() => _userRepository.Get(userName));
        }

        public Task UpdateAsync(User user)
        {
            return Task.Run(() => _userRepository.Update(user));
        }

        public Task SetEmailAsync(User user, string email)
        {
            return Task.Run(() => user.Email = email);
        }

        public Task<string> GetEmailAsync(User user)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user)
        {
            return Task.FromResult(user.ConfirmEmail);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed)
        {
            return Task.FromResult(user.ConfirmEmail = confirmed);
        }

        public Task<User> FindByEmailAsync(string email)
        {
            return Task.FromResult(_userRepository.GetByEmail(email));
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            return Task.Run(() => user.PasswordHash = passwordHash);
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(true);
        }
    }
}
