using Microsoft.EntityFrameworkCore;
using PotluckPantry.Areas.Data.Entities;
using PotluckPantry.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PotluckPantry.Areas.Data.Accessors
{
    public class EFReviewRepository : IReviewRepository
    {
        private ApplicationDbContext _context { get; set; }

        public EFReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DeleteReview(string reviewId)
        {
            throw new NotImplementedException();
        }

        public Review GetReview(string reviewId)
        {
            return _context.Reviews.AsNoTracking().Include(r => r.User).FirstOrDefault();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateReview(Review review)
        {
            throw new NotImplementedException();
        }

        public void CreateReview(Review review)
        {
            throw new NotImplementedException();
        }
    }
}
