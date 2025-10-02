using CaseStudyApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyApi.DataAccess.Interfaces
{
    public interface IProductWriteRepository : IWriteRepository<Product>
    {
    }
}
