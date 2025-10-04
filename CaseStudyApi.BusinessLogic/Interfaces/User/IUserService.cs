using CaseStudyApi.BusinessLogic.Dtos.User;
using CaseStudyApi.BusinessLogic.ViewModels.User;
using CaseStudyApi.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyApi.DataAccess.Interfaces.User
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateUser(CreateUserVM createUserVM);
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
    }
}
