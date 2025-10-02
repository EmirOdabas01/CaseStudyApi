using CaseStudyApi.DataAccess.Interfaces;
using CaseStudyApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyApi.DataAccess.Repositories
{
    public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
    {
        public ProductReadRepository(CaseStudyDbContext context) : base(context)
        {
        }
    }
}
