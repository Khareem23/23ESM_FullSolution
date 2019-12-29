using KaylaaShop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaylaaShop.Data
{
    public interface IExpensesRepo
    {
        IEnumerable<Expenses> GetShopExpenses(int id);
    }

    public class ExpensesRepo : IExpensesRepo
    {
        private readonly KaylaaDataContext dbcontext;

        public ExpensesRepo(KaylaaDataContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public IEnumerable<Expenses> GetShopExpenses(int id)
        {
            return dbcontext.Set<Expenses>().Where(x => x.shopId == id).Select(x => x);
        }
    }
}
