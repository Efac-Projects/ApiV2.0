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