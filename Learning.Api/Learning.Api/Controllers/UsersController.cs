using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Learning.Api.Repositories;
using Learning.Api.Model;
using Microsoft.AspNetCore.Authorization;

namespace Learning.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Retrieves all users in the system
        /// </summary>
        /// <returns>Returns the list of users</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await this.userRepository.GetAllUsers();

            return this.Ok(users);
        }

        /// <summary>
        /// Retrieves the user requested
        /// </summary>
        /// <param name="id">The Id of the user to retrieve</param>
        /// <returns>Returns the user with the supplied Id</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await this.userRepository.GetUser(id);

            return this.Ok(user);
        }

        /// <summary>
        /// Deactivates a user making the user not able to login or access anything
        /// </summary>
        /// <param name="id">The Id of the user to deactivate</param>
        /// <returns>Result based on the sucess of the operation</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Deactivate(string id)
        {
            await this.userRepository.DeactivateUser(id);

            return this.Ok();
        }

        /// <summary>
        /// Deactivates a user making the user not able to login or access anything
        /// </summary>
        /// <param name="deactivateUserModel">The model containing the user Id and whether to activate or deactivate the user</param>
        /// <returns>Result based on the sucess of the operation</returns>
        [HttpPost]
        public async Task<IActionResult> Activate([FromBody] DeactivateUserModel deactivateUserModel)
        {
            if (deactivateUserModel == null)
            {
                return this.BadRequest();
            }

            if (deactivateUserModel.Deactivate == deactivateUserModel.Activate)
            {
                return this.BadRequest("DeactivateActivateMustBeDifferent");
            }

            if (deactivateUserModel.Deactivate)
            {
                await this.userRepository.DeactivateUser(deactivateUserModel.UserId);
            }
            else
            {
                await this.userRepository.ActivateUser(deactivateUserModel.UserId);
            }

            return this.Ok();
        }
    }
}