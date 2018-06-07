using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Learning.Api.Repositories.Entities
{
    public class LearningUserManager : UserManager<LearningUser>
    {
        public LearningUserManager(IUserStore<LearningUser> store, IOptions<IdentityOptions> optionsAccessor, 
            IPasswordHasher<LearningUser> passwordHasher, IEnumerable<IUserValidator<LearningUser>> userValidators,
            IEnumerable<IPasswordValidator<LearningUser>> passwordValidators, 
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, 
            IServiceProvider services, ILogger<UserManager<LearningUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public static LearningUserManager Create(IGenericRepository<LearningUser> userRepository)
        {
            var manager = new LearningUserManager(new LearningUserStore(userRepository), 
                new LearningIdentityOptions(),
                new LearningPasswordHasher(), null, null,null, null, null, null);

            return manager;
        }
    }
}
