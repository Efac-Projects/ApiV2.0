using App.Models;
using App.ViewModel;
using AspNetIdentityDemo.Api.Models;
using Microsoft.AspNetCore.Authorization;
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

    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceView>>> GetAllBusiness()
        {
            return await _context.Services.
                Select(x => serviceViewReturn(x)
                ).ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<ServiceView>>> GetServicebyId(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Business")]
        public async Task<IActionResult> UpdateService(int id, ServiceView serviceView)
        {
            

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            service.ServiceId = serviceView.ServiceId;
            service.ServiceName = serviceView.ServiceName;
            service.StartTime = serviceView.StartTime;
            service.EndTime = serviceView.EndTime;
            service.IsAvailable = serviceView.IsAvailable;


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

        [HttpPost]
        [Authorize(Roles = "Business")]
        public async Task<ActionResult<ServiceView>> CreateService(ServiceView serviceView)
        {
            var service = new Service
            {

                ServiceId = serviceView.ServiceId,
                ServiceName = serviceView.ServiceName,
                StartTime = serviceView.StartTime,
                EndTime = serviceView.EndTime,
                IsAvailable = serviceView.IsAvailable
            };

            _context.Businesses.Add(service);
            await _context.SaveChangesAsync();

            return Ok(service);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private static ServiceView serviceViewReturn( App.Models.Service service
            ) =>
           new ServiceView
           {
               ServiceId = service.ServiceId,
               ServiceName = service.ServiceName,
               StartTime = service.StartTime,
               EndTime = service.EndTime,
               IsAvailable = service.IsAvailable
           };
    }
}