#pragma checksum "E:\corePrac\ui\Views\UserRole\AddUserRole.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8028f1e7ad7c9f1769a1783f3b87f359c1e9861b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_UserRole_AddUserRole), @"mvc.1.0.view", @"/Views/UserRole/AddUserRole.cshtml")]
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
#nullable restore
#line 1 "E:\corePrac\ui\Views\_ViewImports.cshtml"
using ui;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\corePrac\ui\Views\_ViewImports.cshtml"
using ui.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8028f1e7ad7c9f1769a1783f3b87f359c1e9861b", @"/Views/UserRole/AddUserRole.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3b248bf9220e494c74c49641808d2ad6c0d20b33", @"/Views/_ViewImports.cshtml")]
    public class Views_UserRole_AddUserRole : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/jquery/dist/jquery.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "E:\corePrac\ui\Views\UserRole\AddUserRole.cshtml"
  
    ViewData["Title"]="AddUserRole";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<label for=""title""> Email :</label>
<select id=""uid"" tabindex=""1"" class=""form-control""></select>
<label for=""title""> Role :</label>
<select id=""roleid"" tabindex=""2"" class=""form-control""></select>
 <button type=""button"" tabindex=""3"" class=""btn btn-primary form-control"" id=""btnAdd"">Add</button>
 <br/>
<p><b><a href=""/User/UserList"">Back to Home</a></b></p>
 ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8028f1e7ad7c9f1769a1783f3b87f359c1e9861b3979", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
 <script>
     $(function(){
         LoadRole();
         LoadUser();
         $(""#btnAdd"").on(""click"",function(){
             debugger
            var dataObj = {
                 UserID: $(""#uid"").val(),
                 RoleID: $(""#roleid"").val()
             }
             $.ajax({
                 url: '/UserRole/AddUserRole',
                 data: dataObj,
                 type: 'Post',
                 async: false,
                 success: function(){
                     alert(""Added Role Successfully"")
                     window.location.href=""/User/UserList"";
                 },
                 error: function(){
                     alert(""Error Adding Role"")
                 }
             });
         });
     });

     function LoadUser(){
         $.ajax({
            url: '/User/GetAllUser',
            type: 'get',
            dataType: 'json',
            async: false,
            success: function(data){
                $(""#uid"").empty();
         ");
            WriteLiteral(@"       $(""#uid"").append('<option value=""0"">Choose User</option>');
                $.each(data,function(i,item){
                    $(""#uid"").append('<option value=""'+ item.userID +'"">'+ item.emailID +'</option>');
                });
            }
         });
     }


     function LoadRole(){
         $.ajax({
            url: '/Role/GetAllRole', 
            type: 'get',
            dataType: 'json',
            success: function(data){
                $(""#roleid"").empty();
                $(""#roleid"").append('<option value=""0"">Choose Role</option>');
                $.each(data,function(i,item){
                    $(""#roleid"").append('<option value=""'+ item.roleID +'"">'+ item.role +'</option>');
                });
            },
            error: function(){
                alert(""Error loading Role!"")
            }
         });
     }
 </script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
