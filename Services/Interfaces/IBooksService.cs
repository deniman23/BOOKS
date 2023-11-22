using BOOKS.Models.Entities;

namespace BOOKS.Services.Interfaces
{
    public interface IBooksService
    {
        Book Create(Book film);
        Book Update(Book film);
        Book Get(int id);
        List<Book> Get();
        void Delete(int id);

        List<Genre> GetGenres();
        List<Author> GetActors();

        List<Book> Search(string query);
    }
}
