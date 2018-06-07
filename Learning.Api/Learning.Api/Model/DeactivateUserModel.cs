using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Api.Model
{
    public class DeactivateUserModel
    {
        public string UserId { get; set; }

        public bool Deactivate { get; set; }

        public bool Activate { get; set; }
    }
}
