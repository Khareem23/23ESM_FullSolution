using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using KaylaaShop.Core;
using KaylaaShop.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KaylaaShop.Data;

namespace KaylaaShop.Pages.Kaylaa
{

    [KaylaCustomAuth("AdminStaff")]
   // [Authorize(Roles = "AdminStaff")]
    public class StaffModel : PageModel
    {
        private readonly IKaylaaRepository<Shop> shopRepo;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public List<SelectListItem> allShops;

        public string statusMsg;

        public StaffModel(IKaylaaRepository<Shop> shopRepo ,UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.shopRepo = shopRepo;
            userManager = _userManager;
            signInManager = _signInManager;
            this.roleManager = roleManager;
        }

        [BindProperty]
        public RegisterModel regInput { get; set; }

        public string returnUrl{ get; set; }

        public List<SelectListItem> genderlist;

        public List<SelectListItem> roleList;

        public void OnGet( string _returnUrl=null)
        {
            allShops = shopRepo.GetAll().Select(s => new SelectListItem() { Text = s.ShopName, Value = s.Id.ToString() }).ToList();

            genderlist = new List<SelectListItem>() {
                new SelectListItem{Value="Male" , Text="M"},
                new SelectListItem{Value="Female" , Text="F"}
            };

            roleList = new List<SelectListItem>() {

                new SelectListItem{Value="AdminStaff" , Text="Admin/Manager"},
                new SelectListItem{Value="RegularStaff" , Text="Staff/Workers"}
               
            };

        }


        public async Task<IActionResult> OnPostAsync(string returnUrl=null)
        {
            //var newUser;
           //this.CreateRole();

            if (ModelState.IsValid)
            {

                var user = userManager.FindByEmailAsync(regInput.email).Result;
                
                if(user == null)
                {
                    var newUser = new staff()
                    {
                        UserName = regInput.username,
                        Email = regInput.email,
                        PhoneNumber = regInput.Phonenumber,
                        fullName = regInput.fullname,
                        address = regInput.address,
                        gaurantorName = regInput.gaurantorname,
                        gaurantorPhoneNumber = regInput.gaurantorphoneno,
                        gender = regInput.gender,
                        shopId = regInput.shopId

                    };

                    var result =  userManager.CreateAsync(newUser, regInput.password).Result;
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newUser, regInput.roletype);
                        statusMsg = "Staff Added Successfully";

                        return Page();
                    }
                    else statusMsg = "Failed to Add Staff"+result.Errors.ToString();


                }
                else
                    statusMsg = "User Already Exist !";

            }

            return Page();
        }

        public async Task CreateRole()
        {
            string[] roleNames = { "AdminStaff", "RegularStaff" };
            IdentityResult roleResult;
            foreach(var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);

                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        public class RegisterModel
        {

            [Required]
            [Display(Name="Username")]
            public string username { get; set; }

            [Required]
            [Display(Name = "Email")]
            [EmailAddress]
            public string email { get; set; }

            [Required]
            [Display(Name = "Phone Number")]
            public string Phonenumber { get; set; }

            [Required]
            [StringLength(20, ErrorMessage ="Invalid Password")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string password { get; set; }


            [Required]
            [StringLength(20, ErrorMessage = "Password do not match")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            public string confirmpassword { get; set; }


            [Required]
            [Display(Name = "Full Name")]
            public string fullname { get; set; }

            [Required]
            [Display(Name = "Address")]
            public string address { get; set; }

            [Required]
            [Display(Name = "Gaurantor Name")]
            public string gaurantorname { get; set; }

            [Required]
            [Display(Name = "Gaurantor Phone Number")]
            public string gaurantorphoneno { get; set; }

            [Required]
            [Display(Name = "Gender")]
            public string gender { get; set; }

            [Required]
            [Display(Name = "User Type")]
            public string roletype { get; set; }

            [Required]
            [Display(Name = "Shop Id")]
            public int shopId { get; set; }



        }
    }
}