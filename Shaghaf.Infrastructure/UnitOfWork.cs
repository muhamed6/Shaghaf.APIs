using Shaghaf.Core;
using Shaghaf.Core.Entities;
using Shaghaf.Core.Repositories.Contract;
using Shaghaf.Infrastructure.Data;
using Shaghaf.Infrastructure.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Infrastructure
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly StoreContext _dbcontext;

        private Hashtable _repositories;

        
        public UnitOfWork(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;

            

            _repositories = new Hashtable();
        }

        
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var Key = typeof(TEntity).Name; 

           
            if (!_repositories.ContainsKey(Key))
            {
                

                var repository = new GenericRepository<TEntity>(_dbcontext);

                _repositories.Add(Key, repository);
            }

            return _repositories[Key] as IGenericRepository<TEntity>;
        }


        public async Task<int> CompleteAsync()
                 => await _dbcontext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
           => await _dbcontext.DisposeAsync();
    }
}
