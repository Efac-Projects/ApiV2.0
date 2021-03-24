
using App.Models;
using App.ViewModel;
using AspNetIdentityDemo.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public AppointmentRepository()
        {
        }

        // Find all appoinments
        public IEnumerable<Appointment> FindAll()
        {
            var appointments = new List<Appointment>();
            try
            {
                 appointments = _context.Appointments.ToList();

                return appointments;
            }

            catch(Exception e) {
                Console.WriteLine(e.Message);
            }

            return appointments;
        }

        private static AppointmentView AppointmentViewReturn(Appointment appointment) =>
           new AppointmentView
           {
               AppointmentId = appointment.AppointmentId,
               BusinessId = appointment.BusinessId,
               Firstname = appointment.FirstName,
               Lastname = appointment.LastName,
              // TreatmentId = appointment.TreatmentId


           };

        public void Add(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
