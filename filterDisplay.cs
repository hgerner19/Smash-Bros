using Microsoft.AspNetCore.Components;

public partial class FilterDisplay : ComponentBase
{
    [Parameter]
    public string Tier { get; set; }

    [Parameter]
    public EventCallback<string> SetTier { get; set; }

    [Parameter]
    public string Avail { get; set; }

    [Parameter]
    public EventCallback<string> SetAvail { get; set; }

    [Parameter]
    public string Appeared { get; set; }

    [Parameter]
    public EventCallback<string> SetAppeared { get; set; }

    private void HandleChange(ChangeEventArgs e)
    {
        SetTier.InvokeAsync(e.Value.ToString());
    }

    private void HandleChange2(ChangeEventArgs e)
    {
        SetAvail.InvokeAsync(e.Value.ToString());
    }

    private void HandleChange3(ChangeEventArgs e)
    {
        SetAppeared.InvokeAsync(e.Value.ToString());
    }

    private void HandleClear()
    {
        SetTier.InvokeAsync(string.Empty);
        SetAvail.InvokeAsync(string.Empty);
        SetAppeared.InvokeAsync(string.Empty);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");

        builder.OpenElement(1, "Typography");
        builder.AddAttribute(2, "variant", "h3");
        builder.AddAttribute(3, "paddingLeft", "2");
        builder.AddAttribute(4, "fontFamily", "Verdana");
        builder.AddContent(5, "Filter By:");
        builder.CloseElement();

        builder.OpenElement(6, "Divider");
        builder.AddAttribute(7, "sx", "pt:2");
        builder.CloseElement();

        builder.OpenElement(8, "Box");
        builder.AddAttribute(9, "sx", "minWidth: 250");

        // ... Rest of the code here ...

        builder.OpenElement(34, "Box");
        builder.AddAttribute(35, "PaddingLeft", "10");
        builder.AddAttribute(36, "PaddingTop", "3");
        builder.OpenComponent<Button>(37);
        builder.AddAttribute(38, "Variant", "primary");
        builder.AddAttribute(39, "Size", "large");
        builder.AddAttribute(40, "Color", "black");
        builder.AddAttribute(41, "OnClick", EventCallback.Factory.Create(this, HandleClear));
        builder.AddContent(42, "Clear");
        builder.CloseComponent();
        builder.CloseElement();

        builder.CloseElement();
        builder.CloseElement();
    }
}
