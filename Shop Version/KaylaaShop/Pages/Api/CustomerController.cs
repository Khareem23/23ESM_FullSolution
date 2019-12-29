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
    public class CustomerController : ControllerBase
    {
        private readonly IKaylaaRepository<Customer> repo;

        public CustomerController(IKaylaaRepository<Customer> _repo)
        {
            this.repo = _repo;
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var allCustomers = repo.GetById(id);
            return Ok(allCustomers);
        }


        [HttpPost]
        public IActionResult Add([FromBody]Customer Customer)
        {
            
            if(string.IsNullOrWhiteSpace(Customer.Name) || string.IsNullOrWhiteSpace(Customer.phoneNumber))
            {
                var payload = new { name = "Empty Input", status = "One of Name, Email, Phone Number or Address was empty" };
                return Ok(payload);
            }
               
            if (Customer.Id == 0)
            {


                if (repo.IsEmailExist(Customer.email))
                {
                    var payload2 = new { status = "User Already Exist" };
                    return Ok(payload2);
                }

                var newcustomer = repo.Add(Customer);
                int result = repo.Commit();
                var payload = new { name = newcustomer.Name, status = "Added" };
                return Ok(payload);     // Status 200 : OK , 

            }
            else
            {
                var updatedcustomer = repo.Update(Customer);
                repo.Commit();
                var payload = new { name = updatedcustomer.Name, status = "Updated" };
                return Ok(payload);
            }


        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var Allcustomers = repo.GetAll();
            return Ok(Allcustomers);
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