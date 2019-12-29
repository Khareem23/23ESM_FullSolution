using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KaylaaShop.Core;
using System.Linq.Expressions;

namespace KaylaaShop.Data
{

    public class KaylaaRepository<TEntity> : IKaylaaRepository<TEntity> where TEntity : class
    {
        private readonly KaylaaDataContext dbcontext;

        public KaylaaRepository(KaylaaDataContext context)
        {
            this.dbcontext = context;
        }
        public TEntity Add(TEntity entity)
        {
            dbcontext.Add(entity);
            return entity; 
        }

        public int Commit()
        {
            return dbcontext.SaveChanges();
        }

        public int Count()
        {
           return dbcontext.Set<TEntity>().Count();
        }

        public TEntity Delete(int id)
        {
            var myobject = GetById(id);

            if (myobject != null)
            {
                dbcontext.Set<TEntity>().Remove(myobject);
            }

            return myobject;
           
        }

        public staff DeleteByStringID(string id)
        {
            staff myobject = GetById(id);

            if (myobject != null)
            {
                dbcontext.Staffs.Remove(myobject);
            }

            return myobject;

        }



        public IEnumerable<TEntity> GetAll()
        {
            return dbcontext.Set<TEntity>();
        }

       
        public TEntity GetById(int id)
        {
            return dbcontext.Set<TEntity>().Find(id);
        }

        public staff GetById(string id)
        {
            return dbcontext.Staffs.Where(u => u.Id == id).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetByName(string name)
        {
            // Not Tested Yet 
            var EntityResult = dbcontext.Set<TEntity>().Where(x => x.GetType().GetProperty("Name").GetValue(x).Equals(name)).ToList();

            return EntityResult;
        }

        public bool IsEmailExist(string email)
        {
            bool flag = false;
            ParameterExpression TypeName = Expression.Parameter(typeof(TEntity), "x");

            MemberExpression selection = Expression.PropertyOrField(TypeName, "email");

            var lambdaExp = Expression.Lambda<Func<TEntity, string>>(selection, TypeName);

            var EntityList = dbcontext.Set<TEntity>().Select(lambdaExp).ToList();

            foreach (var item in EntityList)
            {
                if (item == email) flag = true;

                return flag;
            }

            return flag;

            //var EntityName = dbcontext.Set<TEntity>().FirstOrDefault(x => x.GetType().GetProperty("email").GetValue(x).Equals(email));

            //if (EntityName != null) return true;
            //else return false;
        }

        public bool IsNameExist(string name)
        {
            bool flag = false; 
            ParameterExpression TypeName = Expression.Parameter(typeof(TEntity), "x");

            MemberExpression selection = Expression.PropertyOrField(TypeName, "Name");

            var lambdaExp = Expression.Lambda<Func<TEntity, string>>(selection, TypeName);

            var EntityList = dbcontext.Set<TEntity>().Select(lambdaExp).ToList();

            foreach (var item in EntityList)
            {
                if (item == name) flag = true;

                return flag;
            }

            return flag;
            // var EntityName = dbcontext.Set<TEntity>().Where(x => x.GetType().GetProperty("Name").GetValue(x).Equals(name)).ToList();
           // var EntityName = dbcontext.Set<TEntity>().FirstOrDefault(x => x.GetType().GetProperty("Name").GetValue(x).Equals(name)) ;

            //if (EntityName != null) return true;
            //else return false;

        }

        public TEntity Update(TEntity newentity)
        {
            var entity = dbcontext.Set<TEntity>().Attach(newentity);
            entity.State = EntityState.Modified;
            return newentity;
        }
    }
}
