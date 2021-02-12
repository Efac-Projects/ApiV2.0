using App.Models;
using App.ViewModel;
using AspNetIdentityDemo.Api.Models;
using Microsoft.AspNetCore.Authorization;
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

        // GET: api/Review
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewView>>> GetAllReview()
        {
            return await _context.Reviews.
                Select(x => reviewViewReturn(x)
                ).ToListAsync();
        }

        // Review by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ReviewView>>> GetReviewbyId(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        // Edit Review
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBusiness(int id, ReviewView reviewView)
        {
            // if (id != businessView.BusinessId)
            // {
            // return BadRequest();
            // }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            review.ReviewId = reviewView.ReviewId;
            review.Comment = reviewView.Comment;
            review.Rating = reviewView.Rating;
            



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Business")]
        public async Task<ActionResult<ReviewView>> CreateReview(ReviewView reviewView)
        {
            var review = new Review
            {

                ReviewId = reviewView.ReviewId,
                Comment = reviewView.Comment,
                Rating = reviewView.Rating
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return Ok(review);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // Map review to review view
        private static ReviewView reviewViewReturn (Review review) =>
            new ReviewView
            {
                ReviewId = review.ReviewId,
                Comment = review.Comment,
                Rating = review.Rating
            };
    }
}