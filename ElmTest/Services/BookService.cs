using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Migrations.Data;
using Migrations.Models;
using Newtonsoft.Json;

namespace ElmTest.Controllers
{
    public class BookService : IBook
    {
        private readonly ElmTestDbContext _context;

        public BookService(ElmTestDbContext context)
        {
            _context = context; 
        }
      
        public async Task<List<BookDto>> GetAll(string? keyword, int pageNumber, int pageSize)
        
        {

            var q = _context.Books.AsQueryable();

            q = !string.IsNullOrWhiteSpace(keyword) ? q.Where(b => b.BookInfo.Contains(keyword)) : q;

            var result = await q.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            List<BookDto> list = new List<BookDto>(); 
            foreach (var item in result)
            {
                BookDto dto = new BookDto();
                var bookInfoData = JsonConvert.DeserializeObject<BookInfoDto>(item.BookInfo) ?? null;
                dto.BookId = item.BookId;
                dto.LastModified = item.LastModified;
                dto.BookInfo = bookInfoData;
                list.Add(dto);
            }  
            return list;
        }

    }
}
