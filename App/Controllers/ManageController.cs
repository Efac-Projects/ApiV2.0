using App.Models;
using App.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ManageController : ControllerBase
    {
        private readonly IBusinessRepository _businessRepository;

        public ManageController(IBusinessRepository repo)
        {
            this._businessRepository = repo;
            // hub config should here
        }






        /// Get all the appointments of the logged in business


        [HttpGet("Appointments")]
        public ActionResult<IEnumerable<Appointment>> GetAppointments()
        {
            Business business = _businessRepository.GetByEmail(User.Identity.Name);

            if (business == null)
                return NotFound();

            return Ok(business.Appointments.Select(a => new AppointmentView() { AppointmentId = a.AppointmentId, Firstname = a.FirstName, Lastname = a.LastName, StartMoment = a.StartMoment, Treatments = a.Threatment }).ToList());    // as a list
        }


        /// Get an appointment of the logged in business

        [HttpGet("Appointments/{id}")]
        public ActionResult<AppointmentView> GetAppointment(int id)
        {
            Business business = _businessRepository.GetByEmail(User.Identity.Name);

            if (business == null)
                return NotFound();

            Appointment appointment = business.GetAppointment(id);

            if (appointment == null)
                return NotFound();

            return Ok(new AppointmentView() { AppointmentId = appointment.AppointmentId, Firstname = appointment.FirstName, Lastname = appointment.LastName, StartMoment = appointment.StartMoment, Treatments = appointment.Threatment });
        }


        /// Delete an appointment of the logged in business

        [HttpDelete("Appointments/{id}")]
        public IActionResult DeleteAppointemnt(int id)
        {
            Business business = _businessRepository.GetByEmail(User.Identity.Name);

            if (business == null)
                return NotFound();

            Appointment appointment = business.GetAppointment(id);

            business.RemoveAppointment(appointment);

            _businessRepository.SaveChanges();

            return NoContent();
        }


        /// Add a new treatment to the logged in business

        [HttpPost("Treatments")]
        public ActionResult<Treatment> PostTreatment(TreatmentView treatment)
        {
            Business business = _businessRepository.GetByEmail(User.Identity.Name);

            if (business == null)
                return NotFound();

            Treatment treatmenToCreate = new Treatment(treatment.Name, new TimeSpan(treatment.Duration.Hours, treatment.Duration.Minutes, treatment.Duration.Seconds), treatment.Category, treatment.Price);

            business.AddTreatment(treatmenToCreate);

            _businessRepository.SaveChanges();

            return Ok(treatmenToCreate);
        }


        /// Update a treatment of the logged in business

        [HttpPut("Treatments/{id}")]
        public ActionResult<Treatment> PutTreatment(int id, TreatmentView treatment)
        {
            Business hairdresser = _businessRepository.GetByEmail(User.Identity.Name);

            if (hairdresser == null)
                return NotFound();

            if (id != treatment.Id)
                return BadRequest();

            Treatment treatment2 = new Treatment(treatment.Name, new TimeSpan(treatment.Duration.Hours, treatment.Duration.Minutes, treatment.Duration.Seconds), treatment.Category, treatment.Price) { TreatmentId = treatment.Id };

            bool result = hairdresser.UpdateTreatment(treatment2);

            if (result == false)
                return NotFound();

            _businessRepository.SaveChanges();
            return NoContent();
        }


        /// Delete a treatment of a business

        [HttpDelete("Treatments/{id}")]
        public ActionResult DeleteTreatment(int id)
        {
            Business business = _businessRepository.GetByEmail(User.Identity.Name);

            if (business == null)
                return NotFound();

            Treatment treatment = business.GetTreatment(id);

            if (treatment == null)
                return NotFound();

            business.RemoveTreatment(treatment);
            _businessRepository.SaveChanges();

            return NoContent();
        }


        /// Get the opening hours of the logged in business


        [HttpGet("Workdays")]
        public ActionResult<List<WorkDay>> GetWorkDays()
        {
            Business business = _businessRepository.GetByEmail(User.Identity.Name);

            if (business == null)
                return NotFound();

            List<WorkDayView> workDays = new List<WorkDayView>();

            foreach (WorkDay workDay in business.OpeningHours.WorkDays)
            {
                workDays.Add(new WorkDayView((int)workDay.Day, workDay.Hours.ToList()));
            }

            return Ok(workDays);
        }


        /// Add a new workday to the logged in business

        [HttpPost("Workdays")]
        public ActionResult<List<WorkDay>> PostWorkDays(IList<WorkDayView> workDays)
        {
            Business business = _businessRepository.GetByEmail(User.Identity.Name);

            if (business == null)
                return NotFound();

            if (workDays.Any(wd => wd.DayId > 6 || wd.DayId < 0))
                return BadRequest();


            foreach (WorkDayView workDay in workDays)
            {
                business.ChangeWorkDays(workDay.DayId, workDay.Hours);
            }

            _businessRepository.SaveChanges();

            return Ok(business.OpeningHours.WorkDays.Select(wd => new WorkDayView((int)wd.Day, wd.Hours.ToList())).ToList());
        }


        
    }

}
