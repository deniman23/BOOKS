namespace BOOKS.Models.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }    //имя автора
        public string Surname { get; set; }     //фамилия
        public int? Age { get; set; }    //возраст
        public List<Book> books { get; set; } = new();  //книги, которые написал автор
    }
}
