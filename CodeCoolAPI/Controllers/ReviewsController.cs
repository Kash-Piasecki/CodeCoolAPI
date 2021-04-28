using System.Collections.Generic;
using System.Threading.Tasks;
using CodeCoolAPI.Dtos;
using CodeCoolAPI.Helpers;
using CodeCoolAPI.Jwt;
using CodeCoolAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeCoolAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly ILogger _logger;

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
            var reviewReadDto = await _reviewService.CreateReviewReadDto(reviewUpsertDto, HttpContext.GetUserId());
            _logger.LogInformation(LogMessages.EntityCreated);
            return CreatedAtAction(nameof(Get), new {reviewReadDto.Id}, reviewReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, ReviewUpsertDto reviewUpsertDto)
        {
            await _reviewService.UserOwnsPost(id, HttpContext.GetUserId());
            await _reviewService.UpdateReview(id, reviewUpsertDto);
            _logger.LogInformation(LogMessages.EntityUpdated);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _reviewService.UserOwnsPost(id, HttpContext.GetUserId());
            await _reviewService.DeleteReview(id);
            _logger.LogInformation(LogMessages.EntityDeleted);
            return NoContent();
        }
    }
}