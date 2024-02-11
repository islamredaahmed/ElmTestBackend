namespace Migrations.Models
{
    public class BookDto
    {
        public long BookId { get; set; }
        public BookInfoDto? BookInfo { get; set; }
        public DateTime LastModified { get; set; }
    }
}
