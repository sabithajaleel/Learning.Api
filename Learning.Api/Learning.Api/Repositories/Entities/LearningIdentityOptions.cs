using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Api.Repositories.Entities
{
    public class LearningIdentityOptions : IOptions<IdentityOptions>
    {
        public IdentityOptions Value => new IdentityOptions()
        {
            Password = new PasswordOptions
            {
                RequireLowercase = true,
                RequireDigit = true
            }
        };
    }

    public class LearningPasswordHasher : IPasswordHasher<LearningUser>
    {
        public string HashPassword(LearningUser user, string password)
        {
            return password;
        }

        public PasswordVerificationResult VerifyHashedPassword(LearningUser user, string hashedPassword, string providedPassword)
        {
            return PasswordVerificationResult.Success;
        }
    }
}
