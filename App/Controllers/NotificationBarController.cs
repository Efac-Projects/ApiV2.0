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
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<NotificationView>>> GetNotificationbyId(int id)
        {
            var notification = await _context.NotificationBars.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        // Update notificationbar
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
        [HttpPost]
        public async Task<ActionResult<NotificationView>> createNotification(NotificationView notificationView)
        {
            var notification = new NotificationBar
            {
                NotificationId = notificationView.NotificationId,
                OpeningHour = notificationView.OpeningHour,
                ClosingHour = notificationView.ClosingHour,
                Notification = notificationView.Notification,
                BusinessId = notificationView.BusinessId
            };

            _context.NotificationBars.Add(notification);
            await _context.SaveChangesAsync();

            return Ok(notification);
        }


    }
}
