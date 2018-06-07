using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Api.Model
{
    public class UpdateUserClaimsModel
    {
        /// <summary>
        /// The user Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The type of claim
        /// </summary>
        public string ClaimType { get; set; }

        /// <summary>
        /// The list of values for the claim type
        /// </summary>
        public IList<string> Values { get; set; }
    }
}
