using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Text.Json;

namespace FranchiseGraph.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieAPIController : ControllerBase
{
    private string url = "https://www.omdbapi.com/";
    private HttpClient _httpClient;
    private readonly string _apiKey;

    private readonly ILogger<MovieAPIController> _logger;

    public MovieAPIController(ILogger<MovieAPIController> logger, IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _logger = logger;
        _apiKey = configuration["OMDB:ApiKey"] ?? throw new InvalidOperationException("OMDB API key is not set.");

    }

    [HttpGet(Name = "getOMDBData")]
    public async Task<JsonDocument> Get()
    {

        string end = "?t=captain+america&y=1990";
        string request = url + end + "&apikey=" + _apiKey;

        using HttpResponseMessage response = await _httpClient.GetAsync(request);

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadFromJsonAsync<JsonDocument>();

        //OMDBResponse omdb = JsonSerializer.Deserialize<OMDBResponse>(jsonResponse)!;

        return jsonResponse;
    }

    //[HttpGet]
    //public async Task<JsonDocument> GetWeatherData<JsonDocument>(string url)
    //{
    //    try
    //    {
    //        var response = await _httpClient.GetAsync(url);

    //        response.EnsureSuccessStatusCode();

    //        var weatherData = await response.Content.ReadFromJsonAsync<JsonDocument>();
    //        return weatherData;
    //    }
    //    catch (HttpRequestException ex)
    //    {
    //        Console.WriteLine($"HTTP request error: {ex.Message}");
    //        throw;
    //    }
    //    catch (JsonException ex)
    //    {
    //        Console.WriteLine($"JSON parsing error: {ex.Message}");
    //        throw;
    //    }
    //    catch (Exception ex)
    //    {
    //        // handle any other exceptions
    //        Console.WriteLine($"Error: {ex.Message}");
    //        throw;
    //    }
    //}
}
