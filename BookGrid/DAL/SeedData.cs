using BookGrid.Models;

namespace BookGrid.DAL
{
    public class SeedData
    {
        public static List<Author> Authors = new()
        {
            new Author { Id = 1, Name = "Parijat" },
            new Author { Id = 2, Name = "Laxmi Prasad Devkota" },
            new Author { Id = 3, Name = "Bhupi Sherchan" },
            new Author { Id = 4, Name = "Bishweshwar Prasad Koirala" },
            new Author { Id = 5, Name = "Madhav Prasad Ghimire" },
            new Author { Id = 6, Name = "Dhruba Chandra Gautam" }
        };

        public static List<Publisher> Publishers = new()
        {
            new Publisher { Id = 1, Name = "Nepalaya" },
            new Publisher { Id = 2, Name = "FinePrint" },
            new Publisher { Id = 3, Name = "Ratna Pustak Bhandar" },
            new Publisher { Id = 4, Name = "Sajha Prakashan" },
            new Publisher { Id = 5, Name = "Kathmandu Press" },
            new Publisher { Id = 6, Name = "Jagadamba Publications" }
        };
    }
}
