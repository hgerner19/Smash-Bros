using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

public class AddCharacter : ComponentBase
{
    // Define the properties for the form fields
    private string name = "";
    private string special = "";
    private string images = "";
    private string seriesName = "";

    // Handle the change event of the form fields
    private void HandleChange(ChangeEventArgs e)
    {
        var name = e.Value.ToString();
        switch (e.Name)
        {
            case "name":
                this.name = name;
                break;
            case "special":
                this.special = name;
                break;
            case "images":
                this.images = name;
                break;
            case "seriesName":
                this.seriesName = name;
                break;
            default:
                break;
        }
    }

    // Handle the form submission
    private async Task HandleSubmit()
    {
        // Create a new object with the data to be sent to the server
        var newPlayerObj = new
        {
            alsoAppearsIn = new List<string> { "N/A" },
            availability = "Starter",
            images = new
            {
                icon = "",
                portrait = images
            },
            name = name,
            order = "1",
            special = new List<string> { special },
            tier = "A",
            series = new
            {
                icon = "",
                name = seriesName
            }
        };

        // Add newPlayer to the database using HTTP POST
        using (var httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri("Your_Base_URL_Here");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(JsonSerializer.Serialize(newPlayerObj), System.Text.Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("api/characters", content);
            if (response.IsSuccessStatusCode)
            {
                var newPlayerJson = await response.Content.ReadAsStringAsync();
                var newPlayer = JsonSerializer.Deserialize<YourCharacterType>(newPlayerJson);
                HandleNewCharacter(newPlayer);
            }
        }

        
        name = "";
        special = "";
        images = "";
        seriesName = "";
    }

   
    private void HandleNewCharacter(YourCharacterType newPlayer)
    {
        
    }
}
