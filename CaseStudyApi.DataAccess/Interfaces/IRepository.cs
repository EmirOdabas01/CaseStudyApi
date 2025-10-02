using CaseStudyApi.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyApi.DataAccess.Interfaces
{
    public interface IRepository<T> where T : BaseEntity 
    {
         DbSet<T> Table { get;}
    }
}
