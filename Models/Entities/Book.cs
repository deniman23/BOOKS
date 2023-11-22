namespace BOOKS.Models.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }   //название книги
        public string Description { get; set; }     //описание
        public List<Genre> genres { get; set; } = new();    //жанры книги
        public List<Author> authors { get; set; } = new();    //авторы (может быть один или несколько)

    }
}
