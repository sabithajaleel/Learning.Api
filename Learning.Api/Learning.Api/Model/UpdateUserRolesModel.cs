using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Api.Model
{
    public class UpdateUserRolesModel
    {
        /// <summary>
        /// the user Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The list of roles to set on the user
        /// </summary>
        public IList<string> Roles { get; set; }
    }
}
