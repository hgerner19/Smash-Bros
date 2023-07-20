using Microsoft.AspNetCore.Components;

public partial class NavBar : ComponentBase
{
    private string activeClass = "active";

    private void OnClickNavLink(string navLink)
    {
        activeClass = navLink;
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "pd", "");

        builder.OpenElement(2, "Box");
        builder.AddAttribute(3, "sx", new Dictionary<string, object> { { "flexGrow", 1 } });

        builder.OpenElement(4, "AppBar");
        builder.AddAttribute(5, "display", "inline-flex");
        builder.AddAttribute(6, "style", "background: #17252A");

        builder.OpenElement(7, "Toolbar");

        BuildNavLink(builder, "Home", "/", "Home");
        BuildNavLink(builder, "Characters", "/characters", "Characters");
        BuildNavLink(builder, "Add Character", "/addCharacter", "Add Character");
        BuildNavLink(builder, "Favorites", "/favorites", "Favorites");

        builder.CloseElement(); // End of Toolbar

        builder.CloseElement(); // End of AppBar
        builder.CloseElement(); // End of Box
        builder.CloseElement(); // End of div
    }

    private void BuildNavLink(RenderTreeBuilder builder, string title, string link, string navLink)
    {
        builder.OpenElement(0, "Button");
        builder.AddAttribute(1, "variant", "text");
        builder.OpenElement(2, "Typography");
        builder.AddAttribute(3, "variant", "h6");
        builder.AddAttribute(4, "noWrap", true);
        builder.AddAttribute(5, "paddingRight", "2.5em");
        builder.AddAttribute(6, "paddingLeft", "2.5em");

        builder.OpenComponent<NavLink>(7);
        builder.AddAttribute(8, "href", link);
        builder.AddAttribute(9, "ActiveClass", activeClass);
        builder.AddAttribute(10, "class", "nav-link");
        builder.AddAttribute(11, "OnClick", EventCallback.Factory.Create(this, () => OnClickNavLink(navLink)));

        builder.AddContent(12, title);

        builder.CloseComponent();
        builder.CloseElement(); 
        builder.CloseElement(); 
    }
}
