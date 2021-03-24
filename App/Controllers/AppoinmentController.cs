﻿using App.HangFire;
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
        public ActionResult<IEnumerable<AppointmentView>> GetAppoinmentbyId(int id)
        {
            var appoinment = _context.Appointments.Where(b => b.BusinessId == id).ToList();

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
            
          

            var appointment = new Appointment
            {

                BusinessId = appointmentView.BusinessId,
                FirstName = appointmentView.Firstname,
                LastName = appointmentView.Lastname,
                StartMoment = appointmentView.StartMoment,
                PhoneNumber = appointmentView.PhoneNumber,
                CreatedAt = appointmentView.CreatedAt,
                Timezone = appointmentView.TimeZone
        };


            TimeSpan time = appointmentView.StartMoment.TimeOfDay - appointmentView.CreatedAt.TimeOfDay;
            int timeValue = (int)time.TotalMinutes - 30; 
           // BackgroundJob.Schedule<SendNotificationAppoinment>((job) =>job.Execute(appointment) ,TimeSpan.FromMinutes(1));

            _repo.Add(appointment);
            _repo.SaveChanges();

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
               //TreatmentId = appointment.TreatmentId


           };


    }
}
