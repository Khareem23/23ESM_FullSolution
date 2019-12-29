using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaylaaShop.Core;
using KaylaaShop.Data;
using KaylaaShop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace KaylaaShop.Pages.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IKaylaaRepository<staff> userRepo;

        private readonly IUserRepo usersingleRepo;
        private readonly IHttpContextAccessor httpcontext;
        private readonly IConfiguration config;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IPasswordHasher<IdentityUser> passwordHasher;
        private readonly UserManager<IdentityUser> userManager;

        public UserController(IKaylaaRepository<staff> userRepo, SignInManager<IdentityUser> signInManager,
            IPasswordHasher<IdentityUser> passwordHasher, UserManager<IdentityUser> userManager , 
            IUserRepo usersingleRepo,IHttpContextAccessor httpcontext,IConfiguration config)
        {
            this.userRepo = userRepo;
            this.signInManager = signInManager;
            this.passwordHasher = passwordHasher;
            this.userManager = userManager;
            this.usersingleRepo = usersingleRepo;
            this.httpcontext = httpcontext;
            this.config = config;
        }


        [HttpGet]
        [Route("getserverhost")]
        public IActionResult GetServerHost()
        {
            var root = config.GetSection("ServerRoot");
            var rootkeyvalue =  root.GetValue<string>("HostUrl");
            return Ok(rootkeyvalue);
        }


        [HttpGet]
        [Route("logout")]
        public async Task<bool> Logout()
        {
           httpcontext.HttpContext.Session.Clear();
           await signInManager.SignOutAsync();
            return true;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var allStaffs = userRepo.GetAll();
            return Ok(allStaffs);
        }

        [HttpGet("{id}")]
        public IActionResult GetStaff(string id)
        {
            var staff= userRepo.GetById(id);
            return Ok(staff);
        }

        [HttpPost]
        public IActionResult Add([FromBody] UserViewModel userVM)
        {

            if (userVM.user.Id == "")
            {
                if (userRepo.IsEmailExist(userVM.user.Email))
                {
                    var payload2 = new { status = "User already exist" };
                    return Ok(payload2);
                }

                var newUser = userRepo.Add(userVM.user);
                userRepo.Commit();
                var payload = new { name = newUser.fullName, status = "Added" };
                return Ok(payload);     // Status 200 : OK , 

            }
            else
            {

                var UserfromDB = usersingleRepo.GetUserById(userVM.user.Id);

                UserfromDB.fullName = userVM.user.fullName;
                UserfromDB.Email = userVM.user.Email;
                UserfromDB.NormalizedEmail =  userVM.user.Email.ToUpper();

                if(userVM.password != "")
                {
                    UserfromDB.PasswordHash = passwordHasher.HashPassword(userVM.user, userVM.password);
                }
                


                UserfromDB.PhoneNumber = userVM.user.PhoneNumber;
                UserfromDB.gender = userVM.user.gender;
                UserfromDB.address = userVM.user.address;
                UserfromDB.gaurantorName = userVM.user.gaurantorName;
                UserfromDB.gaurantorPhoneNumber = userVM.user.gaurantorPhoneNumber;
                UserfromDB.shopId = userVM.user.shopId;

                var updatedStaff = userRepo.Update(UserfromDB);
                userRepo.Commit();
                var payload = new { name = updatedStaff.fullName, status = "Updated" };
                return Ok(payload);
            }


        }


        [HttpDelete("{id}")] 
        public IActionResult Delete(string id)
        {
            userRepo.DeleteByStringID(id);
            userRepo.Commit();
            return NoContent();
        }

    }
}