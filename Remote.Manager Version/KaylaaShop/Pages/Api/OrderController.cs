using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaylaaShop.Core;
using KaylaaShop.Data;
using KaylaaShop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace KaylaaShop.Pages.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IKaylaaRepository<ShoppingCart> shopCartRepo;
        private readonly IOrderRepository orderRepo;
        private readonly IKaylaaRepository<Customer> custRepo;
        private readonly IKaylaaRepository<staff> staffRepo;
        private readonly IKaylaaRepository<Product> productRepo;
        private readonly IKaylaaRepository<ShoppingCartItem> soldItemRepo;
        private readonly IShoppingCartRepo shopsinglerepo;
        private readonly IConfiguration configuration;

        public OrderController(IKaylaaRepository<ShoppingCart> _shopCartRepo, IOrderRepository orderRepo,
                              IKaylaaRepository<Customer> custRepo, IKaylaaRepository<staff> staffRepo,
                              IKaylaaRepository<Product> productRepo,
                              IKaylaaRepository<ShoppingCartItem> soldItemRepo,
                              IShoppingCartRepo shopsinglerepo,IConfiguration configuration)
        {
            this.orderRepo = orderRepo;
            this.custRepo = custRepo;
            this.staffRepo = staffRepo;
            this.productRepo = productRepo;
            this.soldItemRepo = soldItemRepo;
            this.shopsinglerepo = shopsinglerepo;
            this.configuration = configuration;
            shopCartRepo = _shopCartRepo;
        }



        [HttpGet]
        [Route("getorder/{shopId}")]
        public IActionResult GetAll(int shopId)
        {
          

            var allOrders = orderRepo.GetAllCartsByShopId(shopId);

            List<OrderViewModel> AllNewOrder = new List<OrderViewModel>();

          

            foreach(var order in allOrders)
            {
                var customer = custRepo.GetById(order.CustomerId);

                var staff= staffRepo.GetById(order.StaffId);


                OrderViewModel orderVm = new OrderViewModel()
                {
                    ShoppingCartId = order.ShoppingCartId ,
                    customerName = customer.Name,
                    staffName = staff.fullName,
                    totalPrice = order.totalPrice ,
                    totalQuantity = order.totalQuantity , 
                    totalProfit = order.totalProfit,
                    salesdate = order.salesdate ,
                    shopId = order.shopId

                };

                AllNewOrder.Add(orderVm);
            }

            return Ok(AllNewOrder);
        }


        [HttpGet]
        [Route("getsolditem/type/{type}")]
        public IActionResult GetAllSoldItems(string type)
        {
            decimal PriceReductionPercent = 0M;
            decimal newSellingPrice = 0M;
            decimal newCostPrice = 0M;

            if (type == "official")
            {
                PriceReductionPercent = decimal.Parse(configuration.GetSection("ProjectSettings")["PriceReductionPercentage"]);
            }


            List<SoldProductViewModel> soldproductList = new List<SoldProductViewModel>();


            var allsolditems = soldItemRepo.GetAll();

            decimal VatPercent = decimal.Parse(configuration.GetSection("ProjectSettings")["VatPercentage"]);
            

         

            foreach (var item in allsolditems)
            {
                var product = productRepo.GetById(item.ProductId);
                var orderDate = shopsinglerepo.GetSalesdateByCartId(item.ShoppingCartId);

                if (PriceReductionPercent != 0)
                {
                    newSellingPrice = item.AmountSold - (item.AmountSold * PriceReductionPercent);
                    newCostPrice = product.costPrice - (product.costPrice * PriceReductionPercent);
                }
                else
                {
                    newSellingPrice = item.AmountSold;
                    newCostPrice = product.costPrice;
                }

                var soldproduct = new SoldProductViewModel()
                {
                    Quantity = item.quantity,
                    Name = product.Name,
                    ProductCode = product.prodCode,
                    ProductImageUrl = product.productImageUrl,
                    CostPrice = newCostPrice,
                    TotalSellingPrice = newSellingPrice * item.quantity,
                    VAT = newSellingPrice * item.quantity * VatPercent,
                    AmountSold = newSellingPrice,
                    DateSold = orderDate.ToShortDateString()
                };

                soldproductList.Add(soldproduct);
            }


            return Ok(soldproductList);
        }

        [HttpGet]
        [Route("getsolditembyshop/{shopId}/type/{type}/date/{datefromClient?}")]
        public IActionResult GetAllSoldItemsByShop(int shopId, string type, string datefromClient)
        {

            decimal PriceReductionPercent = 0M;
            decimal newSellingPrice = 0M;
            decimal newCostPrice = 0M;

            if (type=="official")
            {
                PriceReductionPercent = decimal.Parse(configuration.GetSection("ProjectSettings")["PriceReductionPercentage"]); 
            }
           
            
            List<SoldProductViewModel> soldproductList = new List<SoldProductViewModel>();
            

            DateTime date = (datefromClient == "Invalid Date" || datefromClient == "")? DateTime.Now : DateTime.Parse(datefromClient);

           // DateTime date = DateTime.Parse(datefromClient);

            var allsoldbyshop = orderRepo.GetSoldItemsByShop(shopId,date);

            decimal VatPercent = decimal.Parse(configuration.GetSection("ProjectSettings")["VatPercentage"]);

           //check null valu
           if(allsoldbyshop != null)
            {
                 foreach (var item in allsoldbyshop)
                            {
                                var product = productRepo.GetById(item.ProductId);
                                var orderDate = shopsinglerepo.GetSalesdateByCartId(item.ShoppingCartId);

                                    if (product != null)
                                    {
                                             if(PriceReductionPercent != 0)
                                                {
                                                     newSellingPrice = item.AmountSold - (item.AmountSold * PriceReductionPercent);
                                                     newCostPrice = product.costPrice - (product.costPrice * PriceReductionPercent);
                                                }
                                                else
                                                {
                                                    newSellingPrice = item.AmountSold;
                                                    newCostPrice = product.costPrice;
                                                }

                                                var soldproduct = new SoldProductViewModel()
                                                {
                                                   Quantity = item.quantity,
                                                   Name = product.Name,
                                                   ProductCode = product.prodCode,
                                                   ProductImageUrl = product.productImageUrl,
                                                   CostPrice = newCostPrice ,
                                                   TotalSellingPrice = newSellingPrice * item.quantity ,
                                                   VAT = newSellingPrice * item.quantity * VatPercent,
                                                   AmountSold = newSellingPrice,
                                                   DateSold = orderDate.ToShortDateString()
                                                };

                                                soldproductList.Add(soldproduct);
                                    }

                              
                            }

            }

           
           
            return Ok(soldproductList);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var allOrders = shopCartRepo.GetAll();

            List<OrderViewModel> AllNewOrder = new List<OrderViewModel>();



            foreach (var order in allOrders)
            {
                var customer = custRepo.GetById(order.CustomerId);

                var staff = staffRepo.GetById(order.StaffId);


                OrderViewModel orderVm = new OrderViewModel()
                {
                    ShoppingCartId = order.ShoppingCartId,
                    customerName = customer.Name,
                    staffName = staff.fullName,
                    totalPrice = order.totalPrice,
                    totalQuantity = order.totalQuantity,
                    totalProfit = order.totalProfit,
                    salesdate = order.salesdate,
                    shopId = order.shopId
                    

                };

                AllNewOrder.Add(orderVm);
            }

            return Ok(AllNewOrder);
        }


        [HttpGet("{OrderId}")]
        public IActionResult GetCartItems(string OrderId)
        {
            var allitems = orderRepo.GetCartItems(OrderId);
            List<ItemViewModel> ItemVMList = new  List<ItemViewModel>();

            foreach (var item in allitems)
            {
                var product = productRepo.GetById(item.ProductId);
                ItemViewModel itemVM = new ItemViewModel()
                {
                    ProductName = product.Name,
                    ProductCost = item.AmountSold,
                    ProductQty = item.quantity,
                    
                };
                ItemVMList.Add(itemVM);
            }
            return Ok(ItemVMList);
        }
    }
}