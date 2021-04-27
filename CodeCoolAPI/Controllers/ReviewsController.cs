using System.Collections.Generic;
using System.Threading.Tasks;
using CodeCoolAPI.Dtos;
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
            _logger.LogInformation("Entities list found");
            return Ok(reviewReadDtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewReadDto>> Get(int id)
        {
            var reviewReadById = await _reviewService.ReadReviewById(id);
            _logger.LogInformation("Entity found");
            return Ok(reviewReadById);
        }

        [HttpPost]
        public async Task<ActionResult> Post(ReviewUpsertDto reviewUpsertDto)
        {
            var reviewReadDto = await _reviewService.CreateReviewReadDto(reviewUpsertDto);
            _logger.LogInformation("Entity created");
            return CreatedAtAction(nameof(Get), new {reviewReadDto.Id}, reviewReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, ReviewUpsertDto reviewUpsertDto)
        {
            await _reviewService.UpdateReview(id, reviewUpsertDto);
            _logger.LogInformation("Entity updated");
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _reviewService.DeleteReview(id);
            _logger.LogInformation("Entity deleted");
            return NoContent();
        }
    }
}