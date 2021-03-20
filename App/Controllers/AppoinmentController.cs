using App.Models;
using App.ViewModel;
using AspNetIdentityDemo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppoinmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppoinmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // get all appoinments
        // GET: api/Appoinment
        //works
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentView>>> GetAllBusiness()
        {
            return await _context.Appointments.
                Select(x => AppointmentViewReturn(x)
                ).ToListAsync();
        }

        // find appoinment with business id
        // api/appoinment/id
        //  works
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<AppointmentView>> GetAppoinmentbyId(int id)
        {
            var appoinment = _context.Businesses.Where(b => b.BusinessId == id)
                                .Include(b => b.Appointments).ToList();
            
            return Ok(appoinment);

            
        }

        // edit appointment
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, AppointmentView appointmentView)
        {
            // if (id != businessView.BusinessId)
            // {
            // return BadRequest();
            // }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            
           




            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok();
        }

        // Post appointment
        // api/ appointment
        [HttpPost]
        public async Task<ActionResult<AppointmentView>> CreateAppointment(AppointmentView appointmentView)
        {
            var appointment = new Appointment
            {
                
                BusinessId = appointmentView.BusinessId,
                FirstName = appointmentView.Firstname,
                LastName = appointmentView.Lastname,
                StartMoment = appointmentView.StartMoment,
                ThreatmentId = appointmentView.TreatmentId
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return Ok(appointment);

        }

        // Delete appointment
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return Ok();
        }

        //  Appointment to AppointmentView
        private static AppointmentView AppointmentViewReturn(Appointment appointment) =>
            new AppointmentView
            {
                AppointmentId = appointment.AppointmentId,
                BusinessId = appointment.BusinessId,
                Firstname = appointment.FirstName,
                Lastname = appointment.LastName,
                TreatmentId = appointment.ThreatmentId
                
                
            };



    }
}
