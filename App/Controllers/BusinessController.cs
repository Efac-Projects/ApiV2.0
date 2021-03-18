using App.HubConfig;
using App.Models;
using App.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using App.Extension;

namespace App.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private IBusinessRepository _businessRepository;
        private IWebHostEnvironment _hostEnvironment;
        private UserManager<IdentityUser> _userManager;
        //private IHubContext<AppointmentHub> _hub;

        public BusinessController(IBusinessRepository repo, IWebHostEnvironment hostEnvironment, UserManager<IdentityUser> userManager)
        {
            _businessRepository = repo;
            this._hostEnvironment = hostEnvironment;
            _userManager = userManager;

        }

         //Businesses
        // GET: api/Business
        
        [HttpGet]
        //[Authorize(Policy = UserRoles.Admin)]
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


        //api/business/email/{your email}
        [HttpGet("email/{email}")]
        public ActionResult<Business> GetBusinessWithoutId(string email)
        {
            Business business = _businessRepository.GetByEmail(email);

            if (business == null)
                return NotFound();

            return Ok(business);
        }
        
        //api/business/update/{email}
        [HttpPut("update/{email}")]
        public ActionResult<Business> PutBusiness(String email,BusinessView businessView)
        {
           
            var business = _businessRepository.GetByEmail(email);

            if (business == null)
                return NotFound();

           // if (business.BusinessId != business2.BusinessId)
              //  return BadRequest();

            business.Name = businessView.BusinessName;
            business.TotalCrowd = businessView.TotalCrowd;
            business.CurrentCrowd = businessView.CurrentCrowd;
            business.PhoneNumber = businessView.PhoneNumber;
            business.PostalCode = businessView.PostalCode;
            business.Email = businessView.Email;  // user can not change email address 
            business.BusinessType = businessView.BusinessType;
            business.Summary = businessView.Summary;

            _businessRepository.Update(business);
            _businessRepository.SaveChanges();

            return Ok(business);
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
        //[Authorize(Policy = UserRoles.User)]
        //[Authorize(Policy =  UserRoles.User)]
        [HttpPost]
        public ActionResult<Business> CreateBusiness([FromForm]BusinessView businessView)
        {
            

            var business = new Business
            {

                Name = businessView.BusinessName,
                Email = businessView.Email,
                TotalCrowd = businessView.TotalCrowd,
                CurrentCrowd = businessView.CurrentCrowd,
                PhoneNumber = businessView.PhoneNumber,
                BusinessType = businessView.BusinessType,
                Summary = businessView.Summary,
                PostalCode = businessView.PostalCode,
                ImageName = SaveImage(businessView.ImageFile)

            };

            _businessRepository.Add(business);
            _businessRepository.SaveChanges();

            return Ok(business);
        }

        // Upload Image
        public string SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                 imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        // Delete Image
        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
        


    }
}
