using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Learning.Api.Contracts;
using Learning.Api.Model;
using Learning.Api.Repositories.Entities;
using Learning.Api.Factories;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Learning.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IGenericRepository<LearningUser> userRepository;
        private readonly IGenericRepository<LearningClaim> claimsRepository;
        private readonly IGenericRepository<LearningRole> rolesRepository;
        private LearningUserManager userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class. 
        /// </summary>
        /// <param name="userRepository">The table handler for the users</param>
        /// <param name="claimsRepository">The table handler handling claims</param>
        /// <param name="rolesRepository">The table handler handling roles</param>
        public UserRepository(IGenericRepository<LearningUser> userRepository, IGenericRepository<LearningClaim> claimsRepository,
            IGenericRepository<LearningRole> rolesRepository)
        {
            this.userRepository = userRepository;
            this.claimsRepository = claimsRepository;
            this.rolesRepository = rolesRepository;
        }

        private LearningUserManager UserManager
        {
            get
            {
                if (this.userManager == null)
                {
                    this.userManager = LearningUserManager.Create(this.userRepository);
                }

                return this.userManager;
            }
        }

        /// <summary>
        /// Registers a new user in the system
        /// </summary>
        /// <param name="userModel">The user model to register</param>
        /// <param name="password">The password to associate with the user</param>
        /// <returns>Success if the user was registered, otherwise failure</returns>
        public async Task<Response> RegisterUser(UserModel userModel, string password)
        {
            var learningUser = EntityFactory.Create(userModel);

            var hashedPassword = this.UserManager.PasswordHasher.HashPassword(learningUser, password);

            var identityResult = await this.UserManager.CreateAsync(learningUser, password);

            return Response.FromIdentityResult(identityResult);
        }

        /// <summary>
        /// Gets all roles associated with a user
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <returns>A list of role names</returns>
        public async Task<IList<string>> GetRolesByUser(string userId)
        {
            if (await this.UserDoesNotExist(userId))
            {
                return new string[0];
            }

            var learningUser = await this.UserManager.FindByIdAsync(userId);

            var roleModelList = await this.UserManager.GetRolesAsync(learningUser);

            return roleModelList;
        }

        /// <summary>
        /// Removes a role from a user
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="roleName">The role to remove</param>
        /// <returns>Success if the role was removed, otherwise failure</returns>
        public async Task<Response> RemoveRoleFromUser(string userId, string roleName)
        {
            if (await this.UserDoesNotExist(userId))
            {
                return Response.Failed("UserDoesNotExist");
            }

            var learningUser = await this.UserManager.FindByIdAsync(userId);

            var identityResult = await this.UserManager.RemoveFromRoleAsync(learningUser, roleName);

            return Response.FromIdentityResult(identityResult);
        }

        /// <summary>
        /// Adds a role to a user
        /// </summary>
        /// <param name="userId">The user Id</param>
        /// <param name="roleName">The role to add</param>
        /// <returns>Success if the role was added, otherwise failure</returns>
        public async Task<Response> AddRoleToUser(string userId, string roleName)
        {
            if (await this.UserDoesNotExist(userId))
            {
                return Response.Failed("Error_UserDoesNotExist");
            }

            if (await this.RoleDoesNotExist(roleName))
            {
                return Response.Failed("Error_RoleDoesNotExist");
            }

            var learningUser = await this.UserManager.FindByIdAsync(userId);

            var identityResult = await this.userManager.AddToRoleAsync(learningUser, roleName);

            return Response.FromIdentityResult(identityResult);
        }

        /// <summary>
        /// Gets all claims associated with a user
        /// </summary>
        /// <param name="userId">The user</param>
        /// <returns>A list of claims</returns>
        public async Task<IList<ClaimModel>> GetClaimsByUser(string userId)
        {
            if (await this.UserDoesNotExist(userId))
            {
                return new ClaimModel[0];
            }

            var learningUser = await this.UserManager.FindByIdAsync(userId);

            var claimsList = await this.UserManager.GetClaimsAsync(learningUser);

            return claimsList.Select(ModelFactory.Create).ToList();
        }

        /// <summary>
        /// Removes a claim from a user
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="claimId">The claim Id</param>
        /// <returns>Success if the claim was removed, otherwise failure</returns>
        public async Task<Response> RemoveClaimFromUser(string userId, string claimId)
        {
            var learningClaim = await this.claimsRepository.GetAsync(claimId);

            if (learningClaim == null)
            {
                return Response.Failed("Error_ClaimDoesNotExist");
            }

            return await this.RemoveClaimFromUser(userId, learningClaim.Type, learningClaim.Value);
        }

        /// <summary>
        /// Removes a claim from a user
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="claimType">The type of claim</param>
        /// <param name="claimValue">The value of the claim</param>
        /// <returns>Success if the claim was removed, otherwise failure</returns>
        public async Task<Response> RemoveClaimFromUser(string userId, string claimType, string claimValue)
        {
            if (await this.UserDoesNotExist(userId))
            {
                return Response.Failed("Error_UserDoesNotExist");
            }

            var learningUser = await this.UserManager.FindByIdAsync(userId);

            var identityResult = await this.UserManager.RemoveClaimAsync(learningUser, new Claim(claimType, claimValue));

            return Response.FromIdentityResult(identityResult);
        }

        /// <summary>
        /// Adds a claim to a user
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="claimId">The claim Id</param>
        /// <returns>Success if the claim was added, otherwise failure</returns>
        public async Task<Response> AddClaimToUser(string userId, string claimId)
        {
            var learningClaim = await this.claimsRepository.GetAsync(claimId);

            if (learningClaim == null)
            {
                return Response.Failed("Error_ClaimDoesNotExist");
            }

            return await this.AddClaimToUser(userId, learningClaim.Type, learningClaim.Value);
        }

        /// <summary>
        /// Adds a claim from a user
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="claimType">The type of claim</param>
        /// <param name="claimValue">The value of the claim</param>
        /// <returns>Success if the claim was added, otherwise failure</returns>
        public async Task<Response> AddClaimToUser(string userId, string claimType, string claimValue)
        {
            if (await this.UserDoesNotExist(userId))
            {
                return Response.Failed("Error_UserDoesNotExist");
            }

            var learningUser = await this.UserManager.FindByIdAsync(userId);

            if ((await this.UserManager.GetClaimsAsync(learningUser)).Any(c => c.Type == claimType && c.Value == claimValue))
            {
                return Response.Failed("Error_UserAlreadyHasClaim");
            }

            var identityResult = await this.userManager.AddClaimAsync(learningUser, new Claim(claimType, claimValue));

            return Response.FromIdentityResult(identityResult);
        }

        /// <summary>
        /// Gets all basic userdata
        /// </summary>
        /// <returns>A list of user information</returns>
        public async Task<IList<SimpleUserModel>> GetAllUsers()
        {
            var result = await this.userRepository.Query().ToListAsync();

            return result.Select(ModelFactory.Create).ToList();
        }

        /// <summary>
        /// Gets a specific user
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <returns>A basic user model</returns>
        public async Task<SimpleUserModel> GetUser(string id)
        {
            var result = await this.UserManager.FindByIdAsync(id);

            return ModelFactory.Create(result);
        }

        /// <summary>
        /// Gets user details
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <returns>The user model containing user details</returns>
        public async Task<UserModel> GetUserDetails(string id)
        {
            var result = await this.UserManager.FindByIdAsync(id);

            return await this.CreateUserModel(result);
        }

        /// <summary>
        /// Sets all roles for a user
        /// </summary>
        /// <param name="updateUserRolesModel">The model containing all user roles</param>
        /// <returns>Success if the roles were updated, otherwise failure</returns>
        public async Task<Response> SetUserRoles(UpdateUserRolesModel updateUserRolesModel)
        {
            var userRoles = await this.GetRolesByUser(updateUserRolesModel.UserId);

            var rolesToRemove = userRoles.Where(ur => updateUserRolesModel.Roles.Contains(ur) == false);
            var rolesToAdd = updateUserRolesModel.Roles.Where(ur => userRoles.Contains(ur) == false);

            foreach (var userRole in rolesToRemove)
            {
                var response = await this.RemoveRoleFromUser(updateUserRolesModel.UserId, userRole);

                if (response.Successful == false)
                {
                    return response;
                }
            }

            foreach (var userRole in rolesToAdd)
            {
                var response = await this.AddRoleToUser(updateUserRolesModel.UserId, userRole);

                if (response.Successful == false)
                {
                    return response;
                }
            }

            return Response.Success();
        }

        /// <summary>
        /// Sets all claims for a user
        /// </summary>
        /// <param name="updateUserClaimsModel">The model containing all user claims</param>
        /// <returns>Success if the claims were updated, otherwise failure</returns>
        public async Task<Response> SetUserClaims(UpdateUserClaimsModel updateUserClaimsModel)
        {
            var response = await this.RemoveClaimsOfType(updateUserClaimsModel.UserId, updateUserClaimsModel.ClaimType);

            if (response.Successful == false)
            {
                return response;
            }

            foreach (var value in updateUserClaimsModel.Values)
            {
                response = await this.AddClaimToUser(updateUserClaimsModel.UserId, updateUserClaimsModel.ClaimType, value);

                if (response.Successful == false)
                {
                    return response;
                }
            }

            return Response.Success();
        }

        /// <summary>
        /// Deactivates a user account
        /// </summary>
        /// <param name="userId">The user Id to deactivate</param>
        /// <returns>The result containing success or failure</returns>
        public async Task<Response> DeactivateUser(string userId)
        {
            var learningUser = await this.UserManager.FindByIdAsync(userId);

            if (learningUser == null)
            {
                return Response.Failed("Error_UserDoesNotExist");
            }

            if (learningUser.Deactivated)
            {
                return Response.Failed("Error_UserAlreadyDeactivated");
            }

            learningUser.Deactivated = true;
            var identityResult = await this.UserManager.UpdateAsync(learningUser);

            return Response.FromIdentityResult(identityResult);
        }

        /// <summary>
        /// Activates a user account
        /// </summary>
        /// <param name="userId">The user Id to activate</param>
        /// <returns>The result containing success or failure</returns>
        public async Task<Response> ActivateUser(string userId)
        {
            var learningUser = await this.UserManager.FindByIdAsync(userId);

            if (learningUser == null)
            {
                return Response.Failed("Error_UserDoesNotExist");
            }

            if (learningUser.Deactivated == false)
            {
                return Response.Failed("Error_UserAlreadyActive");
            }

            learningUser.Deactivated = false;
            var identityResult = await this.UserManager.UpdateAsync(learningUser);

            return Response.FromIdentityResult(identityResult);
        }

        /// <summary>
        /// Gets a specific user by email address
        /// </summary>
        /// <param name="email">The id of the user</param>
        /// <returns>A basic user model</returns>
        public async Task<UserModel> GetUserByEmail(string email)
        {
            var result = await this.UserManager.FindByEmailAsync(email);

            if (result != null) return await this.CreateUserModel(result);
            return null;
        }

        /// <summary>
        /// Gets a specific user by email address
        /// </summary>
        /// <param name="model">The id of the user</param>
        /// <returns>A basic user model</returns>
        public async Task<Response> ResetUserPassword(UserModel model)
        {
            var learningUser = await this.UserManager.FindByIdAsync(model.Id);

            var token = await this.UserManager.GeneratePasswordResetTokenAsync(learningUser);
            var result = await this.UserManager.ResetPasswordAsync(learningUser, token, model.Password);

            model.IsTemporaryPassword = true;
            await this.UpdateUserProfile(model);

            if (result.Succeeded) return Response.FromIdentityResult(result);
            return null;
        }

        /// <summary>
        /// Gets a specific user by email address
        /// </summary>
        /// <param name="model">The id of the user</param>
        /// <returns>A basic user model</returns>
        public async Task<Response> ChangeUserPassword(UserModel model)
        {
            var learningUser = await this.UserManager.FindByIdAsync(model.Id);

            var result = await this.UserManager.ChangePasswordAsync(learningUser, model.OldPassword, model.ConfirmPassword);
            model.IsTemporaryPassword = false;
            await this.UpdateUserProfile(model);
            if (result.Succeeded) return Response.FromIdentityResult(result);
            return null;
        }

        /// <summary>
        /// Gets a specific user by email address
        /// </summary>
        /// <param name="model">The id of the user</param>
        /// <returns>A basic user model</returns>
        private async Task<Response> UpdateUserProfile(UserModel model)
        {
            var learningUser = await this.UserManager.FindByIdAsync(model.Id);
            learningUser.IsTemporaryPassword = model.IsTemporaryPassword;
            var result = await this.UserManager.UpdateAsync(learningUser);
            return Response.FromIdentityResult(result);
        }

        private async Task<Response> RemoveClaimsOfType(string userId, string claimType)
        {
            var learningUser = await this.UserManager.FindByIdAsync(userId);

            var claims = await this.UserManager.GetClaimsAsync(learningUser);

            foreach (var claim in claims.Where(c => c.Type == claimType))
            {
                var identityResult = await this.UserManager.RemoveClaimAsync(learningUser, claim);

                if (identityResult.Succeeded == false)
                {
                    return Response.FromIdentityResult(identityResult);
                }
            }

            return Response.Success();
        }

        private async Task<UserModel> CreateUserModel(LearningUser learningUser)
        {
            var userModel = new UserModel
            {
                Id = learningUser.Id,
                Email = learningUser.Email,
                FirstName = learningUser.FirstName,
                LastName = learningUser.LastName,
                UserName = learningUser.UserName,
                Claims = await GetClaimsByUser(learningUser.Id),
                Roles = await this.GetRolesByUser(learningUser.Id),
                Deactivated = learningUser.Deactivated,
                IsTemporaryPassword = learningUser.IsTemporaryPassword,
                Password = learningUser.PasswordHash
            };

            return userModel;
        }

        private static List<ClaimModel> GetClaimsByUser(LearningUser learningUser)
        {
            if (learningUser.Claims == null)
            {
                return new List<ClaimModel>();
            }

            return learningUser.Claims.Select(
                c => new ClaimModel { Type = c.Type, Value = c.Value })
                .ToList();
        }

        private async Task<bool> RoleDoesNotExist(string roleName)
        {
            var roles = await this.rolesRepository.Query().ToListAsync();

            return roles.Any(r => r.Name == roleName) == false;
        }

        private async Task<bool> UserDoesNotExist(string userId)
        {
            return await this.UserManager.FindByIdAsync(userId) == null;
        }
    }
}
