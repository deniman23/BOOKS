﻿namespace BOOKS.Models.Entities
{
    //Жанр
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> books { get; set; } = new();  //
    }
}
