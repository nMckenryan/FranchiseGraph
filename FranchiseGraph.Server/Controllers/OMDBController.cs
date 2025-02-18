using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Text.Json;

namespace FranchiseGraph.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class OMDBController : ControllerBase
{
    private string url = "https://www.omdbapi.com/";
    private HttpClient _httpClient;
    private readonly string _apiKey;

    private readonly ILogger<OMDBController> _logger;

    public OMDBController(ILogger<OMDBController> logger, IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _logger = logger;
        _apiKey = configuration["OMDB:ApiKey"] ?? throw new InvalidOperationException("OMDB API key is not set.");
    }

    [HttpGet("getOMDBData")]
    public async Task<IEnumerable<OMDBResponse>> GetAsync()
    {
        string search = "?t=spiderman";
        string request = url + search + "&apikey=" + _apiKey;

        using HttpResponseMessage response = await _httpClient.GetAsync(request);

        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadFromJsonAsync<JsonDocument>();

        OMDBResponse omdbResponse = JsonSerializer.Deserialize<OMDBResponse>(jsonResponse);

        // Return some sample data or fetch from a database
        return new List<OMDBResponse>
            {
                omdbResponse
            };
    }
}