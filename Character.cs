using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class Characters : ComponentBase
{
    [Parameter]
    public List<YourCharacterType> AllCharacters { get; set; }

    [Parameter]
    public EventCallback<YourCharacterType> HandleFavorites { get; set; }

    [Parameter]
    public string BaseURL { get; set; }

    private bool Open { get; set; }
    private YourCharacterType Search { get; set; }
    private string Tier { get; set; }
    private string Avail { get; set; }
    private string Appeared { get; set; }

    private void HandleSubmit(YourCharacterType value)
    {
        Search = value;
        Tier = string.Empty;
        Avail = string.Empty;
        Appeared = string.Empty;
    }

    private RenderFragment GetList() => builder =>
    {
        builder.OpenComponent<FilterDisplay>(0);
        builder.AddAttribute(1, nameof(FilterDisplay.Tier), Tier);
        builder.AddAttribute(2, nameof(FilterDisplay.SetTier), EventCallback.Factory.Create<string>(this, newValue => Tier = newValue));
        builder.AddAttribute(3, nameof(FilterDisplay.Avail), Avail);
        builder.AddAttribute(4, nameof(FilterDisplay.SetAvail), EventCallback.Factory.Create<string>(this, newValue => Avail = newValue));
        builder.AddAttribute(5, nameof(FilterDisplay.Appeared), Appeared);
        builder.AddAttribute(6, nameof(FilterDisplay.SetAppeared), EventCallback.Factory.Create<string>(this, newValue => Appeared = newValue));
        builder.CloseComponent();
    };

    private void HandleButtonClick()
    {
        if (IsFav)
        {
            HandleRemoveFavClick();
        }
        else
        {
            HandleFavClick();
        }
    }

    private async Task HandleFavClick()
    {
        IsFav = !IsFav;
        Character.Favorite = "yes";
        await HandleFavorites.InvokeAsync(Character);
    }

    private async Task HandleRemoveFavClick()
    {
        IsFav = !IsFav;
        Character.Favorite = "no";
        await HandleFavorites.InvokeAsync(Character);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", "characterBG");
        builder.OpenComponent<Container>(2);
        builder.AddAttribute(3, "style", "paddingTop: 6em");
        builder.OpenComponent<Grid>(4);
        builder.AddAttribute(5, "Item", true);
        builder.AddAttribute(6, "style", "textAlign: right;display:flex");
        builder.OpenComponent<Autocomplete<YourCharacterType>>(7);
        builder.AddAttribute(8, "Spacing", 2);
        builder.AddAttribute(9, "Sx", "{ width: 300, pr: 4, pl: 5 }");
        builder.AddAttribute(10, "FreeSolo", true);
        builder.AddAttribute(11, "OnChange", EventCallback.Factory.Create<YourCharacterType>(this, HandleSubmit));
        builder.AddAttribute(12, "Options", AllCharacters);
        builder.AddAttribute(13, "GetOptionLabel", (Func<YourCharacterType, string>)(character => character.Name));
        builder.AddAttribute(14, "RenderOption", (Func<RenderTreeBuilder, YourCharacterType, RenderFragment>)((builder, character) =>
        {
            builder.OpenComponent(0, "li");
            builder.AddAttribute(1, "Sx", "{ '& > img': { mr: 2, flexShrink: 0 } }");
            builder.OpenElement(2, "img");
            builder.AddAttribute(3, "loading", "lazy");
            builder.AddAttribute(4, "width", "20");
            builder.AddAttribute(5, "src", character.Images.Icon);
            builder.AddAttribute(6, "srcSet", character.Images.Icon);
            builder.AddAttribute(7, "alt", "");
            builder.CloseElement();
            builder.AddContent(8, character.Name);
            builder.CloseElement();
        }));
        builder.AddAttribute(15, "RenderInput", (Func<RenderTreeBuilder, EventCallback<YourCharacterType>, RenderFragment<TextField>>(RenderInput)));
        builder.CloseComponent();
        builder.OpenComponent<IconButton>(16);
        builder.AddAttribute(17, "OnClick", EventCallback.Factory.Create<MouseEventArgs>(this, () => Open = true));
        builder.OpenComponent<SortIcon>(18);
        builder.AddAttribute(19, "style", "{ fontSize: '2.1em', color: 'white', pr: 10 }");
        builder.CloseComponent();
        builder.CloseComponent();
        builder.OpenComponent<Drawer>(20);
        builder.AddAttribute(21, "Open", Open);
        builder.AddAttribute(22, "Anchor", "right");
        builder.AddAttribute(23, "OnClose", EventCallback.Factory.Create<MouseEventArgs>(this, () => Open = false));
        builder.AddAttribute(24, "ChildContent", GetList());
        builder.CloseComponent();
        builder.CloseComponent();
        builder.OpenComponent<Card.Group>(25);
        builder.AddAttribute(26, "ItemsPerRow", 5);
        builder.AddAttribute(27, "style", "{ paddingTop: '2.5em' }");
        builder.AddContent(28, characterDisplay);
        builder.CloseComponent();
        builder.CloseComponent();
        builder.CloseComponent();
        builder.CloseElement();
    }

    private RenderFragment<TextField> RenderInput(RenderTreeBuilder builder, EventCallback<YourCharacterType> handleSubmit)
    {
        return props =>
        {
            builder.OpenComponent<CssTextField>(0);
            builder.AddAttribute(1, "Label", "Search");
            builder.AddAttribute(2, "Sx", "{ input: { color: 'black' } }");
            builder.AddAttribute(3, "Value", Search);
            builder.AddAttribute(4, "OnInput", EventCallback.Factory.Create<ChangeEventArgs>(this, e =>
            {
                if (e.Value is string searchText)
                {
                    var character = AllCharacters.Find(c => c.Name == searchText);
                    handleSubmit.InvokeAsync(character);
                }
            }));
            builder.CloseComponent();
        };
    }
}
