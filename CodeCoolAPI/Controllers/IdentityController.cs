using System.Threading.Tasks;
using CodeCoolAPI.Dtos.User;
using CodeCoolAPI.Helpers;
using CodeCoolAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeCoolAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger _logger;

        public IdentityController(IIdentityService identityService, ILogger<IdentityController> logger)
        {
            _identityService = identityService;
            _logger = logger;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterUserDto registerUserDto)
        {
            await _identityService.RegisterUser(registerUserDto);
            _logger.LogInformation(LogMessages.UserRegisteredSuccess);
            return Ok();
        }
        
    }
}