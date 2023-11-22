using BOOKS.Data;
using BOOKS.Models.Entities;
using BOOKS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BOOKS.Services
{
    public class BooksService : IBooksService
    {
        private MyDataContext _dataContext;
        public BooksService(MyDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Book Create(Book book)
        {
            var newFilm = new Book()
            {
                Title = book.Title,
                Description = book.Description
            };
            foreach(var a in book.authors)
            {
                var actor = _dataContext.Authors.FirstOrDefault(x => x.Id == a.Id);
                if (actor != null)
                {
                    newFilm.authors.Add(actor);
                }
            }
            foreach (var g in book.genres)
            {
                var genre = _dataContext.Genres.FirstOrDefault(x => x.Id == g.Id);
                if (genre != null)
                {
                    newFilm.genres.Add(genre);
                }
            }
            _dataContext.Books.Add(newFilm);
            _dataContext.SaveChanges();
            return book;
        }

        public Book Update(Book film)
        {
            var bookToUpdate = _dataContext.Books.FirstOrDefault(x => x.Id == film.Id);
            if (bookToUpdate != null)
            {
                bookToUpdate.Title = film.Title;
                bookToUpdate.Description = film.Description;
                _dataContext.SaveChanges();
            }
            return bookToUpdate;
        }

        public void Delete(int id)
        { 
            var bookToDelete = _dataContext.Books.FirstOrDefault(x => x.Id == id);
            if (bookToDelete != null)
            {
                _dataContext.Books.Remove(bookToDelete);
                _dataContext.SaveChanges();
            }
        }

        public Book Get(int id)
        {
            return _dataContext.Books.FirstOrDefault(x => x.Id == id);
        }

        public List<Book> Get()
        {
            return _dataContext.Books
                .Include(x => x.authors)
                .Include(x => x.genres)
                .ToList();
        }

        public List<Genre> GetGenres()
        {
            return _dataContext.Genres.ToList();
        }

        public List<Author> GetActors()
        {
            return _dataContext.Authors.ToList();
        }

        public List<Book> Search(string query)
        {
            return _dataContext.Books.
                Where(x => x.Title.ToLower().Contains(query.ToLower()) 
                || x.Description.ToLower().Contains(query.ToLower())
                || x.authors.Where(y => y.Name.ToLower().Contains(query.ToLower()) || y.Surname.ToLower().Contains(query.ToLower())).ToList().Count > 0
                || x.genres.Where(z => z.Name.ToLower().Contains(query.ToLower())).ToList().Count > 0
                )
                .Include(x => x.authors)
                .Include(x => x.genres)
                .ToList();
        }
    }
}
