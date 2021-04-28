using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeCoolAPI.Dtos;
using CodeCoolAPI.Helpers;
using CodeCoolAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeCoolAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService, ILogger<ReviewsController> logger)
        {
            _reviewService = reviewService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewReadDto>>> Get()
        {
            var reviewReadDtoList = await _reviewService.ReadAllReviews();
            _logger.LogInformation(LogMessages.EntitiesFound);
            return Ok(reviewReadDtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewReadDto>> Get(int id)
        {
            var reviewReadById = await _reviewService.ReadReviewById(id);
            _logger.LogInformation(LogMessages.EntityFound);
            return Ok(reviewReadById);
        }

        [HttpPost]
        public async Task<ActionResult> Post(ReviewUpsertDto reviewUpsertDto)
        {
            var userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
            var reviewReadDto = await _reviewService.CreateReviewReadDto(reviewUpsertDto, userId);
            _logger.LogInformation(LogMessages.EntityCreated);
            return CreatedAtAction(nameof(Get), new {reviewReadDto.Id}, reviewReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, ReviewUpsertDto reviewUpsertDto)
        {
            var userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
            var userRole = HttpContext.User.Claims.ToList()[3].Value;
            await _reviewService.UpdateReview(id, reviewUpsertDto, userId, userRole);
            _logger.LogInformation(LogMessages.EntityUpdated);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _reviewService.DeleteReview(id);
            _logger.LogInformation(LogMessages.EntityDeleted);
            return NoContent();
        }
    }
}