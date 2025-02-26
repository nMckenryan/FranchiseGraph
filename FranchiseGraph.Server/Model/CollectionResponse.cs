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
            Results = results;
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

    public class Part
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }

        public Part(string backdropPath, int id, string title, string originalTitle, string overview, string posterPath, string mediaType, bool adult, string originalLanguage, List<int> genreIds, double popularity, string releaseDate, bool video, double voteAverage, int voteCount)
        {

            Id = id;
            Title = title;
            Overview = overview;
            poster_path = posterPath;
            release_date = releaseDate;
            vote_average = voteAverage;
            vote_count = voteCount;
        }
    }

    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public string PosterPath { get; set; }
        public List<Part> Parts { get; set; }
        public double Popularity { get; set; }

        public Collection(int id, string name, string overview, string posterPath, string backdropPath, List<Part> parts, double popularity)
        {
            Id = id;
            Name = name;
            Overview = overview;
            PosterPath = posterPath;
            Parts = parts;
            Popularity = popularity;
        }
    }
}