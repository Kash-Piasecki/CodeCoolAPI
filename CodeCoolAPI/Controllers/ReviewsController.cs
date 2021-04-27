using System.Collections.Generic;
using System.Threading.Tasks;
using CodeCoolAPI.Dtos;
using CodeCoolAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeCoolAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewReadDto>>> Get()
        {
            var reviewReadDtoList = await _reviewService.ReadAllReviews();
            return Ok(reviewReadDtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewReadDto>> Get(int id)
        {
            var reviewReadById = await _reviewService.ReadReviewById(id);
            return Ok(reviewReadById);
        }

        [HttpPost]
        public async Task<ActionResult> Post(ReviewUpsertDto reviewUpsertDto)
        {
            var reviewReadDto = await _reviewService.CreateReviewReadDto(reviewUpsertDto);
            return CreatedAtAction(nameof(Get), new {reviewReadDto.Id}, reviewReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, ReviewUpsertDto reviewUpsertDto)
        {
            await _reviewService.UpdateReview(id, reviewUpsertDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _reviewService.DeleteReview(id);
            return NoContent();
        }
    }
}