using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaylaaShop.Core;
using KaylaaShop.Data;
using KaylaaShop.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KaylaaShop.Pages.Kaylaa
{

    [KaylaCustomAuth("AdminStaff")]
    //[Authorize(Roles = "AdminStaff")]
    public class AccountingModel : PageModel
    {
        private readonly IAccountingRepo accountRepo;
        IKaylaaRepository<Shop> shopRepo;
        public List<SelectListItem> allShops;

        public AccountingModel(IAccountingRepo accountRepo, IKaylaaRepository<Shop> shopRepo)
        {
            this.accountRepo = accountRepo;
            this.shopRepo = shopRepo;
        }
        public bool IsMonth { get; set; }
        public bool IsMonth_Profit { get; set; }
        public bool IsMonth_Expense { get; set; }
        public double  Result { get; set; }
        public double Result_Expenses { get; set; }
        public double Result_Profits { get; set; }

        public DateTime salesdate { get; set; }
        public DateTime profitdate { get; set; }
        public DateTime expensedate { get; set; }

        [BindProperty]
        public int shopId { get; set; }
        public void OnGet()
        {
            allShops = shopRepo.GetAll().Select(s => new SelectListItem() { Text = s.ShopName, Value = s.Id.ToString() }).ToList();
            if(allShops != null)
            {
                var allShopsInSession = ComplexTypeSerializerHelper.SerializeObject<SelectListItem>(allShops);
                TempData["allShopsInSession"] = allShopsInSession;

                if (ViewData["Result"] != null)
                Result = Convert.ToDouble(ViewData["Result"]);
                ViewData["Result"] = null;
            }
           
        }

        public void OnPostProcessSales(DateTime salesdate ,bool IsMonth)
        {
            if (!ModelState.IsValid)
            {
                ViewData["msg"] = "Invalid Inputs: Select Date and Shop ";
                return;
            }

            if (TempData["allShopsInSession"] != null)
            allShops = ComplexTypeSerializerHelper.DeserializeObject<SelectListItem>(TempData["allShopsInSession"].ToString());
            else
            {
                allShops = shopRepo.GetAll().Select(s => new SelectListItem() { Text = s.ShopName, Value = s.Id.ToString() }).ToList();
            }
                

            if (allShops != null)
            {
                   if (IsMonth.Equals(false))
                        {
                            Result = accountRepo.GetTotalSales_day(salesdate,shopId);
                            ViewData["Result"] = Result;
                        }
                        else
                        {
                            Result = accountRepo.GetTotalSales_month(salesdate,shopId);
                            ViewData["Result"] = Result;
                        }
            }
          
        }

        public void OnPostProcessExpenses(DateTime expensedate, bool IsMonth_Expense)
        {
            if (!ModelState.IsValid)
            {
                ViewData["msg"] = "Invalid Inputs: Select Date and Shop ";
                return;
            }


            if (TempData["allShopsInSession"] != null)
                allShops = ComplexTypeSerializerHelper.DeserializeObject<SelectListItem>(TempData["allShopsInSession"].ToString());
            else
            {
                allShops = shopRepo.GetAll().Select(s => new SelectListItem() { Text = s.ShopName, Value = s.Id.ToString() }).ToList();
            }
            if (allShops !=null)
            {
                          if (IsMonth_Expense.Equals(false))
                            {
                                Result_Expenses = accountRepo.GetTotalExpenses_day(expensedate,shopId);
                                ViewData["Result"] = Result_Expenses;

                            }
                            else
                            {
                                Result_Expenses = accountRepo.GetTotalExpenses_month(expensedate,shopId);
                                ViewData["Result"] = Result_Expenses;
                            }
            }
           
           
        }

        public void OnPostProcessProfits(DateTime profitdate, bool IsMonth_Profit)
        {
            if (!ModelState.IsValid)
            {
                ViewData["msg"] = "Invalid Inputs: Select Date and Shop ";
                return;
            }


            if (TempData["allShopsInSession"] != null)
                allShops = ComplexTypeSerializerHelper.DeserializeObject<SelectListItem>(TempData["allShopsInSession"].ToString());
            else
            {
                allShops = shopRepo.GetAll().Select(s => new SelectListItem() { Text = s.ShopName, Value = s.Id.ToString() }).ToList();
            }
            if (allShops != null)
            {
                          if (IsMonth_Profit.Equals(false))
                            {
                                Result_Profits = accountRepo.GetProfit_day(profitdate, shopId);
                                ViewData["Result"] = Result_Profits;
                            }
                            else
                            {
                                Result_Profits = accountRepo.GetProfit_month(profitdate,shopId);
                                ViewData["Result"] = Result_Profits;
                            }
            }
            
        }

        }
}