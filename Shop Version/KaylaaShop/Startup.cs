using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaylaaShop.Infastructure;
using KaylaaShop.Core;
using KaylaaShop.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using KaylaaShop.Pages.Kaylaa;
using Microsoft.Extensions.Options;
using KaylaaShop.Helpers;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.Mvc.Razor;


namespace KaylaaShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration )
        {
            Configuration = configuration;
        }

       
     
        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        //  public const string CookieScheme = "KaylaCookieScheme";
        //private readonly IWebHostEnvironment env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentity<IdentityUser, IdentityRole>()
              .AddEntityFrameworkStores<KaylaaDataContext>();


            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
           
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            //    //options.LoginPath = "/Index";
            //   //options.AccessDeniedPath = "/AccessDenied";

            //});


            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("https://kaylabeau.herokuapp.com","http://localhost:5000", "http://localhost:5001");                 
                });
            });



            // Adding Global Cookie Policy 
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContextPool<KaylaaDataContext>(options => options.UseSqlite(Configuration.GetConnectionString("MakeUpResidenceDb")));
              


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));

            services.AddScoped<ILocalFileUpload, LocalFileUpload>();
            services.AddScoped<IShopRepository, ShopRepository>();
            services.AddScoped<IAccountingRepo, AccountingRepo>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<ICustomerRepo, CustomerRepo>();
            services.AddScoped<IShoppingCartRepo, ShoppingCartRepo>();
            services.AddScoped<IProductRepo, ProductRepo>();
            // services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IKaylaaRepository<ProductBrand>, KaylaaRepository<ProductBrand>>();
            services.AddScoped<IKaylaaRepository<ProductColor>, KaylaaRepository<ProductColor>>();
            services.AddScoped<IKaylaaRepository<ProductCountry>, KaylaaRepository<ProductCountry>>();
            services.AddScoped<IKaylaaRepository<ProductCategory>, KaylaaRepository<ProductCategory>>();

            services.AddScoped<IKaylaaRepository<Customer>, KaylaaRepository<Customer>>();
            services.AddScoped<IKaylaaRepository<Expenses>, KaylaaRepository<Expenses>>();
            services.AddScoped<IKaylaaRepository<Product>, KaylaaRepository<Product>>();
            services.AddScoped<IKaylaaRepository<ShoppingCartItem>, KaylaaRepository<ShoppingCartItem>>();
            services.AddScoped<IKaylaaRepository<ShoppingCart>, KaylaaRepository<ShoppingCart>>();
            services.AddScoped<IKaylaaRepository<staff>, KaylaaRepository<staff>>();
            services.AddScoped<IKaylaaRepository<Shop>, KaylaaRepository<Shop>>();

            services.AddScoped<IDeleteUnavailableProducts, DeleteUnavailableProducts>();

            services.AddSession();

            services.AddControllers().AddNewtonsoftJson(); 
            services.AddRazorPages();

            services.AddMvc().AddRazorRuntimeCompilation();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddMvc()
           .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
           .AddSessionStateTempDataProvider();

          

           
        }

      

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {

                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                        if (exceptionHandlerFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global exception logger");
                            logger.LogError(500,
                                exceptionHandlerFeature.Error,
                                exceptionHandlerFeature.Error.Message);

                            context.Response.StatusCode = 500;
                            await context.Response.WriteAsync($"An unexpected fault happened. Try again later. {exceptionHandlerFeature.Error.Message}");
                        }
                        else
                        {
                            context.Response.StatusCode = 500;
                            await context.Response.WriteAsync($"An unexpected fault happened. Try again later");
                        }


                    });
                });

               // app.UseExceptionHandler("/Error");
            }
          
           

        

            // For serving static files in wwwroot folder 
            app.UseStaticFiles();

            //For Cookie Settings                   
            app.UseCookiePolicy(); 
            
            // To Use Session in your Application 
            app.UseSession();


          //  app.UseCors(_myAllowSpecificOrigins);

            //app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            // To Use Identity Authentication in your  Application 
            app.UseAuthentication();
            app.UseAuthorization();
         
            //For MVC, Controllers, and all
            //app.UseMvc();


            //For Dotnet 3.0 only 
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
            //'UseMvc', please set 'MvcOptions.EnableEndpointRouting = false' inside 'ConfigureServices'.
            //

        }





    }
}
