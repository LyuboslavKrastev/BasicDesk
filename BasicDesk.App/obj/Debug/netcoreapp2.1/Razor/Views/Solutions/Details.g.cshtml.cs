#pragma checksum "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Solutions\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f70c19c3cd6efc191fe82e5a06b0383735c59993"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Solutions_Details), @"mvc.1.0.view", @"/Views/Solutions/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Solutions/Details.cshtml", typeof(AspNetCore.Views_Solutions_Details))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f70c19c3cd6efc191fe82e5a06b0383735c59993", @"/Views/Solutions/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"60972e8d07214b2ad026c19851d667b6215156c7", @"/Views/_ViewImports.cshtml")]
    public class Views_Solutions_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SolutionDetailsViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Solutions", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Solutions\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
            BeginContext(76, 48, true);
            WriteLiteral("\r\n<h2 class=\"text-center\">Details for Solution #");
            EndContext();
            BeginContext(125, 8, false);
#line 6 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Solutions\Details.cshtml"
                                         Write(Model.Id);

#line default
#line hidden
            EndContext();
            BeginContext(133, 223, true);
            WriteLiteral("</h2>\r\n\r\n<div class=\"container\">\r\n    <div class=\"panel-group\">\r\n        <div class=\"panel panel-primary\">\r\n            <div class=\"panel-heading clearfix\">\r\n                <div class=\"pull-left\"><strong>Subject:</strong> ");
            EndContext();
            BeginContext(357, 11, false);
#line 12 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Solutions\Details.cshtml"
                                                            Write(Model.Title);

#line default
#line hidden
            EndContext();
            BeginContext(368, 77, true);
            WriteLiteral("</div>\r\n                <div class=\"pull-right\"><strong>Created On:</strong> ");
            EndContext();
            BeginContext(446, 15, false);
#line 13 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Solutions\Details.cshtml"
                                                                Write(Model.CreatedOn);

#line default
#line hidden
            EndContext();
            BeginContext(461, 97, true);
            WriteLiteral("</div>\r\n            </div>\r\n            <div class=\"panel-body\"><strong>Description:</strong> <p>");
            EndContext();
            BeginContext(559, 13, false);
#line 15 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Solutions\Details.cshtml"
                                                                Write(Model.Content);

#line default
#line hidden
            EndContext();
            BeginContext(572, 130, true);
            WriteLiteral("</p></div>\r\n\r\n            <div class=\"panel-footer clearfix\">\r\n                <div class=\"text-center\"><strong> Author:</strong> ");
            EndContext();
            BeginContext(703, 12, false);
#line 18 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Solutions\Details.cshtml"
                                                              Write(Model.Author);

#line default
#line hidden
            EndContext();
            BeginContext(715, 30, true);
            WriteLiteral("</div>\r\n            </div>\r\n\r\n");
            EndContext();
#line 21 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Solutions\Details.cshtml"
             if (Model.Attachment != null)
            {

#line default
#line hidden
            BeginContext(804, 124, true);
            WriteLiteral("                <div class=\"panel-footer clearfix\">\r\n                    <div class=\"text-center\">\r\n                        ");
            EndContext();
            BeginContext(928, 55, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "473206a254304b5ba6f5b164d46917cb", async() => {
                BeginContext(963, 12, true);
                WriteLiteral("Attachment: ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#line 25 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Solutions\Details.cshtml"
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
            BeginContext(983, 26, true);
            WriteLiteral("\r\n                        ");
            EndContext();
            BeginContext(1009, 297, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2e390468436d435b93c25f2d230e0878", async() => {
                BeginContext(1220, 30, true);
                WriteLiteral("\r\n                            ");
                EndContext();
                BeginContext(1251, 25, false);
#line 28 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Solutions\Details.cshtml"
                       Write(Model.Attachment.FileName);

#line default
#line hidden
                EndContext();
                BeginContext(1276, 26, true);
                WriteLiteral("\r\n                        ");
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
#line 27 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Solutions\Details.cshtml"
                                   WriteLiteral(Model.Attachment.FileName);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["filename"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-filename", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["filename"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 27 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Solutions\Details.cshtml"
                                                                                   WriteLiteral(Model.Attachment.PathToFile);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["filePath"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-filePath", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["filePath"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 27 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Solutions\Details.cshtml"
                                                                                                                                       WriteLiteral(Model.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["solutionId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-solutionId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["solutionId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1306, 54, true);
            WriteLiteral("\r\n                    </div>\r\n                </div>\r\n");
            EndContext();
#line 32 "C:\Users\Zorko\Documents\BasicDesk\BasicDesk-Project-CSharp-MVC-Frameworks-ASP.NET-Core\BasicDesk.App\Views\Solutions\Details.cshtml"
            }

#line default
#line hidden
            BeginContext(1375, 38, true);
            WriteLiteral("        </div>\r\n\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SolutionDetailsViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
