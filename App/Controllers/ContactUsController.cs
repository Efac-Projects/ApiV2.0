using App.Models;
using App.Service;
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
        private readonly IMailService _mailService;

        public ContactUsController(ApplicationDbContext context, IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
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


        // get reply
        //api/contactus/id
        [HttpGet("{id}")]
        public ActionResult GetReply(int id) {

            var reply = _context.ContactUs.Where(c => c.ContactId == id).Select(c => c.Reply).ToList();
            
            return Ok(200);
        }

        // send reply
        // api/contactus/reply
        [HttpPost("reply")]

        public ActionResult SendReply(ContactView contactView) {

            ContactUs contact = _context.ContactUs.Find(contactView.ContactId);

            contact.AddReply(contactView.Reply);

            // send email
             _mailService.SendEmailAsync(contact.Email, "Hello There", $"<h3>Welcome to No Queue</h3>" +
                  $"<p>{contactView.Reply}</p>");

            
            return Ok();
        }


    }
}
