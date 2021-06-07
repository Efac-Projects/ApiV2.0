using App.Models;
using App.ViewModel;
using AspNetIdentityDemo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ContactUsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactUsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get All contact messages
        // api/contactus/id
        [HttpGet]
        public ActionResult<IEnumerable<ContactView>> GetContactbyId()
        {
            var messages = _context.ContactUs.ToList();

            if (messages == null) {
                return NotFound();
            }

            return Ok(messages);
           
        }

        //api/contactus
        [HttpPost]
        public async Task<ActionResult<ContactView>> CreateContact(ContactView contactView)
        {
            var contact = new ContactUs
            {
                ContactId = contactView.ContactId,
                FullName = contactView.FullName,
                Email = contactView.Email,
                Message = contactView.Message,
                // ?

            };

            _context.ContactUs.Add(contact);
            await _context.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactUs(int id)
        {
            var contact = await _context.ContactUs.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            _context.ContactUs.Remove(contact);
            await _context.SaveChangesAsync();

            return Ok(contact);
        }

        // get all

    }
}
