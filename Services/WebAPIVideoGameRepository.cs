using DSF22CSCI3110Lab05.Models.Entities;
using System.Text.Json;

namespace DSF22CSCI3110Lab05.Services;
public class WebAPIVideoGameRepository : IVideoGameRepository
{
    private readonly HttpClient _client;

    public WebAPIVideoGameRepository(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri("https://localhost:7176/api/");
    }

    public async Task<ICollection<VideoGame>> ReadAllAsync()
    {
        List<VideoGame>? games = null;
        // HTTP GET
        var response = await _client.GetAsync("videogame/all");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            games = JsonSerializer.Deserialize<List<VideoGame>>(json, serializeOptions);
        }

        games ??= new List<VideoGame>();
        return games;
    }

    public async Task<VideoGame?> CreateAsync(VideoGame videoGame)
    {
        var gameData = new FormUrlEncodedContent(new Dictionary<string, string>()
        {
            ["Id"] = $"{videoGame.Id}",
            ["Title"] = $"{videoGame.Title}",
            ["Genre"] = $"{videoGame.Genre}",
            ["Developer"] = $"{videoGame.Developer}",
            ["Rating"] = $"{videoGame.Rating}",
            ["Year"] = $"{videoGame.Year}"
        });

        var result = await _client.PostAsync("videogame/create", gameData);

        if(result.IsSuccessStatusCode)
        {
            return videoGame;
        }
        return null;
    }

    public async Task<VideoGame?> ReadAsync(int id)
    {
        VideoGame? game = null;
        // HTTP GET
        var response = await _client.GetAsync($"videogame/one/{id}");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            game = JsonSerializer.Deserialize<VideoGame>(json, serializeOptions);
        }
        return game;
    }

    public async Task UpdateAsync(int oldId, VideoGame videoGame)
    {
        var gameData = new FormUrlEncodedContent(new Dictionary<string, string>()
        {
            ["Id"] = $"{videoGame.Id}",
            ["Title"] = $"{videoGame.Title}",
            ["Genre"] = $"{videoGame.Genre}",
            ["Developer"] = $"{videoGame.Developer}",
            ["Rating"] = $"{videoGame.Rating}",
            ["Year"] = $"{videoGame.Year}"
        });
        var result = await _client.PutAsync("videogame/update", gameData);

        if (!result.IsSuccessStatusCode)
        {
            // May want to do something here
        }
    }

    public async Task DeleteAsync(int id)
    {
        var result = await _client.DeleteAsync($"videogame/delete/{id}");
        if (!result.IsSuccessStatusCode)
        {
            // May want to do something here
        }
    }
}

