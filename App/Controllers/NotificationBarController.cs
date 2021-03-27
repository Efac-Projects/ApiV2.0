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

    public class NotificationBarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotificationBarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // get notification by id
        //api/notificationbar/id
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<NotificationView>> GetNotificationbyId(Guid id)
        {
            var notification =  _context.NotificationBars.Where(no => no.BusinessId == id).ToList();

            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        // Update notificationbar
        //api/notificationbar/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotificationBar(int id, NotificationView notificationView)
        {
            var notification = await _context.NotificationBars.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }


            notification.NotificationId = notificationView.NotificationId;
            notification.OpeningHour = notificationView.OpeningHour;
            notification.ClosingHour = notification.ClosingHour;
            notification.Notification = notificationView.Notification;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        //create notification
        //api/notificationbar/id
        [HttpPost("{id}")]
        public async Task<ActionResult<NotificationView>> createNotification(NotificationView notificationView)
        {
            var notification = new NotificationBar
            { BusinessId = notificationView.BusinessId,
                NotificationId = notificationView.NotificationId,
                OpeningHour = notificationView.OpeningHour,
                ClosingHour = notificationView.ClosingHour,
                Notification = notificationView.Notification
            };

            _context.NotificationBars.Add(notification);
            await _context.SaveChangesAsync();

            return Ok(notification);
        }


    }
}
