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
        public ActionResult<IEnumerable<TreatmentView>> GetAppoinmentbyId(Guid id)
        {
            var threatments = _context.Treatments.Where(b => b.BusinessId == id).ToList();
                               

            return Ok(threatments);


        }

        // create treatment
        [HttpPost("{id}")]
        public ActionResult<Treatment> CreateTreatment(TreatmentView treatmentView) {

            var Treatment = new Treatment
            {
                TreatmentId = treatmentView.Id,
                BusinessId = treatmentView.BusinessId,
                Specification = treatmentView.Specification,
                Duration = treatmentView.Duration,
                Price = treatmentView.Price,
                Category = treatmentView.Category,
                DoctorName = treatmentView.DoctorName
            };

            _context.Treatments.Add(Treatment);
            _context.SaveChanges();
            return Ok(Treatment);
        }

        // update - kavindi 

        // deleted - kavindi
    }
}
