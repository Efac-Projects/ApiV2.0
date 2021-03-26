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
    public class ThreatmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThreatmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // get threamtment of business location
        // api/threatment/id  ; businessId
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<TreatmentView>> GetAppoinmentbyId(int id)
        {
            var threatments = _context.Businesses.Where(b => b.BusinessId == id)
                                .Include(b => b.Treatments).ToList();

            return Ok(threatments);


        }

        // create treatment
        [HttpPost("{id}")]
        public ActionResult<Treatment> CreateTreatment(TreatmentView treatmentView) {

            var Treatment = new Treatment
            {
                TreatmentId = treatmentView.Id,
                BusinessId = treatmentView.BusinessId,
                Name = treatmentView.Name,
                Price = treatmentView.Price,
                DoctorName = treatmentView.DoctorName
            };

            _context.Treatments.Add(Treatment);
            _context.SaveChanges();
            return Ok(Treatment);
        }

        // update treatment
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTreatment(int id, TreatmentView treatmentView)
        {
            var treatment = await _context.Treatments.FindAsync(id);

            if (treatment == null)
            {
                return NotFound();
            }

            treatment.Name = treatmentView.Name;
            //treatment.Duration = treatmentView.Duration;
            treatment.Category = treatmentView.Category;
            treatment.Price = treatmentView.Price;
            treatment.DoctorName = treatmentView.DoctorName;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok(treatment);
        }

        // Delete treatment
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTreatment(int id)
        {
            var treatment = await _context.Treatments.FindAsync(id);

            if (treatment == null)
            {
                return NotFound();
            }

            _context.Treatments.Remove(treatment);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}
