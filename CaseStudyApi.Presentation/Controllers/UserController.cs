using CaseStudyApi.BusinessLogic.ViewModels.User;
using CaseStudyApi.DataAccess.Interfaces.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserVM createUserVM)
        {
            var response = await _userService.CreateUser(createUserVM);
            return Ok(response);
        }
    }
}
