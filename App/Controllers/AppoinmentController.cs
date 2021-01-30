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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentView>>> GetAllBusiness()
        {
            return await _context.Appointments.
                Select(x => AppointmentViewReturn(x)
                ).ToListAsync();
        }


        private static AppointmentView AppointmentViewReturn(Appointment appointment) =>
            new AppointmentView
            {
                AppointmentId = appointment.AppointmentId,
                Price = appointment.Price,
                Total = appointment.Total,
            };



    }
}
