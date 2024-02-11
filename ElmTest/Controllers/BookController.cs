using ElmTest.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace FecebookAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBook _book;

        public BookController(IBook book)
        {
            _book = book;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<ActionResult> GetAll(string? keyword, int pageNumber, int pageSize)
        {
            return Ok(await _book.GetAll(keyword, pageNumber, pageSize));
        }

    }
}
