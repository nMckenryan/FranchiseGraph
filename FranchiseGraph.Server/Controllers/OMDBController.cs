using FranchiseGraph.Server.Model;
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


    [HttpGet("getTMDBCollectionHead")]
    public async Task<IEnumerable<CollectionResponse>> GetTMDBCollectionHead(string collectionSearch)
    {
        string urlTVDB = "https://api.themoviedb.org/3/search/collection?query=" + collectionSearch + "&include_adult=false&language=en-US";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(urlTVDB);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to fetch collection data from TMDB API. Status Code: {response.StatusCode}");
                return null;
            }

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonDocument>();

            CollectionResponse collectionResponse = JsonSerializer.Deserialize<CollectionResponse>(jsonResponse);


            // Return some sample data or fetch from a database
            return new List<CollectionResponse>
            {
                collectionResponse
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching Collection data from OMDB API");
            return null;
        }
    }


    [HttpGet("getTMDBCollectionRecords")]
    public async Task<IEnumerable<CollectionDetail>> GetTMDBCollectionRecords(int collectionId)
    {
        string urlTVDB = "https://api.themoviedb.org/3/search/collection/" + collectionId;

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(urlTVDB);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to fetch collection record data from TMDB API. Status Code: {response.StatusCode}");
                return null;
            }

            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonDocument>();

            CollectionDetail collectionResponse = JsonSerializer.Deserialize<CollectionDetail>(jsonResponse);


            // Return some sample data or fetch from a database
            return new List<CollectionDetail>
            {
                collectionResponse
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching collection record data from OMDB API");
            return null;
        }
    }

}