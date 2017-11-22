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

        PaginationLinksTagHelper(IUrlHelperFactory urlHelperFactory) {
            this.urlHelperFactory = urlHelperFactory;
        }
        [ViewContext][HtmlAttributeNotBound]

        public ViewContext ViewContext { get; set; }
        
        public PagingInfo PageModel { get; set; } //Importajte - faz ligação com o page-model acima

        public string PageAction { get; set; }

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

                result.InnerHtml.AppendHtml(pageLink);
            }
            output.Content.AppendHtml(result.InnerHtml); //apenas conteudo da div
        }
    }
}
