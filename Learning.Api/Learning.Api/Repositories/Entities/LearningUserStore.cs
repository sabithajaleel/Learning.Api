using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Claims;

namespace Learning.Api.Repositories.Entities
{
    public class LearningUserStore : IUserStore<LearningUser>,
                                    IUserPasswordStore<LearningUser>,
                                    IUserClaimStore<LearningUser>,
                                    IUserRoleStore<LearningUser>,
                                    IUserEmailStore<LearningUser>
    {
        private readonly IGenericRepository<LearningUser> userRepository;

        public LearningUserStore(IGenericRepository<LearningUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task AddClaimsAsync(LearningUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.Claims.ToList().AddRange(claims));
        }

        public async Task AddToRoleAsync(LearningUser user, string roleName, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.Roles.Add(roleName));
        }

        public async Task<IdentityResult> CreateAsync(LearningUser user, CancellationToken cancellationToken)
        {
            await this.userRepository.InsertAsync(user);
            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(LearningUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }

        public async Task<LearningUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            var user = await this.userRepository.GetAsync(normalizedEmail);

            return user;
        }

        public async Task<LearningUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await this.userRepository.GetAsync(userId);

            return user;
        }

        public Task<LearningUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Claim>> GetClaimsAsync(LearningUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Claims);
        }

        public Task<string> GetEmailAsync(LearningUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(LearningUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task<string> GetNormalizedEmailAsync(LearningUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(LearningUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(LearningUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public async Task<IList<string>> GetRolesAsync(LearningUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Roles);
        }

        public Task<string> GetUserIdAsync(LearningUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(LearningUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<IList<LearningUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<LearningUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> HasPasswordAsync(LearningUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.PasswordHash != null);
        }

        public async Task<bool> IsInRoleAsync(LearningUser user, string roleName, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Roles.Contains(roleName));
        }

        public Task RemoveClaimsAsync(LearningUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveFromRoleAsync(LearningUser user, string roleName, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.Roles.Remove(roleName));
        }

        public Task ReplaceClaimAsync(LearningUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(LearningUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task SetEmailConfirmedAsync(LearningUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task SetNormalizedEmailAsync(LearningUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.Email = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetNormalizedUserNameAsync(LearningUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.UserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetPasswordHashAsync(LearningUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(LearningUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(LearningUser user, CancellationToken cancellationToken)
        {
            await this.userRepository.UpdateAsync(user);
            return IdentityResult.Success;
        }
    }
}
