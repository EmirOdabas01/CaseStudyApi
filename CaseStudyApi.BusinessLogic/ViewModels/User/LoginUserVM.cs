using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyApi.BusinessLogic.ViewModels.User
{
    public class LoginUserVM
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
