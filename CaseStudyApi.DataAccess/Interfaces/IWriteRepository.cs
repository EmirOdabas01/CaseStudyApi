using CaseStudyApi.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyApi.DataAccess.Interfaces
{
    public interface IWriteRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T model);
        Task AddRangeAsync(List<T> models);
        bool Remove(T model);
        void RemoveRange(List<T> models);
        Task<bool> RemoveAsync(int id);
        bool Update(T model);

        Task<int> SaveAsync();
    }
}
