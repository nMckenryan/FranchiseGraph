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
    public IEnumerable<OMDBResponse> Get()
    {
        // Return some sample data or fetch from a database
        return new List<OMDBResponse>
            {
                new OMDBResponse { Title = "Inception", Year = "2010", Poster = "https://example.com/inception.jpg", Metascore = "74", ImdbRating = 8.8f },
                                new OMDBResponse { Title = "Inception", Year = "2010", Poster = "https://example.com/inception.jpg", Metascore = "745", ImdbRating = 8.8f },
                // Add more sample data
            };
    }
}