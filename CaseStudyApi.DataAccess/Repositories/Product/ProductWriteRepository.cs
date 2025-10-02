using CaseStudyApi.DataAccess.Interfaces;
using CaseStudyApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyApi.DataAccess.Repositories
{
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(CaseStudyDbContext context) : base(context)
        {
        }
    }
}
