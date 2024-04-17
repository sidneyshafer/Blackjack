using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Reflection;

namespace BlackJack.TagHelpers
{
    [HtmlTargetElement("hand-heading")]
    public class HeadingTagHelper : TagHelper
    {
        [HtmlAttributeName("name")]
        public string Name { get; set; } = "";

        [HtmlAttributeName("total")]
        public int Total { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            output.TagName = "h5";

            if (!string.IsNullOrEmpty(Name))
            {
                if (Name == "Dealer" && Total > 0)
                {
                    output.Content.SetHtmlContent($"{Name}: {Total}");
                }
                else if (Name == "Player")
                {
                    output.Content.SetHtmlContent($"{Name}: {Total}");
                }
                else
                {
                    output.Content.SetHtmlContent(Name);
                }
            }
        }
    }
}
