using CaseStudyApi.BusinessLogic.Dtos;
using CaseStudyApi.BusinessLogic.Dtos.User;
using CaseStudyApi.BusinessLogic.Interfaces;
using CaseStudyApi.BusinessLogic.ViewModels.User;
using CaseStudyApi.DataAccess.Interfaces.User;
using CaseStudyApi.Domain.Entities.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaseStudyApi.BusinessLogic.Interfaces.Authentication;
using System.Data.Entity;
namespace CaseStudyApi.BusinessLogic.Services.Authentication
{
    public class AuthenticationService : CaseStudyApi.BusinessLogic.Interfaces.Authentication.IAuthenticationService
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;
        readonly IUserService _userService;
        public AuthenticationService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, 
            ITokenHandler tokenHandler,
            IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _userService = userService;
        }

        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);

            if (user == null)
                throw new Exception();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded) 
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 15);
                return token;
            }
            throw new Exception();
        }
        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user =  _userManager.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(15);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 15);
                return token;
            }
            else
                throw new Exception("User not found");
        }
    }
}
