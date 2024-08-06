using Shaghaf.Core.Entities;
using Shaghaf.Core.Entities.HomeEntities;
using Shaghaf.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {

    
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

       
        Task<int> CompleteAsync();


    }
}
