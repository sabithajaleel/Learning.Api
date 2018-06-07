using Learning.Api.Contracts;
using Learning.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Api.Repositories
{
    /// <summary>
    /// The user repository handling the database operations related to a user
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets all roles associated with a user
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <returns>A list of role names</returns>
        Task<IList<string>> GetRolesByUser(string userId);

        /// <summary>
        /// Registers a new user in the system
        /// </summary>
        /// <param name="userModel">The user model to register</param>
        /// <param name="password">The password to associate with the user</param>
        /// <returns>Success if the user was registered, otherwise failure</returns>
        Task<Response> RegisterUser(UserModel userModel, string password);

        /// <summary>
        /// Removes a role from a user
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="roleName">The role to remove</param>
        /// <returns>Success if the role was removed, otherwise failure</returns>
        Task<Response> RemoveRoleFromUser(string userId, string roleName);

        /// <summary>
        /// Adds a role to a user
        /// </summary>
        /// <param name="userId">The user Id</param>
        /// <param name="roleName">The role to add</param>
        /// <returns>Success if the role was added, otherwise failure</returns>
        Task<Response> AddRoleToUser(string userId, string roleName);

        /// <summary>
        /// Gets all claims associated with a user
        /// </summary>
        /// <param name="userId">The user</param>
        /// <returns>A list of claims</returns>
        Task<IList<ClaimModel>> GetClaimsByUser(string userId);

        /// <summary>
        /// Removes a claim from a user
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="claimId">The claim Id</param>
        /// <returns>Success if the claim was removed, otherwise failure</returns>
        Task<Response> RemoveClaimFromUser(string userId, string claimId);

        /// <summary>
        /// Removes a claim from a user
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="claimType">The type of claim</param>
        /// <param name="claimValue">The value of the claim</param>
        /// <returns>Success if the claim was removed, otherwise failure</returns>
        Task<Response> RemoveClaimFromUser(string userId, string claimType, string claimValue);

        /// <summary>
        /// Adds a claim to a user
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="claimId">The claim Id</param>
        /// <returns>Success if the claim was added, otherwise failure</returns>
        Task<Response> AddClaimToUser(string userId, string claimId);

        /// <summary>
        /// Adds a claim from a user
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="claimType">The type of claim</param>
        /// <param name="claimValue">The value of the claim</param>
        /// <returns>Success if the claim was added, otherwise failure</returns>
        Task<Response> AddClaimToUser(string userId, string claimType, string claimValue);

        /// <summary>
        /// Gets all basic userdata
        /// </summary>
        /// <returns>A list of user information</returns>
        Task<IList<SimpleUserModel>> GetAllUsers();

        /// <summary>
        /// Gets a specific user
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <returns>A basic user model</returns>
        Task<SimpleUserModel> GetUser(string id);

        /// <summary>
        /// Gets user details
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <returns>The user model containing user details</returns>
        Task<UserModel> GetUserDetails(string id);

        /// <summary>
        /// Sets all roles for a user
        /// </summary>
        /// <param name="updateUserRolesModel">The model containing all user roles</param>
        /// <returns>Success if the roles were updated, otherwise failure</returns>
        Task<Response> SetUserRoles(UpdateUserRolesModel updateUserRolesModel);

        /// <summary>
        /// Sets all claims for a user
        /// </summary>
        /// <param name="updateUserClaimsModel">The model containing all user claims</param>
        /// <returns>Success if the claims were updated, otherwise failure</returns>
        Task<Response> SetUserClaims(UpdateUserClaimsModel updateUserClaimsModel);

        /// <summary>
        /// Deactivates a user account
        /// </summary>
        /// <param name="userId">The user Id to deactivate</param>
        /// <returns>The result containing success or failure</returns>
        Task<Response> DeactivateUser(string userId);

        /// <summary>
        /// Activates a user account
        /// </summary>
        /// <param name="userId">The user Id to activate</param>
        /// <returns>The result containing success or failure</returns>
        Task<Response> ActivateUser(string userId);

        /// <summary>
        /// Activates a user account
        /// </summary>
        /// <param name="email">The user email address</param>
        /// <returns>The result containing success (SimpleUserModel or failure(null)</returns>
        Task<UserModel> GetUserByEmail(string email);

        /// <summary>
        /// Activates a user account
        /// </summary>
        /// <param name="model">The SimpleUserModel</param>
        /// <returns>The result containing success or failure</returns>
        Task<Response> ResetUserPassword(UserModel model);

        /// <summary>
        /// Change User Password
        /// </summary>
        /// <param name="model">The ChangePasswordModel</param>
        /// <returns>The result containing success or failure</returns>
        Task<Response> ChangeUserPassword(UserModel model);
    }
}
