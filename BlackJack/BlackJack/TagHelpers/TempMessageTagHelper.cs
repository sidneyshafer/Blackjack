using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using Microsoft.AspNetCore.Html;

namespace BlackJack.TagHelpers
{
    [HtmlTargetElement("temp-message")]
    public class TempMessageTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext VeiwCtx { get; set; } = null!;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var td = VeiwCtx.TempData;

            if (td.ContainsKey("message"))
            {
                var classes = "bg-" + td["background"] + " text-white p-2";

                output.TagName = "h4";
                output.TagMode = TagMode.StartTagAndEndTag;
                output.Attributes.SetAttribute("class", classes);
                output.Content.SetContent(td["message"]?.ToString());
            }
            else
            {
                output.SuppressOutput();
            }
        }
    }
}
