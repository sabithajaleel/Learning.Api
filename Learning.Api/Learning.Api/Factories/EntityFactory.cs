using Learning.Api.Model;
using Learning.Api.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Api.Factories
{
    public static class EntityFactory
    {
        public static LearningUser Create(UserModel userModel)
        {
            var learningUser = new LearningUser
            {
                Id = userModel.Email,
                UserName = userModel.Email,
                Email = userModel.Email,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                IsTemporaryPassword = userModel.IsTemporaryPassword
            };

            return learningUser;
        }
    }
}
