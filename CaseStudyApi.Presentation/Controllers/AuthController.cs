using CaseStudyApi.BusinessLogic.Interfaces.Authentication;
using CaseStudyApi.BusinessLogic.ViewModels.Auth;
using CaseStudyApi.BusinessLogic.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyApi.Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] string refreshToken)
        {
            var response = await _authenticationService.RefreshTokenLoginAsync(refreshToken);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginVM loginVM)
        {
            var response = await _authenticationService.LoginAsync(loginVM.UserNameOrEmail, loginVM.Password, 15);
            return Ok(response);
        }
    }
}
