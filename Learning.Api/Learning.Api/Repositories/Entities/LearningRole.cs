using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Api.Repositories.Entities
{
    public class LearningRole : BaseEntity, IRole
    {
        /// <summary>
        /// Name of the role
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the role
        /// </summary>
        public string Description { get; set; }
    }
}
