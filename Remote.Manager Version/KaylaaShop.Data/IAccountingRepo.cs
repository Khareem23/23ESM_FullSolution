using System;
using System.Linq;

namespace KaylaaShop.Data
{
    public interface IAccountingRepo
    {
        decimal GetTotalSales_day(DateTime day, int shopId);
        decimal GetTotalSales_month(DateTime day, int shopId);

        decimal GetTotalExpenses_day(DateTime day, int shopId);
        decimal GetTotalExpenses_month(DateTime day, int shopId);

        decimal GetProfit_day(DateTime day, int shopId);
        decimal GetProfit_month(DateTime day, int shopId);


    }

    public class AccountingRepo : IAccountingRepo
    {
        private readonly KaylaaDataContext dbContext;

        public AccountingRepo(KaylaaDataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public decimal GetProfit_day(DateTime day, int shopId)
        {
         
            var GenTotalProfit = dbContext.ShoppingCarts.Where(c => c.salesdate.Day == day.Day && c.shopId==shopId)
                     .Select(s => s.totalProfit).Sum();

            return GenTotalProfit;
            
        }

        public decimal GetProfit_month(DateTime day, int shopId)
        {
            var GenTotalProfit = dbContext.ShoppingCarts.Where(c => c.salesdate.Month == day.Month && c.shopId == shopId)
                      .Select(s => s.totalProfit).Sum();

            return GenTotalProfit;
        }

        public decimal GetTotalExpenses_day(DateTime day, int shopId)
        {
            var alldaily_expenses = dbContext.Expenses.Where(e => e.date == day.Date && e.shopId == shopId)
                      .Select(r => r.amount).Sum();


            return alldaily_expenses; 
        }

        public decimal GetTotalExpenses_month(DateTime day, int shopId)
        {
            var allmontly_expenses = dbContext.Expenses.Where(e => e.date.Month == day.Date.Month && e.shopId == shopId)
                     .Select(r => r.amount).Sum();

            return allmontly_expenses; 
        }

        public decimal GetTotalSales_day(DateTime day, int shopId)
        {
            var GenTotalSales = dbContext.ShoppingCarts.Where(c => c.salesdate.Day == day.Day && c.shopId == shopId)
                      .Select(s => s.totalPrice).Sum();

            return GenTotalSales;
        }

        public decimal GetTotalSales_month(DateTime day, int shopId)
        {
            var GenTotalSales = dbContext.ShoppingCarts.Where(c => c.salesdate.Month == day.Month && c.shopId == shopId)
                        .Select(s => s.totalPrice).Sum();

            return GenTotalSales;
        }
    }
}