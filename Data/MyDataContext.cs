using BOOKS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BOOKS.Data
{
    public class MyDataContext: DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;


        public MyDataContext(DbContextOptions<MyDataContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Комедия"},
                new Genre { Id = 2, Name = "Трагедия" },
                new Genre { Id = 3, Name = "Драма" },
                new Genre { Id = 4, Name = "Ужасы" },
                new Genre { Id = 5, Name = "Баллада" },
                new Genre { Id = 6, Name = "Роман" },
                new Genre { Id = 7, Name = "Триллер" },
                new Genre { Id = 8, Name = "Повесть" },
                new Genre { Id = 9, Name = "Поэма" },
                new Genre { Id = 10, Name = "Научные" },
                new Genre { Id = 11, Name = "Техническая литература" },
                new Genre { Id = 12, Name = "Боевик" },
                new Genre { Id = 13, Name = "Приключения" },
                new Genre { Id = 14, Name = "Рассказ" },
                new Genre { Id = 15, Name = "Биографический" },
                new Genre { Id = 16, Name = "Пьеса" }
                );

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Лев", Surname = "Толстой"},
                new Author { Id = 2, Name = "Роберт", Surname = "Кийосаки" },
                new Author { Id = 3, Name = "Роберт", Surname = "Мартин" },
                new Author { Id = 4, Name = "Мартин", Surname = "Фаулер" },
                new Author { Id = 5, Name = "Джефри", Surname = "Рихтер" },
                new Author { Id = 6, Name = "Марк", Surname = "Прайс" },
                new Author { Id = 7, Name = "Александр", Surname = "Пушкин" },
                new Author { Id = 8, Name = "Джордж", Surname = "Оруэл" },
                new Author { Id = 9, Name = "Эндрю", Surname = "Таненбаум" },
                new Author { Id = 10, Name = "Наполеон", Surname = "Хилл" },
                new Author { Id = 11, Name = "Михаил", Surname = "Булгаков" },
                new Author { Id = 12, Name = "Иван", Surname = "Тургенев" },
                new Author { Id = 13, Name = "Николай", Surname = "Гоголь" },
                new Author { Id = 14, Name = "Фёдор", Surname = "Достоевский" }
                );
        }
    }
}
