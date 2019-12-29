using System;
using System.Linq;

namespace KaylaaShop.Data
{
    public interface IAccountingRepo
    {
        double GetTotalSales_day(DateTime day, int shopId);
        double GetTotalSales_month(DateTime day, int shopId);

        double GetTotalExpenses_day(DateTime day, int shopId);
        double GetTotalExpenses_month(DateTime day, int shopId);

        double GetProfit_day(DateTime day, int shopId);
        double GetProfit_month(DateTime day, int shopId);


    }

    public class AccountingRepo : IAccountingRepo
    {
        private readonly KaylaaDataContext dbContext;

        public AccountingRepo(KaylaaDataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public double GetProfit_day(DateTime day, int shopId)
        {
            double total = 0D;

            var GenTotalProfit = dbContext.ShoppingCarts.Where(c => c.salesdate.Day == day.Day && c.shopId == shopId)
                     .Select(s => s.totalProfit).ToList();

            foreach (var item in GenTotalProfit)
            {
                total += (double)item;
            }

            return total;

        }

        public double GetProfit_month(DateTime day, int shopId)
        {
            double total = 0D;
            var GenTotalProfit = dbContext.ShoppingCarts.Where(c => c.salesdate.Month == day.Month && c.shopId == shopId)
                      .Select(s => s.totalProfit).ToList();

            foreach (var item in GenTotalProfit)
            {
                total += (double)item;
            }

            return total;
        }

        public double GetTotalExpenses_day(DateTime day, int shopId)
        {
            double total = 0D;

            var alldaily_expenses = dbContext.Expenses.Where(e => e.date == day.Date && e.shopId == shopId)
                      .Select(r => r.amount).ToList();

            foreach (var item in alldaily_expenses)
            {
                total += (double)item;
            }

            return total;
        }

    public double GetTotalExpenses_month(DateTime day, int shopId)
    {
            double total = 0D;
            var allmontly_expenses = dbContext.Expenses.Where(e => e.date.Month == day.Date.Month && e.shopId == shopId)
                 .Select(r => r.amount).ToList();

            foreach (var item in allmontly_expenses)
            {
                total += (double)item;
            }

            return total;
        }

    public double GetTotalSales_day(DateTime day, int shopId)
        {
            double total = 0D;

            var GenTotalSales = dbContext.ShoppingCarts.Where(c => c.salesdate.Day == day.Day && c.shopId == shopId)
                      .Select(s => s.totalPrice).ToList();

            foreach (var item in GenTotalSales)
            {
                total += (double)item;
            }

            return total ;
        }

        public double GetTotalSales_month(DateTime day, int shopId)
        {
            double total = 0D;

            var GenTotalSales = dbContext.ShoppingCarts.Where(c => c.salesdate.Month == day.Month && c.shopId == shopId)
                        .Select(s => s.totalPrice).ToList();

            foreach (var item in GenTotalSales)
            {
                total += (double)item;
            }

            return total;
        }
    }
}