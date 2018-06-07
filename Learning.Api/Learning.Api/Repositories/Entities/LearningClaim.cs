using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Api.Repositories.Entities
{
    public class LearningClaim : BaseEntity
    {
        /// <summary>
        /// The type of claim described in ClaimTypeValues
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The name of the claim
        /// </summary>
        public string Value { get; set; }
    }
}
