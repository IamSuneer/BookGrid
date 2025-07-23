using System.Text.Json.Serialization;

namespace BookGrid.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public int AuthorId { get; set; }
        [JsonIgnore]
        public Author Author { get; set; }
        public int PublisherId { get; set; }
        [JsonIgnore]
        public Publisher Publisher { get; set; }
    }
}
