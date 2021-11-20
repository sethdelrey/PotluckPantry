using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PotluckPantry.Areas.Data.Accessors
{
    public interface IUserRepository
    {
        public IdentityUser GetUser(string id);
        public string GetUserIdFromUsername(string username);
        public void Save();
    }
}
