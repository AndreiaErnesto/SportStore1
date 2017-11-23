using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.InfraStructure {
    [HtmlTargetElement("div", Attributes = "page-model")]

    public class PaginationLinksTagHelper : TagHelper {
        private IUrlHelperFactory urlHelperFactory;

        public static int MaxLinksBeforeAndAfterCurrentPage = 7;

        public PaginationLinksTagHelper(IUrlHelperFactory urlHelperFactory) {
            this.urlHelperFactory = urlHelperFactory;
        }
        [ViewContext][HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PagingInfo PageModel { get; set; } //Importante - faz ligação com o page-model acima
        public string PageAction { get; set; }
        //Para a paginação aprecer correta
        public bool CssClassesEnable { get; set; } = false;
        public string CssClassePage { get; set; }  //Css classe que quero em cada um dos links da pagina
        public string CssClassePageNormal { get; set; }  //links da pagina normal
        public string CssClassePageSelected { get; set; }  //links da pagina selecionada

        public override void Process(TagHelperContext context, TagHelperOutput output){
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            TagBuilder result = new TagBuilder("div");
            int initial = PageModel.CurrentPage - MaxLinksBeforeAndAfterCurrentPage;
            if (initial < 1) initial = 1;

            int final = PageModel.CurrentPage + MaxLinksBeforeAndAfterCurrentPage;
            if (final > PageModel.TotalPages) final = PageModel.TotalPages;

            for (int p = initial; p <= PageModel.TotalPages; p++) {
                TagBuilder pageLink = new TagBuilder("a"); //tag da hiperligação
                pageLink.Attributes["href"] = urlHelper.Action(PageAction, new { page = p}); //temos de ir ao atributo de href criar um link para a ação page = p
                pageLink.InnerHtml.Append(p.ToString());

                if (CssClassesEnable){
                    pageLink.AddCssClass(CssClassePage);
                    pageLink.AddCssClass(p == PageModel.CurrentPage ? CssClassePageSelected : CssClassePageNormal);
                }

                result.InnerHtml.AppendHtml(pageLink);
            }
            output.Content.AppendHtml(result.InnerHtml); //apenas conteudo da div
        }
    }
}
