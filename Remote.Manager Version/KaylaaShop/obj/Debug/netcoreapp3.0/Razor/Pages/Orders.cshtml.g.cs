#pragma checksum "C:\Users\Khareem_Win_Usr\Documents\Visual Studio 2019\2 - YemisCopy_SyncVersion\Remote.Manager Version\KaylaaShop\Pages\Orders.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ff5d893074a3e1e9cde7234f0448cf07d265ba57"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(KaylaaShop.Pages.Pages_Orders), @"mvc.1.0.razor-page", @"/Pages/Orders.cshtml")]
namespace KaylaaShop.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Khareem_Win_Usr\Documents\Visual Studio 2019\2 - YemisCopy_SyncVersion\Remote.Manager Version\KaylaaShop\Pages\_ViewImports.cshtml"
using KaylaaShop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Khareem_Win_Usr\Documents\Visual Studio 2019\2 - YemisCopy_SyncVersion\Remote.Manager Version\KaylaaShop\Pages\_ViewImports.cshtml"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Khareem_Win_Usr\Documents\Visual Studio 2019\2 - YemisCopy_SyncVersion\Remote.Manager Version\KaylaaShop\Pages\Orders.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Khareem_Win_Usr\Documents\Visual Studio 2019\2 - YemisCopy_SyncVersion\Remote.Manager Version\KaylaaShop\Pages\Orders.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Khareem_Win_Usr\Documents\Visual Studio 2019\2 - YemisCopy_SyncVersion\Remote.Manager Version\KaylaaShop\Pages\Orders.cshtml"
using Microsoft.Extensions.Configuration;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ff5d893074a3e1e9cde7234f0448cf07d265ba57", @"/Pages/Orders.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0a02355490aab7d38820e1b316dd8f5007ab870f", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Orders : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("shopId"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control deepBlueBack whiteFont"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/library/datatables.net/js/jquery.dataTables.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/library/datatables.net-bs4/js/dataTables.bootstrap4.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/library/datatables.net-bs4/css/dataTables.bootstrap4.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("include", "Development", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("exclude", "Development", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.EnvironmentTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
            WriteLiteral("\n\n");
#nullable restore
#line 10 "C:\Users\Khareem_Win_Usr\Documents\Visual Studio 2019\2 - YemisCopy_SyncVersion\Remote.Manager Version\KaylaaShop\Pages\Orders.cshtml"
  
    ViewData["Title"] = "Orders";
    Layout = "~/Pages/Shared/_LayoutMain.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\n");
#nullable restore
#line 16 "C:\Users\Khareem_Win_Usr\Documents\Visual Studio 2019\2 - YemisCopy_SyncVersion\Remote.Manager Version\KaylaaShop\Pages\Orders.cshtml"
 if (User.Identity.IsAuthenticated && User.IsInRole("AdminStaff"))
{


#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""row"">
        <div class=""col-xl-12"">
            <div id=""viewAllOrder"" class=""kt-portlet kt-portlet--mobile"">


                <div class=""kt-portlet__head kt-portlet__head--lg deepBlueBack"">
                    <div class=""kt-portlet__head-label"">
                        <span class=""kt-portlet__head-icon"">
                            <i class=""flaticon-calendar""></i>
                        </span>
                        <h2 class=""kt-portlet__head-title kt-font-primary"">
                            All Sales Orders

                        </h2>


                    </div>

                    <div class=""kt-portlet__head-toolbar"">

                        <div class=""kt-portlet__head-actions"">
                            <div class=""row mt-4"">
                                <div class=""form-group"">
                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ff5d893074a3e1e9cde7234f0448cf07d265ba579160", async() => {
                WriteLiteral("\n                                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ff5d893074a3e1e9cde7234f0448cf07d265ba579458", async() => {
                    WriteLiteral(" Select Shop ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n                                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
#nullable restore
#line 42 "C:\Users\Khareem_Win_Usr\Documents\Visual Studio 2019\2 - YemisCopy_SyncVersion\Remote.Manager Version\KaylaaShop\Pages\Orders.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = Model.allShops;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                                </div>\n\n                            </div>\n\n                        </div>\n\n                    </div>\n                    <input id=\"serverUrl\" type=\"hidden\"");
            BeginWriteAttribute("value", " value=\"", 1644, "\"", 1702, 1);
#nullable restore
#line 52 "C:\Users\Khareem_Win_Usr\Documents\Visual Studio 2019\2 - YemisCopy_SyncVersion\Remote.Manager Version\KaylaaShop\Pages\Orders.cshtml"
WriteAttributeValue("", 1652, Configuration.GetSection("ServerRoot")["HostUrl"], 1652, 50, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />

                </div>

                <div class=""kt-portlet__body"">
                    <!--begin: Datatable -->
                    <table class=""table"" id=""allOrderTable"">
                        <tr></tr>
                    </table>
                    <!--end: Datatable -->
                </div>

            </div>

        </div>
    </div>
");
            WriteLiteral(@"    <div class=""modal fade"" id=""modal_order"" data-backdrop=""static"" tabindex=""-1"" role=""dialog"" aria-hidden=""true"">
        <div class=""modal-dialog"" role=""document"">
            <div class=""modal-content"">
                <div class=""modal-header deepBlueBack"">
                    <h5 class=""modal-title whiteFont"" id=""exampleModalLabel"">Get Order Items - <span id=""orderIdModal""> </span> </h5>
                    <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close""></button>
                </div>
                <div class=""modal-body"">
                    <table class=""table"">

                        <thead class=""deepBlueBack whiteFont"">
                            <tr>
                                <th> S/N </th>
                                <th>Product Name </th>
                                <th>Amount Sold </th>
                                <th>Quantity</th>
                            </tr>
                        </thead>
                        <tbody id=""");
            WriteLiteral(@"allItemTable"">
                            
                        </tbody>
                           

                    </table>

                </div>
                <div id=""statusParent_order"" style=""display:none"" class=""alert alert-success my-alert"" role=""alert"">
                    <div id=""status_order"" class=""alert-text""></div>
                </div>
                <div class=""modal-footer"">
                    <button type=""button"" id=""btnClose_order"" class=""btn btn-secondary"" data-dismiss=""modal"">Close</button>
");
            WriteLiteral("\n                </div>\n            </div>\n        </div>\n    </div>\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("environment", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ff5d893074a3e1e9cde7234f0448cf07d265ba5715018", async() => {
                    WriteLiteral("\n            ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ff5d893074a3e1e9cde7234f0448cf07d265ba5715302", async() => {
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\n            ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ff5d893074a3e1e9cde7234f0448cf07d265ba5716464", async() => {
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\n            ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "ff5d893074a3e1e9cde7234f0448cf07d265ba5717626", async() => {
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\n        ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.EnvironmentTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper.Include = (string)__tagHelperAttribute_7.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("environment", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ff5d893074a3e1e9cde7234f0448cf07d265ba5719850", async() => {
                    WriteLiteral("\r\n            ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ff5d893074a3e1e9cde7234f0448cf07d265ba5720136", async() => {
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\r\n            ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ff5d893074a3e1e9cde7234f0448cf07d265ba5721300", async() => {
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\r\n            ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "ff5d893074a3e1e9cde7234f0448cf07d265ba5722464", async() => {
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\r\n        ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.EnvironmentTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper.Exclude = (string)__tagHelperAttribute_8.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"

        <script>

            var ServerRoot = $(""#serverUrl"").val();

            var orderTbl = $('#allItemTable');

            $(document).ready(function () {

                GetAllOrder();

                $(document).on('click', '#btnClose_order', ClearTable);

                $(document).on('click', '#editOrder', editOrder);

                $('select').on('change', getShopOrders)


            })


            var getShopOrders = function () {


                $(""#allOrderTable"").dataTable().fnDestroy();

                var shopId = $(""#shopId"").val();

                $.ajax(`${ServerRoot}api/order/getorder/${shopId}`, { method: ""get"" })
                    .then(function (resp) {

                        console.log(""ordres for shop:"", resp);

                        $(""#allOrderTable"").dataTable({
                            ""scrollX"": true,
                            ""data"": resp,
                            ""columns"": [

                                { ""title"": ""Order Date"", ""data"": ""sale");
                WriteLiteral(@"sdate"" },

                                {
                                    ""title"": ""Customer Name"",
                                    ""data"": ""customerName"",
                                    ""render"": function (data) {
                                        return `<strong> ${data}  </strong>`
                                    }
                                },
                                {
                                    ""title"": ""Order ID"",
                                    ""data"": ""shoppingCartId"",
                                    ""render"": function (data) {
                                        return `<strong> ${data}  </strong>`
                                    }
                                },
                                { ""title"": ""Order Price"", ""data"": ""totalPrice"" },
                                { ""title"": ""Order Quantity"", ""data"": ""totalQuantity"" },
                                { ""title"": ""Order Profit"", ""data"": ""totalProfit"" },

                     ");
                WriteLiteral(@"           {
                                    ""title"": ""Staff Name"",
                                    ""data"": ""staffName"",
                                    ""render"": function (data) {
                                        return `<strong> ${data}  </strong>`
                                    }
                                },
                                {
                                    ""title"": ""Shop Number"",
                                    ""data"": ""shopId""
                                },
                                {
                                    ""title"": ""View"",
                                    ""data"": ""shoppingCartId""
                                }

                            ],

                            ""columnDefs"":
                                [
                                    {
                                        ""targets"": 8,

                                        ""render"": function (data) {
                                            var");
                WriteLiteral(@" update = `<button id=""editOrder"" data-id=""${data}"" class=""btn btn-primary btn-sm mr-2"" data-toggle=""modal"" data-target=""#modal_order""><i class=""fa fa-2 fa-edit""></i></button>`


                                            return update;
                                        }

                                    }


                                ]
                        })




                    });
            }

            var ClearTable = function () {
                console.log(""Clear Fired!"");
                $('#allItemTable').empty();
            }



            var GetAllOrder = function () {

                $.ajax(`${ServerRoot}api/order`, { method: ""get"" })
                    .then(function (resp) {

                        console.log(resp);
                        $(""#allOrderTable"").dataTable({
                            ""scrollX"": true,
                            ""data"": resp,
                            ""columns"": [

                                { ""title"": ""Order Date"", ""data"":");
                WriteLiteral(@" ""salesdate"" },

                                {
                                    ""title"": ""Customer Name"",
                                    ""data"": ""customerName"",
                                    ""render"": function (data) {
                                        return `<strong> ${data}  </strong>`
                                    }
                                },
                                {
                                    ""title"": ""Order ID"",
                                    ""data"": ""shoppingCartId"",
                                    ""render"": function (data) {
                                        return `<strong> ${data}  </strong>`
                                    }
                                },
                                { ""title"": ""Order Price"", ""data"": ""totalPrice"" },
                                { ""title"": ""Order Quantity"", ""data"": ""totalQuantity"" },
                                { ""title"": ""Order Profit"", ""data"": ""totalProfit"" },

               ");
                WriteLiteral(@"                 {
                                    ""title"": ""Staff Name"",
                                    ""data"": ""staffName"",
                                    ""render"": function (data) {
                                        return `<strong> ${data}  </strong>`
                                    }
                                },
                                {
                                    ""title"": ""Shop Number"",
                                    ""data"": ""shopId""
                                },

                                {
                                    ""title"": ""View"",
                                    ""data"": ""shoppingCartId""
                                }

                            ],

                            ""columnDefs"":
                                [
                                    {
                                        ""targets"": 8,

                                        ""render"": function (data) {
                                        ");
                WriteLiteral(@"    var update = `<button id=""editOrder"" data-id=""${data}"" class=""btn btn-primary btn-sm mr-2"" data-toggle=""modal"" data-target=""#modal_order""><i class=""fa fa-2 fa-edit""></i></button>`


                                            return update;
                                        }

                                    }


                                ]
                        })
                    });



            }



            // Binding Data to Editproduct Modal Form
            var editOrder = function () {

                $(""#orderIdModal"").html();
                var btnEdit = $(this);
                var orderId = btnEdit.attr('data-id');

                $(""#orderIdModal"").html(`${orderId}`);

                $.ajax(`${ServerRoot}api/order/${orderId}`, { method: ""get"" })
                    .then(resp => {
                        console.log(""Single Order"", resp);

                        bindAllOrdersToTable(resp);

                    }).catch(err => {


                    })



      ");
                WriteLiteral(@"      }


            var bindAllOrdersToTable = function (arrayData) {

                var orderTbl = document.getElementById(""allItemTable"");


                arrayData.forEach((data, idx) => {
                    buildOrderRow(data, idx);
                });


            }

            var buildOrderRow = function (data, idx_order) {

                orderTbl.append(`
                                    <tr>
                                                <td>${idx_order + 1} </td>
                                                <td>${data.productName} </td>
                                                <td>${data.productCost} </td>
                                                <td>${data.productQty} </td>
                                                

                                        </tr>
                                `);


            }

        </script>

    ");
            }
            );
#nullable restore
#line 364 "C:\Users\Khareem_Win_Usr\Documents\Visual Studio 2019\2 - YemisCopy_SyncVersion\Remote.Manager Version\KaylaaShop\Pages\Orders.cshtml"
     

}
else
{
   //@RedirectToPage(../accessdenied"));
  
   // @Redirect("../accessdenied");
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\n\n\n\n\n\n\n\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IConfiguration Configuration { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<KaylaaShop.Pages.Kaylaa.OrdersModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<KaylaaShop.Pages.Kaylaa.OrdersModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<KaylaaShop.Pages.Kaylaa.OrdersModel>)PageContext?.ViewData;
        public KaylaaShop.Pages.Kaylaa.OrdersModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
