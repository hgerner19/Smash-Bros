using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

public partial class CharacterCard : ComponentBase
{
    [Parameter]
    public YourCharacterType Character { get; set; }

    [Parameter]
    public EventCallback<YourCharacterType> HandleFavorites { get; set; }

    [Parameter]
    public string BaseURL { get; set; }

    private bool IsFav { get; set; }

    protected override void OnInitialized()
    {
        IsFav = Character.Favorite == "yes";
    }

    private List<RenderFragment> DisplaySpecials()
    {
        List<RenderFragment> fragments = new List<RenderFragment>();
        foreach (var move in Character.Special)
        {
            fragments.Add(builder =>
            {
                builder.OpenElement(0, "li");
                builder.AddContent(1, move);
                builder.CloseElement();
            });
        }
        return fragments;
    }

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
        builder.AddAttribute(1, "class", "card");
        builder.OpenElement(2, "img");
        builder.AddAttribute(3, "src", Character.Images.Portrait);
        builder.CloseElement();
        builder.OpenElement(4, "div");
        builder.AddAttribute(5, "class", "card-content");
        builder.OpenElement(6, "h3");
        builder.AddAttribute(7, "style", "text-align: left");
        builder.AddContent(8, Character.Name);
        builder.CloseElement();
        builder.OpenElement(9, "div");
        builder.AddAttribute(10, "class", "card-meta");
        builder.AddContent(11, "Tier " + Character.Tier);
        builder.CloseElement();
        builder.OpenElement(12, "div");
        builder.AddAttribute(13, "class", "card-description");
        builder.AddContent(14, "Special Moves:");
        builder.CloseElement();
        builder.OpenElement(15, "ul");
        builder.AddAttribute(16, "class", "bulleted");
        foreach (var fragment in DisplaySpecials())
        {
            builder.AddContent(17, fragment);
        }
        builder.CloseElement();
        builder.CloseElement();
        builder.OpenElement(18, "div");
        builder.AddAttribute(19, "class", "card-content");
        builder.AddContent(20, HandleButton());
        builder.CloseElement();
        builder.CloseElement();
    }

    private RenderFragment HandleButton()
    {
        if (IsFav)
        {
            return builder =>
            {
                builder.OpenElement(0, "button");
                builder.AddAttribute(1, "class", "fluid");
                builder.AddAttribute(2, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, HandleRemoveFavClick));
                builder.AddContent(3, "Remove from Favorites");
                builder.CloseElement();
            };
        }
        else
        {
            return builder =>
            {
                builder.OpenElement(0, "button");
                builder.AddAttribute(1, "class", "fluid");
                builder.AddAttribute(2, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, HandleFavClick));
                builder.AddContent(3, "Add to Favorites");
                builder.CloseElement();
            };
        }
    }
}
