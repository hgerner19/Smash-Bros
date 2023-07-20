using Microsoft.AspNetCore.Components;

public partial class Home : ComponentBase
{
    private const string Logo = "./Super-Smash-Bros.-Logo-PNG-Pic (1).png";

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", "homeBG");
        builder.AddAttribute(2, "style", "paddingTop: 6em");

        builder.OpenElement(3, "img");
        builder.AddAttribute(4, "class", "SSLogo");
        builder.AddAttribute(5, "src", Logo);
        builder.AddAttribute(6, "alt", "image");
        builder.CloseElement();

        builder.OpenElement(7, "h1");
        builder.AddAttribute(8, "class", "homeTitle");
        builder.AddContent(9, "Character Portal");
        builder.CloseElement();

        builder.CloseElement();
    }
}
