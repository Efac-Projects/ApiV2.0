using App.HangFire;
using App.Models;
using App.Repositories;
using App.Twilio;
using App.ViewModel;
using AspNetIdentityDemo.Api.Models;
using Hangfire;
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
        private readonly IAppointmentRepository _repo;

        public AppoinmentController(ApplicationDbContext context, IAppointmentRepository repo)
        {
            _context = context;
            _repo = repo;

        }

        // get all appoinments
        // GET: api/Appoinment
        //works
        [HttpGet]
        public ActionResult<IEnumerable<AppointmentView>> GetAllAppoinments()
        {
            return Ok(_repo.FindAll());
        }

        // find appoinment with business id
        // api/appoinment/id
        //  works
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<AppointmentView>> GetAppoinmentbyId(Guid id)
        {
            // CODE CHANGED HERE
            var appoinment = _context.Appointments.Where(b => b.BusinessId == id && b.IsConfirmed == false).ToList();

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
        // api/appointment/id
        [HttpPost("{id}")]
        public  ActionResult<Appointment> CreateAppointment( AppointmentView appointmentView)
        {

            int id = appointmentView.TreatmentId;
            var treatment = _context.Treatments.Find(id);

            var appointment = new Appointment()
            {

                BusinessId = appointmentView.BusinessId,
                FirstName = appointmentView.Firstname,
                LastName = appointmentView.Lastname,
                StartDate = appointmentView.StartDate,
                Start = appointmentView.Start,
                End = appointmentView.Start.Add(treatment.Duration),
                PhoneNumber = appointmentView.PhoneNumber,
                CreatedAt = appointmentView.CreatedAt.ToShortDateString(),
                Age = appointmentView.Age,
                Gender = appointmentView.Gender,
                Treatment = _context.Treatments.Find(appointmentView.TreatmentId)
                

            };


            TimeSpan time = appointmentView.Start.TimeOfDay - appointmentView.CreatedAt.TimeOfDay;
            int timeValue = (int)time.TotalMinutes - 30; 
            BackgroundJob.Schedule<SendNotificationAppoinment>((job) =>job.Execute(appointment) ,TimeSpan.FromSeconds(30));

            _repo.Add(appointment);
            _repo.SaveChanges();

            return Ok(appointment);

        }

        // send appoinment confirmation message
        // api/appointment/confirm
        [HttpPost("confirm")]
        public ActionResult<ConfirmAppoinment> ConfirmAppoinment(ConfirmAppoinment confirmAppoinment)
        {
            Appointment appointment = _context.Appointments.Find(confirmAppoinment.AppoinmentId);

            appointment.ConfirmApp(); // confirm current appoinmet
            _context.SaveChanges();

            BackgroundJob.Schedule<SendAppoinmentConfirm>((job) => job.Execute(confirmAppoinment), TimeSpan.FromSeconds(30));

            return Ok("Appoinment has succesfully confirmed");
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

        // return time slots for business
        //api/appoinment/time/{id}
        [HttpGet("time/{id}")]
        public ActionResult GetTime(Guid id)
        {

            var time = _context.Appointments.Where(ap => ap.BusinessId == id && ap.IsConfirmed == true).Select(ap => new
            {
                title = ap.Treatment.DoctorName,
                start = ap.Start,
                end = ap.End
            }).ToList();


            return Ok(time);
        }

        // return booked timed slots for treatments - not approved
        // //api/appoinment/treatment/{id}
        [HttpGet("tretment/{id}")]
        public ActionResult GetTimesforTreat(int id)
        {
            var time = _context.Appointments.Where(ap => ap.Treatment.TreatmentId == id).Select(ap => new
            {
                start = ap.Start,
                end = ap.End
            }).ToList();

            return Ok(time);
        }

        //  Appointment to AppointmentView

        private static AppointmentView AppointmentViewReturn(Appointment appointment) =>
           new AppointmentView
           {
               AppointmentId = appointment.AppointmentId,
               BusinessId = appointment.BusinessId,
               Firstname = appointment.FirstName,
               Lastname = appointment.LastName,
               //TreatmentId = appointment.TreatmentId


           };


    }
}
