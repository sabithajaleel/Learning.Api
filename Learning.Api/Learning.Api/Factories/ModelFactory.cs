using Learning.Api.Model;
using Learning.Api.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Learning.Api.Factories
{
    public static class ModelFactory
    {
        public static ClaimModel Create(Claim claim)
        {
            return new ClaimModel { Type = claim.Type, Value = claim.Value };
        }

        public static SimpleUserModel Create(LearningUser learningUser)
        {
            return new SimpleUserModel
            {
                Id = learningUser.Id,
                UserName = learningUser.UserName,
                Email = learningUser.Email,
                FirstName = learningUser.FirstName,
                LastName = learningUser.LastName,
                Deactivated = learningUser.Deactivated,
                IsTemporaryPassword = learningUser.IsTemporaryPassword
            };
        }
    }
}
