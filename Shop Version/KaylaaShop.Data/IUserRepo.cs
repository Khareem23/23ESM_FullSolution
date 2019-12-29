using KaylaaShop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaylaaShop.Data
{
    public interface IUserRepo
    {
        staff GetUserById(string userId);
        int GetStaffShopId(string name);
    }

    public class UserRepo : IUserRepo
    {
        private readonly KaylaaDataContext dbContext;

        public UserRepo(KaylaaDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int GetStaffShopId(string name)
        {
            var StaffshopId = dbContext.Staffs.Where(c => c.UserName == name).Select(c=> c.shopId).FirstOrDefault();

            return StaffshopId ?? default(int);
        }

        public staff GetUserById(string userId)
        {
          return  dbContext.Staffs.Where(c => c.Id == userId ).FirstOrDefault();
        }
    }
}
