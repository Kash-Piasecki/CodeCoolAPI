using System.Threading.Tasks;
using CodeCoolAPI.Dtos;
using CodeCoolAPI.Jwt;
using CodeCoolAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeCoolAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("/Register")]
        public async Task<IActionResult> Register(IdentityRegistrationDto identityRegistrationDto)
        {
            var authResponse =
                await _identityService.Register(identityRegistrationDto.Email, identityRegistrationDto.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse()
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse()
            {
                Token = authResponse.Token
            });
        }
    }
}