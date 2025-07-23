namespace BookGrid.Models.DTOS
{
    public class BookDTO
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
    }
}
