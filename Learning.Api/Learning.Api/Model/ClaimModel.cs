using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Api.Model
{
    public class ClaimModel
    {
        /// <summary>
        /// The database identifier of the claim
        /// </summary>
        public string Id { get; set; }

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
