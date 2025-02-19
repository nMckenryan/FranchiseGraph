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

    public OMDBController(ILogger<OMDBController> logger, IConfiguration configuration, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _logger = logger;
        _apiKey = configuration["OMDB:ApiKey"] ?? throw new InvalidOperationException("OMDB API key is not set.");
    }

    public int testThin(int a, int b)
    {
        return a + b;
    }

    [HttpGet("getOMDBData")]
    public async Task<IEnumerable<OMDBResponse>> GetAsync(string franchiseName)
    {
        string search = "?t=" + franchiseName;
        string request = url + search + "&apikey=" + _apiKey;

        HttpResponseMessage response = await _httpClient.GetAsync(request);

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