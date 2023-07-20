using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

public partial class Favorites : ComponentBase
{
    [Parameter]
    public List<YourCharacterType> AllCharacters { get; set; }

    [Parameter]
    public EventCallback<YourCharacterType> HandleFavorites { get; set; }

    [Parameter]
    public string BaseURL { get; set; }

    private List<YourCharacterType> FavoriteCharacters => AllCharacters.FindAll(character => character.Favorite == "yes");

    private RenderFragment CharacterDisplay => builder =>
    {
        foreach (var character in FavoriteCharacters)
        {
            builder.OpenComponent<CharacterCard>(0);
            builder.AddAttribute(1, "Character", character);
            builder.AddAttribute(2, "HandleFavorites", HandleFavorites);
            builder.AddAttribute(3, "BaseURL", BaseURL);
            builder.CloseComponent();
        }
    };

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", "bg");
        builder.OpenComponent<Container>(2);
        builder.AddAttribute(3, "style", "paddingTop: 6em");
        builder.OpenComponent<Card.Group>(4);
        builder.AddAttribute(5, "ItemsPerRow", 5);
        builder.AddContent(6, CharacterDisplay);
        builder.CloseComponent();
        builder.CloseComponent();
        builder.CloseComponent();
    }
}
