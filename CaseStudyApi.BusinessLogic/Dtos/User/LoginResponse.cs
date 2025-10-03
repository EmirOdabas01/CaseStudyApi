using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyApi.BusinessLogic.Dtos.User
{
    public class LoginResponse
    {

    }
    public class LoginSuccessResponse : LoginResponse
    {
        public Token Token { get; set; }
    }
    public class LoginErrorResponse : LoginResponse
    {
        public string Message { get; set; }
    }
}
