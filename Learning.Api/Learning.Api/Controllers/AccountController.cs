using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Learning.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Learning.Api.Model;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Learning.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserRepository userRepository;

        public AccountController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// POST api/Account/Register 
        /// </summary>
        /// <param name="userModel">The user model to register to the system</param>
        /// <returns>The result of the post</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var response = await this.userRepository.RegisterUser(userModel, userModel.Password);

            return this.Ok(response);
        }

        /// <summary>
        /// Gets the user details on the logged in user
        /// </summary>
        /// <returns>The user details</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var principal = this.User as ClaimsPrincipal;

            if (principal == null)
            {
                return this.NotFound();
            }

            var userId = principal.Identity.GetUserId();

            var user = await this.userRepository.GetUserDetails(userId);

            return this.Ok(user);
        }
    }
}
