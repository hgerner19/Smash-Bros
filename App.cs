using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

public partial class App : ComponentBase
{
    private List<YourCharacterType> allCharacters = new List<YourCharacterType>();
    private const string baseURL = "http://localhost:3001/characters/";

    protected override async Task OnInitializedAsync()
    {
        await FetchCharacters();
    }

    private async Task FetchCharacters()
    {
        using (var httpClient = new HttpClient())
        {
            allCharacters = await httpClient.GetFromJsonAsync<List<YourCharacterType>>(baseURL);
        }
        StateHasChanged();
    }

    private void HandleNewCharacter(YourCharacterType newCharacterObj)
    {
        allCharacters.Add(newCharacterObj);
        StateHasChanged();
    }

    private void HandleFavorites(YourCharacterType favCharacter)
    {
        var index = allCharacters.FindIndex(c => c.id == favCharacter.id);
        if (index != -1)
        {
            allCharacters[index] = favCharacter;
            StateHasChanged();
        }
    }

    

    protected override async Task OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            
        }
    }
}
