using CaseStudyApi.BusinessLogic.Dtos;
using CaseStudyApi.BusinessLogic.Dtos.User;
using CaseStudyApi.BusinessLogic.Interfaces;
using CaseStudyApi.BusinessLogic.ViewModels.User;
using CaseStudyApi.DataAccess.Interfaces.User;
using CaseStudyApi.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CaseStudyApi.BusinessLogic.Services.User
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }
        public async Task<CreateUserResponse> CreateUser(CreateUserVM createUserVM)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                UserName = createUserVM.Username,
                Email = createUserVM.Email,
                NameSurname = createUserVM.NameSurname,
            }, createUserVM.Password);

            CreateUserResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "User is created successfully";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new Exception("User not found");
        }
    }
}
