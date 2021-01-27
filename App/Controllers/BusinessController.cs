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
    public class BusinessController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BusinessController(ApplicationDbContext context)
        {
            _context = context;  
        }

        // GET: api/Business
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessView>>> GetAllBusiness()
        {
            return await _context.Businesses.
                Select(x => businessViewReturn(x)
                ).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<BusinessView>>> GetBusinessbyId(int id) {
            var business = await _context.Businesses.FindAsync(id);

            if (business == null) {
                return NotFound();
            }

            return Ok(business);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBusiness(int id, BusinessView businessView)
        {
           // if (id != businessView.BusinessId)
           // {
               // return BadRequest();
           // }

            var business = await _context.Businesses.FindAsync(id);
            if (business == null)
            {
                return NotFound();
            }

            business.BusinessName = businessView.BusinessName;
            business.TotalCrowd = businessView.TotalCrowd;
            business.CurrentCrowd = businessView.CurrentCrowd;
            business.Summary = businessView.Summary;
            business.PhoneNumber = businessView.PhoneNumber;
            business.Address = businessView.Address;



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
        public async Task<ActionResult<BusinessView>> CreateBusiness (BusinessView businessView)
        {
            var business = new Business
            {
              
                BusinessName = businessView.BusinessName,
                TotalCrowd = businessView.TotalCrowd,
                CurrentCrowd = businessView.CurrentCrowd,
                Summary = businessView.Summary,
                PhoneNumber = businessView.PhoneNumber,
                Address = businessView.Address
            };

            _context.Businesses.Add(business);
            await _context.SaveChangesAsync();

            return Ok(business);
                
        }

        // delete method also should be here, sometime not be usefull for this particular case


        private static BusinessView businessViewReturn(Business business) =>
            new BusinessView {
                BusinessId = business.BusinessId,
                BusinessName = business.BusinessName,
                TotalCrowd = business.TotalCrowd,
                CurrentCrowd = business.CurrentCrowd,
                Summary = business.Summary,
                PhoneNumber = business.PhoneNumber,
                Address = business.Address
            };

        private bool businessExists(int id) =>
            _context.Businesses.Any(e => e.BusinessId == id);
    }
}
