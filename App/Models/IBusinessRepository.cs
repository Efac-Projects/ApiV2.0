using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public interface IBusinessRepository
    {
        Business GetBy(int id);
        Business GetByEmail(string email);
        IEnumerable<Business> GetAll();
        IEnumerable<Business> GetBy(string name = null); // there should be method for postolcode also
        void Add(Business business);
        void Delete(Business business);
        void Update(Business business);
        void SaveChanges();
    }
}
