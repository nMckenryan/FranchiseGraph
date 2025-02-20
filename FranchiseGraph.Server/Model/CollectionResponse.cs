
namespace FranchiseGraph.Server.Model
{
    public class CollectionResponse
    {
        public int Page { get; set; }
        public List<CollectionResult> Results { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }

        public CollectionResponse(int page, List<CollectionResult> results, int totalPages, int totalResults)
        {
            Page = page;
            results = new List<CollectionResult>();
            TotalPages = totalPages;
            TotalResults = totalResults;
        }
    }

    public class CollectionResult
    {
        public string BackdropPath { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string Overview { get; set; }
        public string PosterPath { get; set; }

        public CollectionResult(string backdropPath, int id, string name, string originalName, string overview, string posterPath)
        {
            BackdropPath = backdropPath;
            Id = id;
            Name = name;
            OriginalName = originalName;
            Overview = overview;
            PosterPath = posterPath;

        }
    }


    public class CollectionDetail
    {
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public float VoteAverage { get; set; }
        public string PosterPath { get; set; }

        public CollectionDetail(string title, string originalTitle, float voteAverage, string posterPath)
        {
            Title = title;
            OriginalTitle = originalTitle;
            VoteAverage = voteAverage;
            PosterPath = posterPath;
        }
    }
}
