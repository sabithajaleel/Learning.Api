using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Learning.Api.Repositories.Entities
{
    public class LearningUser : BaseEntity, IUser
    {
        private IList<Claim> claims;
        private IList<string> roles;

        /// <summary>
        /// The first name of the user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// States whether the user has been deactivated.
        /// </summary>
        public bool Deactivated { get; set; }

        /// <summary>
        /// The username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The user password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The user email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Number of failed logins for the user
        /// </summary>
        public int FailedLogIns { get; set; }

        /// <summary>
        /// User locked out until...
        /// </summary>
        [NotMapped]
        public DateTimeOffset LockOutEndDate { get; set; }

        /// <summary>
        /// The users roles
        /// </summary>
        [NotMapped]
        public IList<string> Roles
        {
            get
            {
                return this.roles ?? (this.roles = new List<string>());
            }

            set
            {
                this.roles = value;
            }
        }

        /// <summary>
        /// The users claims
        /// </summary>
        [NotMapped]
        public IList<Claim> Claims
        {
            get
            {
                return this.claims ?? (this.claims = new List<Claim>());
            }

            set
            {
                this.claims = value;
            }
        }

        public string LockOutEndDateString
        {
            get
            {
                return this.LockOutEndDate.ToString();
            }

            set
            {
                this.LockOutEndDate = DateTimeOffset.Parse(value);
            }
        }

        public string ClaimString
        {
            get
            {
                return JsonConvert.SerializeObject(this.Claims);
            }

            set
            {
                this.Claims = JsonConvert.DeserializeObject<IList<Claim>>(value);
            }
        }

        public string RolesString
        {
            get
            {
                return JsonConvert.SerializeObject(this.Roles);
            }

            set
            {
                this.Roles = JsonConvert.DeserializeObject<IList<string>>(value);
            }
        }
        /// <summary>
        /// The hashed password
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Whether the user has confirmed email or not
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Whether the user has confirmed email or not
        /// </summary>
        public bool IsTemporaryPassword { get; set; }
    }
}
