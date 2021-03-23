using App.Models;
using App.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Repositories
{
    public interface IAppointmentRepository
    {
        IEnumerable<Appointment> FindAll();
        void Add(Appointment appointment);
        void SaveChanges();
    }
}
