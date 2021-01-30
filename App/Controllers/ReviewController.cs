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

    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewView>>> GetAllBusiness()
        {
            return await _context.Reviews.
                Select(x => reviewViewReturn(x)
                ).ToListAsync();
        }

        private static ReviewView reviewViewReturn (Review review) =>
            new ReviewView
            {
                ReviewId = review.ReviewId,
                Comment = review.Comment,
                Rating = review.Rating
            };
    }
}