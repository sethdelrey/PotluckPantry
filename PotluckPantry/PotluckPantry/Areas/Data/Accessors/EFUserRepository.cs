using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PotluckPantry.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PotluckPantry.Areas.Data.Accessors
{
    public class EFUserRepository : IUserRepository
    {
        private ApplicationDbContext _context { get; set; }

        public EFUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IdentityUser GetUser(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var user = _context.Users.AsNoTracking().Where(u => u.Id.Equals(id)).FirstOrDefault();
                return user;
            }

            return null;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public string GetUserIdFromUsername(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                return _context.Users.AsNoTracking().Where(u => u.UserName.Equals(username)).Select(u => u.Id).FirstOrDefault();
            }

            return null;
        }
    }
}
