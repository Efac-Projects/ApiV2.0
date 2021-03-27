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
using AspNetIdentityDemo.Api.Models;
using Microsoft.EntityFrameworkCore;
using Hangfire;

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
        private readonly ApplicationDbContext _context;
        private readonly IBackgroundJobClient _backgroundJobClient;

        //private IHubContext<AppointmentHub> _hub;

        public BusinessController(IBusinessRepository repo, IWebHostEnvironment hostEnvironment, ApplicationDbContext context, IBackgroundJobClient backgroundJobClient)
        {
            _businessRepository = repo;
            this._hostEnvironment = hostEnvironment;
            _context = context;
            _backgroundJobClient = backgroundJobClient;



        }

        //Businesses
        // GET: api/Business
        // This get method is used to gel all business info inside business proffile
        
        [HttpGet]
        //[Authorize(Policy = UserRoles.Admin)]
        public ActionResult<IEnumerable<Business>> GetBusinesses(string name = null)
        {
            if (string.IsNullOrEmpty(name))
                return Ok(_businessRepository.GetAll());
            else return Ok(_businessRepository.GetBy(name));
        }

        // get business card image details
        // api/business/card
        [HttpGet("card")]
        public async Task<ActionResult<IEnumerable<Business>>> GetEmployees()
        {
            return await _context.Businesses
                .Select(x => new Business()
                {
                    BusinessId = x.BusinessId,
                    Name = x.Name,
                    Email = x.Email,
                    Summary = x.Summary,
                    ImageName = x.ImageName,
                    ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)
                })
                .ToListAsync();
        }

        // GET: api/Business/1 // 

        [HttpGet("{id}")]
        public ActionResult<Business> GetBusiness(Guid id)
        {
            Business business = _businessRepository.GetById(id);

            if (business == null)
                return NotFound();

            return Ok(business);
        }

        // get business without ids
        //api/business/email/{your email}
        [HttpGet("email/{email}")]
        public ActionResult<Business> GetBusinessWithoutId(string email)
        {
            Business business = _businessRepository.GetByEmail(email);

            if (business == null)
                return NotFound();

            return Ok(business);
        }

        // update method  for businesses new,
        //api/business/id

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin,Business")]
        public async Task<IActionResult> UpdateBusiness(Guid id, BusinessView businessView)
        {

            


            var business = await _context.Businesses.FindAsync(id);
            if (business == null)
            {
                return NotFound();
            }

            business.Name = businessView.Name;
           // business.Email = businessView.Email;
            business.TotalCrowd = businessView.TotalCrowd;

            business.PhoneNumber = businessView.PhoneNumber;
            business.BusinessType = businessView.BusinessType;
            business.Summary = businessView.Summary;
            business.PostalCode = businessView.PostalCode;




            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok(business);
        }


        /// Deletes the logged in business // - kavindi
        
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
        public ActionResult<Business> CreateBusiness(BusinessView businessView)
        {
            
            var business = new Business
            {

                Name = businessView.Name,
                Email = businessView.Email,
                TotalCrowd = businessView.TotalCrowd,
                
                PhoneNumber = businessView.PhoneNumber,
                BusinessType = businessView.BusinessType,
                Summary = businessView.Summary,
                PostalCode = businessView.PostalCode,
                //ImageName = SaveImage(businessView.ImageFile)

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
                 imageFile.CopyTo(fileStream);
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

        // Delete business
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusiness(int id)
        {
            var business = await _context.Businesses.FindAsync(id);

            if (business == null)
            {
                return NotFound();
            }

            _context.Businesses.Remove(business);
            await _context.SaveChangesAsync();

            return Ok();

        }

    }
}
