namespace BookManagementAPI.API.DTOs
{
    public class UpdateBookDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Language { get; set; }
        public string? Category { get; set; }
    }
}