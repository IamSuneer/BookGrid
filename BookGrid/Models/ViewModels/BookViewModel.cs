namespace BookGrid.Models.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public string AuthorName { get; set; }
        public string PublisherName { get; set; }
    }
}
