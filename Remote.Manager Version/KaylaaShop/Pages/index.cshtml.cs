using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;



namespace KaylaaShop.Pages
{

    [AllowAnonymous]
    public class indexModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        [BindProperty]
        public LoginModel login { get; set; }

        public indexModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public void OnGet()
        {
           // return Page();
        }

        public async Task<IActionResult> OnPostLoginAsync(LoginModel login )
        {
            if (!ModelState.IsValid)
                return Page();
            var user = await userManager.FindByEmailAsync(login.email);
            //if(user==null)
            //{
            //    user = new IdentityUser { Email = "yemi@gmail.com", UserName = "yemi", PhoneNumber = "08090909090" };
            //    var r=await userManager.CreateAsync(user, "Shopapp1$");
            //    await userManager.AddToRoleAsync(user, "AdminStaff");
            //}
            if(user!= null)
            {
                var loginResult =  await signInManager.PasswordSignInAsync(user, login.password,true ,false);

                if (loginResult.Succeeded)
                {
                    return RedirectToPage("dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Email/Password Incorrect");
                    return Page();
                }
            }
            ModelState.AddModelError("", "User not Exist !");
            return Page();
        }


       

        public class LoginModel
        {
            public string email { get; set; }
            public string password { get; set; }

            public string returnUrl { get; set; }
        }

        
    }
}
