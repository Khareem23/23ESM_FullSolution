using KaylaaShop.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KaylaaShop.Data
{
    public interface IKaylaaRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity) ;
        TEntity Delete(int  id);
        staff DeleteByStringID(string id);
        TEntity Update(TEntity entity);
        IEnumerable<TEntity> GetAll();

        bool IsNameExist(string name);

        bool IsEmailExist(string email);
        
       
        TEntity GetById(int id);
        staff GetById(string id);
        IEnumerable<TEntity> GetByName(string name);

        int Count();
        int Commit();
    }
}
