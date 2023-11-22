using BOOKS.Models.Entities;
using BOOKS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BOOKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        //CRUD для списка фильмов
        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            return _booksService.Create(book);
        }

        [HttpPatch]
        public ActionResult<Book> Update(Book book)
        {
            return _booksService.Update(book);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            return _booksService.Get(id);
        }

        [HttpGet]
        public ActionResult<List<Book>> GetAll()
        {
            return _booksService.Get();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _booksService.Delete(id);
            return Ok();
        }

        //получение списка актёров
        [HttpGet("authors")]
        public ActionResult<List<Author>> GetActors()
        {
            return _booksService.GetActors();
        }

        //получение списка жанров
        [HttpGet("genres")]
        public ActionResult<List<Genre>> GetGenres()
        {
            return _booksService.GetGenres();
        }

        //поиск
        [HttpGet("search={query}")]
        public ActionResult<List<Book>> Search(string query)
        {
            return _booksService.Search(query);
        }
    }
}
