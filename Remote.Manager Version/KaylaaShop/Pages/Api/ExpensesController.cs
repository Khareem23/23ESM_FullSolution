using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaylaaShop.Core;
using KaylaaShop.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KaylaaShop.Pages.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IKaylaaRepository<Expenses> repo;
        private readonly IExpensesRepo expenseRepo;

        public ExpensesController(IKaylaaRepository<Expenses> _repo, IExpensesRepo expenseRepo)
        {
            this.repo = _repo;
            this.expenseRepo = expenseRepo;
        }



        [HttpPost]
        public IActionResult Add([FromBody]Expenses Expenses)
        {
            if(string.IsNullOrWhiteSpace(Expenses.Name))
            {
                var payload = new { name = "Empty Input", status = "One of Name, Amount or Date empty" };
                return Ok(payload);
               
            }

            if (Expenses.Id == 0)
            {
                var newExpenses = repo.Add(Expenses);
                repo.Commit();
                var payload = new { name = newExpenses.Name, status = "Added" };
                return Ok(payload);     // Status 200 : OK , 

            }
            else
            {
                var updatedExpenses = repo.Update(Expenses);
                repo.Commit();
                var payload = new { name = updatedExpenses.Name, status = "Updated" };
                return Ok(payload);
            }


        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var AllExpensess = repo.GetAll();
            return Ok(AllExpensess);
        }

        [HttpGet]
        [Route("getShopExpenses/{shopId}")]
        public IActionResult GetShopExpenses(int Shopid)
        {
            var AllshopExpensess = expenseRepo.GetShopExpenses(Shopid);
            return Ok(AllshopExpensess);
        }


        [HttpGet("{id}")]
        public IActionResult GetExpenses(int id)
        {
            var allExpenses = repo.GetById(id);
            return Ok(allExpenses);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            repo.Delete(id);
            repo.Commit();
            return NoContent();
        }

    }
}