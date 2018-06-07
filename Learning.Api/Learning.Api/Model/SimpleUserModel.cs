using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Api.Model
{
    public class SimpleUserModel
    {
        /// <summary>
        /// The Id of the user
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The Email of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The first name of the user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Whether the user is deactivated
        /// </summary>
        public bool Deactivated { get; set; }

        /// <summary>
        /// Whether autho generated password
        /// </summary>
        public bool IsTemporaryPassword { get; set; }
    }
}
