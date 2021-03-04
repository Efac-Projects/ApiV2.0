using App.HubConfig;
using App.Models;
using App.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private IBusinessRepository _businessRepository;
        private IHubContext<AppointmentHub> _hub;

        public BusinessController(IBusinessRepository repo)
        {
            _businessRepository = repo;

        }

        #region Businesses
        // GET: api/Business

        [HttpGet]
        public ActionResult<IEnumerable<Business>> GetBusinesses(string name = null)
        {
            if (string.IsNullOrEmpty(name))
                return Ok(_businessRepository.GetAll());
            else return Ok(_businessRepository.GetBy(name));
        }

        // GET: api/Business/1

        [HttpGet("{id}")]
        public ActionResult<Business> GetBusiness(int id)
        {
            Business business = _businessRepository.GetBy(id);

            if (business == null)
                return NotFound();

            return Ok(business);
        }


        [Authorize]
        [HttpGet("loggedInUser")]
        public ActionResult<Business> GetBusinessWithoutId()
        {
            Business business = _businessRepository.GetByEmail(User.Identity.Name);

            if (business == null)
                return NotFound();

            return Ok(business);
        }

        [HttpPut("Business")]
        public ActionResult<Business> PutBusiness(Business business)
        {
            Business business2 = _businessRepository.GetByEmail(User.Identity.Name);

            if (business == null)
                return NotFound();

            if (business.BusinessId != business2.BusinessId)
                return BadRequest();

            business2.Name = business.Name;

            _businessRepository.Update(business2);
            _businessRepository.SaveChanges();

            return Ok(business2);
        }


        /// Deletes the logged in business

        [HttpDelete("Business/{id}")]
        public IActionResult DeleteBusiness()
        {
            Business business = _businessRepository.GetByEmail(User.Identity.Name);

            if (business == null)
                return NotFound();

            _businessRepository.Delete(business);
            _businessRepository.SaveChanges();

            return NoContent();
        }

        // Create Business
        [HttpPost]
        public ActionResult<Business> CreateBusiness(BusinessView businessView)
        {

            var business = new Business
            {

                Name = businessView.Name,
                Email = businessView.Email,
                TotalCrowd = businessView.TotalCrowd,
                CurrentCrowd = businessView.CurrentCrowd,
                PhoneNumber = businessView.PhoneNumber,
                BusinessType = businessView.BusinessType,
                Summary = businessView.Summary,
                PostalCode = businessView.PostalCode


            };

            _businessRepository.Add(business);
            _businessRepository.SaveChanges();

            return Ok(business);
        }

        #endregion


    }
}
