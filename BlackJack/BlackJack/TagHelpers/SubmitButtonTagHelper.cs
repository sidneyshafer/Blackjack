using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BlackJack.TagHelpers
{
    [HtmlTargetElement("submit-button")]
    public class SubmitButtonTagHelper : TagHelper
    {
        [HtmlAttributeName("action")]
        public string Action { get; set; } = "";

        [HtmlAttributeName("disabled")]
        public bool Disabled { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; } = null!;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "form";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.SetAttribute("action", Action);
            output.Attributes.SetAttribute("method", "post");
            output.Attributes.SetAttribute("class", "col");

            var button = new TagBuilder("button");
            button.Attributes.Add("type", "submit");
            button.Attributes.Add("class", "btn btn-primary");
            button.InnerHtml.Append(Action);

            if (Disabled)
            {
                button.Attributes.Add("disabled", "disabled");
            }

            output.Content.SetHtmlContent(button);
        }

    }
}
