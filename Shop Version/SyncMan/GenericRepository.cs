using System;
using System.Collections.Generic;
using System.Text;

namespace SyncMan
{
    public class GenericRepository<TEntity> : GenericRepositoryBase<TEntity> where TEntity : class
    {
        public class Customer
        {
            public int Id { get; set; }
            public int Name { get; set; }
            public int Email { get; set; }
        }
        public void Add(TEntity entity)
        {
            
        }
    }
}
