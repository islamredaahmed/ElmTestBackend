using Migrations.Models;

namespace ElmTest.Controllers
{
    public interface IBook
    {
        Task<List<BookDto>> GetAll(string? keyword, int pageNumber, int pageSize);
    }
}
