using CaseStudyApi.BusinessLogic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyApi.BusinessLogic.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime);
        Task<Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
