#pragma checksum "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Requests\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bf86482a9e47fced0792ea70f3ad08d68be0f2d0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Requests_Details), @"mvc.1.0.view", @"/Views/Requests/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Requests/Details.cshtml", typeof(AspNetCore.Views_Requests_Details))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\_ViewImports.cshtml"
using BasicDesk.App;

#line default
#line hidden
#line 2 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\_ViewImports.cshtml"
using BasicDesk.App.Models;

#line default
#line hidden
#line 3 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\_ViewImports.cshtml"
using BasicDesk.Models;

#line default
#line hidden
#line 4 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\_ViewImports.cshtml"
using BasicDesk.App.Models.ViewModels;

#line default
#line hidden
#line 5 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\_ViewImports.cshtml"
using BasicDesk.App.Models.BindingModels;

#line default
#line hidden
#line 6 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\_ViewImports.cshtml"
using BasicDesk.App.Helpers.Messages;

#line default
#line hidden
#line 7 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\_ViewImports.cshtml"
using BasicDesk.App.Helpers;

#line default
#line hidden
#line 1 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Requests\Details.cshtml"
using BasicDesk.Common.Constants;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bf86482a9e47fced0792ea70f3ad08d68be0f2d0", @"/Views/Requests/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"60972e8d07214b2ad026c19851d667b6215156c7", @"/Views/_ViewImports.cshtml")]
    public class Views_Requests_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<RequestDetailsViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Requests", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Download", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Requests\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
            BeginContext(111, 174, true);
            WriteLiteral("\r\n<div class=\"btn-group btn-group-toggle\" data-toggle=\"buttons\">\r\n<button class=\"btn disabled btn\" style=\"display:table; background-color: #00611C; color:white;\">Request ID: ");
            EndContext();
            BeginContext(286, 8, false);
#line 8 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Requests\Details.cshtml"
                                                                                                       Write(Model.Id);

#line default
#line hidden
            EndContext();
            BeginContext(294, 392, true);
            WriteLiteral(@"</button>
        <button id=""btn_desc"" class=""btn btn-danger"">Request</button>
        <button id=""btn_res"" class=""btn"">Resolution</button>
        <button id=""btn_hist"" class=""btn"">History</button>
</div>

<div class=""panel-group"" id=""request"">
        <div class=""panel"">
        <div class=""panel-heading clearfix"">
            <div class=""pull-left""><strong>Requester:</strong> ");
            EndContext();
            BeginContext(687, 12, false);
#line 17 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Requests\Details.cshtml"
                                                          Write(Model.Author);

#line default
#line hidden
            EndContext();
            BeginContext(699, 73, true);
            WriteLiteral("</div>\r\n            <div class=\"pull-right\"><strong>Created On:</strong> ");
            EndContext();
            BeginContext(773, 15, false);
#line 18 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Requests\Details.cshtml"
                                                            Write(Model.CreatedOn);

#line default
#line hidden
            EndContext();
            BeginContext(788, 99, true);
            WriteLiteral("</div>\r\n        </div>\r\n        <div class=\"panel-body\">\r\n            <p><strong>Subject:</strong> ");
            EndContext();
            BeginContext(888, 13, false);
#line 21 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Requests\Details.cshtml"
                                    Write(Model.Subject);

#line default
#line hidden
            EndContext();
            BeginContext(901, 85, true);
            WriteLiteral("</p>\r\n            <hr>\r\n            <strong>Description</strong><hr>\r\n            <p>");
            EndContext();
            BeginContext(987, 17, false);
#line 24 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Requests\Details.cshtml"
          Write(Model.Description);

#line default
#line hidden
            EndContext();
            BeginContext(1004, 141, true);
            WriteLiteral("</p>     \r\n        </div>\r\n       \r\n        <div class=\"panel-footer clearfix\">\r\n            <div class=\"pull-left\"><strong>Status:</strong> ");
            EndContext();
            BeginContext(1146, 12, false);
#line 28 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Requests\Details.cshtml"
                                                       Write(Model.Status);

#line default
#line hidden
            EndContext();
            BeginContext(1158, 74, true);
            WriteLiteral("</div>\r\n            <div class=\"pull-right\"><strong>Technician: </strong> ");
            EndContext();
            BeginContext(1233, 20, false);
#line 29 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Requests\Details.cshtml"
                                                             Write(Model.AssignedToName);

#line default
#line hidden
            EndContext();
            BeginContext(1253, 96, true);
            WriteLiteral("</div>\r\n            <br /><hr />\r\n            <div class=\"pull-left\"><strong>Category:</strong> ");
            EndContext();
            BeginContext(1350, 14, false);
#line 31 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Requests\Details.cshtml"
                                                         Write(Model.Category);

#line default
#line hidden
            EndContext();
            BeginContext(1364, 90, true);
            WriteLiteral("</div>\r\n\r\n            <div class=\"pull-right\">\r\n                <strong>Email: </strong><a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1454, "\"", 1492, 2);
            WriteAttributeValue("", 1461, "mailto://", 1461, 9, true);
#line 34 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Requests\Details.cshtml"
WriteAttributeValue("", 1470, Model.AssignedToEmail, 1470, 22, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1493, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(1495, 21, false);
#line 34 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Requests\Details.cshtml"
                                                                             Write(Model.AssignedToEmail);

#line default
#line hidden
            EndContext();
            BeginContext(1516, 44, true);
            WriteLiteral("</a>\r\n            </div>\r\n        </div>\r\n\r\n");
            EndContext();
#line 38 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Requests\Details.cshtml"
         if (Model.Attachment != null)
        {

#line default
#line hidden
            BeginContext(1611, 112, true);
            WriteLiteral("            <div class=\"panel-footer clearfix\">\r\n                <div class=\"text-center\">\r\n                    ");
            EndContext();
            BeginContext(1723, 55, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "781a73c047a3414d975c78c3c8ad48a4", async() => {
                BeginContext(1758, 12, true);
                WriteLiteral("Attachment: ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#line 42 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Requests\Details.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => Model.Attachment);

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1778, 22, true);
            WriteLiteral("\r\n                    ");
            EndContext();
            BeginContext(1800, 283, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f4d995b072644c7b95c12d5902bf48f3", async() => {
                BeginContext(2005, 26, true);
                WriteLiteral("\r\n                        ");
                EndContext();
                BeginContext(2032, 25, false);
#line 45 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Requests\Details.cshtml"
                   Write(Model.Attachment.FileName);

#line default
#line hidden
                EndContext();
                BeginContext(2057, 22, true);
                WriteLiteral("\r\n                    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-filename", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 44 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Requests\Details.cshtml"
                               WriteLiteral(Model.Attachment.FileName);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["filename"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-filename", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["filename"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 44 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Requests\Details.cshtml"
                                                                               WriteLiteral(Model.Attachment.PathToFile);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["filePath"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-filePath", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["filePath"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 44 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Requests\Details.cshtml"
                                                                                                                                  WriteLiteral(Model.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["requestId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-requestId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["requestId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2083, 46, true);
            WriteLiteral("\r\n                </div>\r\n            </div>\r\n");
            EndContext();
#line 49 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Requests\Details.cshtml"
        }

#line default
#line hidden
            BeginContext(2140, 350, true);
            WriteLiteral(@"    </div>
</div>

<div class=""panel-group"" id=""resolution"" style=""display:none"">
    <div class=""panel"">
        <div class=""panel-heading clearfix"">
            <div class=""pull-left""><strong>Resolution</strong></div>
        </div>
        <div class=""panel-body"">
            <textarea class=""form-control"" rows=""4"" style=""resize:none;"">");
            EndContext();
            BeginContext(2491, 16, false);
#line 59 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Requests\Details.cshtml"
                                                                    Write(Model.Resolution);

#line default
#line hidden
            EndContext();
            BeginContext(2507, 1729, true);
            WriteLiteral(@"</textarea>
        </div>
        <div class=""panel-footer clearfix"">
            <div class=""col-md-offset-6"">
                <button id=""btn_edit"" class=""btn btn-success"">Save</button>
                <button id=""btn_edit"" class=""btn btn-warning"">Edit</button>

            </div>
        </div>
    </div>
</div>

<div class=""panel-group"" id=""history"" style=""display:none"">
    <div class=""panel"">
        <div class=""panel-heading clearfix"">
            <div class=""pull-left""><strong>History</strong></div>
        </div>
        <div class=""panel-body""><p>Model.History</p></div>
    </div>
</div>
<script src=""https://code.jquery.com/jquery-3.3.1.min.js""
        integrity=""sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8=""
        crossorigin=""anonymous""></script>
<script>
    $('#btn_desc').on('click', function(){
        $('#history').hide();
        $('#resolution').hide();
        $('#request').show();
        $('#btn_res').removeClass('btn-danger');
        $('#btn_hist').");
            WriteLiteral(@"removeClass('btn-danger');
        $('#btn_desc').addClass('btn-danger');
    });
    $('#btn_res').on('click', function(){
        $('#history').hide();  
        $('#request').hide();
        $('#resolution').show();
        $('#btn_desc').removeClass('btn-danger');
        $('#btn_hist').removeClass('btn-danger');
        $('#btn_res').addClass('btn-danger');

    });
    $('#btn_hist').on('click', function(){      
        $('#request').hide();
        $('#resolution').hide();
        $('#history').show();  
        $('#btn_desc').removeClass('btn-danger');
        $('#btn_res').removeClass('btn-danger');
        $('#btn_hist').addClass('btn-danger');
    });
</script>

");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<RequestDetailsViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
