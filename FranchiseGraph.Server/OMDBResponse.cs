namespace FranchiseGraph.Server
{
    public class OMDBResponse
    {
        public required string Title { get; set; }
        public required string Year { get; set; }
        public string? Poster { get; set; }
        public string? Metascore { get; set; }
        public float? ImdbRating { get; set; }

    }
}
