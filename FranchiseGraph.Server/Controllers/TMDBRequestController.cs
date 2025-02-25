using FranchiseGraph.Server.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FranchiseGraph.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class TMDBRequestController : ControllerBase
{
    private HttpClient _httpClient;
    private readonly string _apiKey;

    private readonly ILogger<TMDBRequestController> _logger;

    public TMDBRequestController(ILogger<TMDBRequestController> logger, IConfiguration configuration, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _logger = logger;
        _apiKey = configuration["TheMovieDB:APIToken"] ?? throw new InvalidOperationException("TMDB API key is not set.");
    }

    [HttpGet("getTMDBCollectionData")]
    public async Task<IEnumerable<CollectionResult>> retrieveCollection(string collectionSearch)
    {
        string urlTVDB = $"https://api.themoviedb.org/3/search/collection?query={collectionSearch}&include_adult=false&language=en-US";

        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
            var response = await _httpClient.GetAsync(urlTVDB);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            CollectionResponse collectionResponse = JsonConvert.DeserializeObject<CollectionResponse>(responseBody);

            return collectionResponse?.Results ?? Enumerable.Empty<CollectionResult>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching Collection data from OMDB API");
            return Enumerable.Empty<CollectionResult>();
        }
    }

    [HttpGet("getMoviesFromCollection")]
    public async Task<IEnumerable<Part>> retrieveMoviesFromCollection(int collectionId)
    {
        string urlTVDB = $"https://api.themoviedb.org/3/collection/{collectionId}";

        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
            var response = await _httpClient.GetAsync(urlTVDB);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            Collection collectionResponse = JsonConvert.DeserializeObject<Collection>(responseBody);

            return collectionResponse.Parts;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching Movie data from Collection ID");
            return Enumerable.Empty<Part>();
        }
    }
}