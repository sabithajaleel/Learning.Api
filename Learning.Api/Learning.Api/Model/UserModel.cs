using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Api.Model
{
    public class UserModel
    {
        /// <summary>
        /// The Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The first name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The list of roles associated with the user
        /// </summary>
        public IList<string> Roles { get; set; }

        /// <summary>
        /// The user Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The list of claims associated with the user
        /// </summary>
        public IList<ClaimModel> Claims { get; set; }

        /// <summary>
        /// Says whether the user has been deactivated
        /// </summary>
        public bool Deactivated { get; set; }

        /// <summary>
        /// auto generated password
        /// </summary>
        public bool IsTemporaryPassword { get; set; }

        [NotMapped]
        public string OldPassword { get; set; }

        [NotMapped]
        public string ConfirmPassword { get; set; }
    }
}
