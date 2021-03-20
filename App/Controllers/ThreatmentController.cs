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
        [HttpPost]
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
    }
}
