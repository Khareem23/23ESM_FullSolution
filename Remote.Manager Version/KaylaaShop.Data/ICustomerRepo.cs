using KaylaaShop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaylaaShop.Data
{
    public interface ICustomerRepo
    {
       Customer GetCustomerByPhoneNo(string Phonenumber);
    }

    public class CustomerRepo : ICustomerRepo
    {
        private readonly KaylaaDataContext dbcontext;

        public CustomerRepo(KaylaaDataContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

      

        public Customer GetCustomerByPhoneNo(string Phonenumber)
        {
            return dbcontext.Customers.Where(c => c.phoneNumber == Phonenumber).FirstOrDefault();
        }
    }
}
