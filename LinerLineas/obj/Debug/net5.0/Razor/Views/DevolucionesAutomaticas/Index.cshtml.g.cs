#pragma checksum "C:\Users\ccardoso\Desktop\PagoReferenciadoLuis\LinerLineas\LinerLineas\Views\DevolucionesAutomaticas\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5a1c2e98f3e2692c0f0c408e31b538f3091e40fe"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DevolucionesAutomaticas_Index), @"mvc.1.0.view", @"/Views/DevolucionesAutomaticas/Index.cshtml")]
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
#line 1 "C:\Users\ccardoso\Desktop\PagoReferenciadoLuis\LinerLineas\LinerLineas\Views\_ViewImports.cshtml"
using LinerLineas;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ccardoso\Desktop\PagoReferenciadoLuis\LinerLineas\LinerLineas\Views\_ViewImports.cshtml"
using LinerLineas.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5a1c2e98f3e2692c0f0c408e31b538f3091e40fe", @"/Views/DevolucionesAutomaticas/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ed23d40f98faed2e966adc229ba10a007f5f40e3", @"/Views/_ViewImports.cshtml")]
    public class Views_DevolucionesAutomaticas_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/jsFiles/DevolucionesAutomaticas.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral(@"<link rel=""stylesheet"" href=""https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"">

<div id=""dvSeccionesControlSaldos"" class=""seccionesControlSaldos"">
    <div id=""dvControlSaldos"" class=""controlSaldos"">
        <div class=""cardHeader criteriosBusqueda"">
            <h2>Devolución de Remesas</h2>
            <div class=""fila3Columnas  marginBottom5Px paddingLeft15Px"">
                <div class=""dvElementos"">
                    <input type=""text"" id=""txtBusqueda"" placeholder=""BL"" />
                </div>
                <div class=""dvElementos enLinea col-md-4"">
                    <input type=""text"" id=""txtItinerario""");
            BeginWriteAttribute("value", " value=\"", 666, "\"", 674, 0);
            EndWriteAttribute();
            WriteLiteral(@" title=""Itinerario"" placeholder=""ITINERARIO"" />
                </div>
            </div>
        </div>
        <br />
        <div class=""cardTabla"">
            <table id=""tContenedores"" class="" table table-bordered table-responsive"">
                <thead>
                    <tr>
                        <td class=""centrarColumna"">BL</td>
                        <td class=""centrarColumna"">ITINERARIO</td>
                        <td class=""centrarColumna"" hidden>nIdTipoRevision</td>
                        <td class=""centrarColumna"">TIPO DE REVISIÓN</td>
                        <td class=""centrarColumna"" hidden>nNumeroCompañia</td>
                        <td class=""centrarColumna"">COMPAÑIA</td>
                        <td class=""centrarColumna"">ADEUDO FLETES</td>
                        <td class=""centrarColumna"">ADEUDO DEMORAS</td>
                        <td class=""centrarColumna"">ADEUDO DAÑOS</td>
                        <td class=""centrarColumna"" hidden>nIdreferencia</td> 
        ");
            WriteLiteral(@"                <td class=""centrarColumna"">REALIZAR DEVOLUCIÓN</td>
                    </tr>
                </thead>
                <tbody id=""tbContenedores"">
                </tbody>
            </table>
            <div id=""dvSpinner"" class=""centrar invisibilidad"">
                <i class=""fa fa-spinner fa-spin spinnerIcon""></i>
            </div>
        </div>
    </div>
</div>

");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5a1c2e98f3e2692c0f0c408e31b538f3091e40fe6398", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
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
