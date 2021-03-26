using App.Models;
using AspNetIdentityDemo.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace App.Repositories
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly DbSet<Business> _businesses;
        private readonly ApplicationDbContext _context;

        public BusinessRepository(ApplicationDbContext context)
        {
            _context = context;
            _businesses = context.Businesses;
        }
        
        // get user ID of business user
        public void Add(Business business)
        {
            _businesses.Add(business);
            
            
        }

        public void Delete(Business business)
        {
            _businesses.Remove(business);
        }

        public IEnumerable<Business> GetAll()
        {
            return _businesses
                .Include(hd => hd.Treatments)
                .AsNoTracking()
                .ToList();
        }

        public Business GetBusiness(string email)
        {
            var business = _businesses.Find(email);

            if (business == null) {
                Console.WriteLine("No business find with email");
            }

            return business;
        }

        //works
        public Business GetById(Guid id)
        {
            return _businesses
                 .Include(hd => hd.Treatments)
                
                 .SingleOrDefault(hd => hd.BusinessId == id);
        }

        //working method
        public IEnumerable<Business> GetBy(string name = null)
        {
            return _businesses.Where(hd => hd.Name.StartsWith(name))
                               .Include(hd => hd.Treatments)
                               .Include(hd => hd.Appointments)
                                 
                               .ToList();
        }

        // Working mehthod
        public Business GetByEmail(string email)
        {
            return _businesses.Where(hd => hd.Email == email)
                                .Include(hd => hd.Treatments)
                                .Include(hd => hd.Appointments)
                                
                                .SingleOrDefault();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Business business)
        {
            _businesses.Update(business);
        }
    }
}
