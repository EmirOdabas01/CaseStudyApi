using CaseStudyApi.BusinessLogic.Dtos.User;
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

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
    }
}
