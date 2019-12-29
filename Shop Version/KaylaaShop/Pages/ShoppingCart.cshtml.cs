using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KaylaaShop.Core;
using KaylaaShop.Data;
using KaylaaShop.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KaylaaShop.Pages.Kaylaa
{
    [KaylaCustomAuth("")]
   
    //  [Authorize]
    public class ShoppingCartModel : PageModel
    {
        private readonly IKaylaaRepository<Product> productRepo;
        private readonly IShoppingCartRepo shopRepo;
        private readonly ICustomerRepo custRepo;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IUserRepo userRepo;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IProductRepo prodspecificRepo;

        [BindProperty]
        public ShoppingCartItem item { get; set; }

        public decimal Total;
        public int TotalQty;

        public decimal TotalProfit;

        public decimal NewAmountSold { get; set; }

        public int productId { get; set; }





        public List<ShoppingCartItem> Cart { get; set; }

        public int StaffShopId { get; set; }

        public ShoppingCartModel(IKaylaaRepository<Product> prodRepo ,IShoppingCartRepo shopRepo,
            ICustomerRepo custRepo, UserManager<IdentityUser> _userManager,IUserRepo userRepo,
            SignInManager<IdentityUser> signInManager,IHttpContextAccessor httpContextAccessor,IProductRepo prodspecificRepo)
        {
            this.productRepo = prodRepo;
            this.shopRepo = shopRepo;
            this.custRepo = custRepo;
            userManager = _userManager;
            this.userRepo = userRepo;
            this.signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
            this.prodspecificRepo = prodspecificRepo;
        }

        public void OnGet()
        {
           

            Cart = SessionHelper.GetObjectFromJSON<List<ShoppingCartItem>>(HttpContext.Session, "Cart");
            if(Cart != null)
            {
                Total = Cart.Sum(item => item.AmountSold * item.quantity);
                TotalQty = Cart.Sum(item => item.quantity);
            }
          
        }




        public IActionResult OnGetAddToCart(int productId,int quantity)
        {
            var prod = productRepo.GetById(productId);

            if (prod == null)
            {
                RedirectToPage("/NotFound");
            }


            Cart = SessionHelper.GetObjectFromJSON<List<ShoppingCartItem>>(HttpContext.Session, "Cart");

            if (Cart == null)
            {
                Cart = new List<ShoppingCartItem>();

                Cart.Add(new ShoppingCartItem
                {
                    AmountSold = prod.NormalSellingPrice,
                    product = prod,
                    quantity = quantity 
                });

                SessionHelper.SetObjectAsJSON(HttpContext.Session, "Cart", Cart);

            }
            else
            {
                int index = Exists(Cart, productId);

                if(index == -1)
                {
                    Cart.Add(new ShoppingCartItem
                    {
                        AmountSold = prod.NormalSellingPrice,
                        product = prod,
                        quantity = quantity 
                        //profit = quantity * (prod.NormalSellingPrice - prod.costPrice)

                    });
                    SessionHelper.SetObjectAsJSON(HttpContext.Session, "Cart", Cart);

                }
                else
                {
                    int qtyavailable = prodspecificRepo.GetProductQuantity(Cart[index].product.Id);

                    int QtyFromCart = Cart[index].quantity;

                    int QuantityLeft = qtyavailable - QtyFromCart;

                    if(quantity <= QuantityLeft)
                    {
                        Cart[index].quantity += quantity;
                        SessionHelper.SetObjectAsJSON(HttpContext.Session, "Cart", Cart);
                    }
                    else
                    {
                        ViewData["status"] = "The Quantity Requested more than Total Quantity Available";
                    }


                    //if (Cart[index].quantity <= qtyavailable)
                    //{
                    //    Cart[index].quantity += quantity;
                    //    SessionHelper.SetObjectAsJSON(HttpContext.Session, "Cart", Cart);
                    //}

                   
                }

               
            }
            return Page();
               
                
            //return RedirectToPage("ShoppingCart");
        }


        private int Exists(List<ShoppingCartItem> Cart , int id)
        {
            for (var i=0; i< Cart.Count; i++)
            {
                if(Cart[i].product.Id == id)
                {
                    return i;
                }
            }
            return -1;
        }


        public IActionResult OnGetGiveDiscount(int id)
        {
            Cart = SessionHelper.GetObjectFromJSON<List<ShoppingCartItem>>(HttpContext.Session, "Cart");

            for (var i = 0; i < Cart.Count; i++)
            {
               
                if (Cart[i].product.Id == id)
                {
                    Cart[i].AmountSold = Cart[i].product.discountSellingPrice;
                }
            }

           

            Total = Cart.Sum(item => item.AmountSold * item.quantity);
            TotalQty = Cart.Sum(item => item.quantity);

            SessionHelper.SetObjectAsJSON(HttpContext.Session, "Cart", Cart);

            return Page();
        }

        public IActionResult OnPostUpdatePriceSold(int productId, decimal NewAmountSold)
        {
            Cart = SessionHelper.GetObjectFromJSON<List<ShoppingCartItem>>(HttpContext.Session, "Cart");
            for (var i = 0; i < Cart.Count; i++)
            {

                if (Cart[i].product.Id == productId)
                {
                    Cart[i].AmountSold = NewAmountSold;
                }
            }

           

            Total = Cart.Sum(item => item.AmountSold * item.quantity);
            TotalQty = Cart.Sum(item => item.quantity);
            SessionHelper.SetObjectAsJSON(HttpContext.Session, "Cart", Cart);


            return Page();

        }
        public IActionResult OnGetDelete ( int id)
        {
            Cart = SessionHelper.GetObjectFromJSON<List<ShoppingCartItem>>(HttpContext.Session, "Cart");
            int index = Exists(Cart, id);
            Cart.RemoveAt(index);
            SessionHelper.SetObjectAsJSON(HttpContext.Session, "Cart", Cart);

            return RedirectToPage("ShoppingCart");

        }

        public async Task<IActionResult> OnPostCheckout(string myPhoneNumber)
        {

            string staffName = User.Identity.Name;
            int  StaffShopId = userRepo.GetStaffShopId(staffName);

            if (StaffShopId > 0)
            {

                Cart = SessionHelper.GetObjectFromJSON<List<ShoppingCartItem>>(HttpContext.Session, "Cart");
                Total = Cart.Sum(item => item.AmountSold * item.quantity);
                TotalQty = Cart.Sum(item => item.quantity);

                foreach (ShoppingCartItem item in Cart)
                {
                    TotalProfit += item.quantity * (item.AmountSold - item.product.costPrice); 
                } 

                var AuthState = User.Identity.IsAuthenticated;

                var loggedInUser = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);

                string[] name_Phone_array = myPhoneNumber.Split(':');

                Customer cust = custRepo.GetCustomerByPhoneNo(name_Phone_array[1]);

                var mycart = new ShoppingCart()
                {
                    Id = Guid.NewGuid().ToString(),
                    salesdate = DateTime.Now,
                    totalQuantity = TotalQty,
                    totalPrice = Total,
                    totalProfit = TotalProfit,
                    shoppingItems = Cart,
                    customer = cust,
                    staff = (staff)loggedInUser,
                    shopId = StaffShopId

                };
                shopRepo.SaveCart(mycart);
                shopRepo.Commit();
                ViewData["status"] = "Order Checked Out Successfully !";
                Cart = null;
                SessionHelper.SetObjectAsJSON(HttpContext.Session, "Cart", Cart);
            }
            else
                ViewData["Error"] = "You are a Manager, Only staffs have the right to sell. Kindly login as a Staff";
            return Page();
           
        }

    }
}