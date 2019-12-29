using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KaylaaShop.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public IActionResult OnGet()
        {

            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewData["ExceptionPath"] = exceptionDetails.Path;
            ViewData["ExceptionMessage"] = exceptionDetails.Error.Message;
            ViewData["StackTrace"] = exceptionDetails.Error.StackTrace;
            // RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return Page();
         
        }

     
    }
}
