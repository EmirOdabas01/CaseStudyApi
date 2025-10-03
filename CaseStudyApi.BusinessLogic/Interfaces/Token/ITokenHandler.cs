using CaseStudyApi.BusinessLogic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyApi.BusinessLogic.Interfaces
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(int minute);
    }
}
