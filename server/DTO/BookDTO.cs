namespace server.DTO
{
    public class BooksDTO{
        public required int BookId { get; set; }

        public required string Title {get; set;}
        
        public required decimal Price {get; set;}

        public required string ImageUrl {get; set;}
    }
}