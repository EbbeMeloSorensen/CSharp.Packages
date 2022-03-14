using BookLibrary;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConsoleApp
{
    internal class Program
    {
        static IEnumerable<Author> CreateFakeData()
        {
            var authors = new List<Author>
            {
                new Author
                {
                    Name = "Jane Austen", Books = new List<Book>
                    {
                        new Book { Title = "Emma", PublicationYear = 1815 },
                        new Book { Title = "Persuasion", PublicationYear = 1818 },
                        new Book { Title = "Mansfield Park", PublicationYear = 1814 }
                    }
                },
                new Author
                {
                    Name = "Ian Fleming", Books = new List<Book>
                    {
                        new Book { Title = "Dr No", PublicationYear = 1958 },
                        new Book { Title = "Goldfinger", PublicationYear = 1959 },
                        new Book { Title = "Froms Russia with Love", PublicationYear = 1957 }
                    }
                }
            };

            return authors;
        }

        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<BooksContext>()
                .UseSqlite("Filename=../../../MyLocalLibrary.db")
                .Options;

            using var db = new BooksContext(options);
            db.Database.EnsureCreated();

            // This block is for populating the database
            //var authors = CreateFakeData();
            //db.Authors.AddRange(authors);
            //db.SaveChanges();

            //foreach (var author in authors) // This was just for displaying the data created with the CreateFakeData method
            //foreach (var author in db.Authors) // This is lazy loading, where it doesn't include the books, therefore causing a null ref error
            foreach (var author in db.Authors.Include(a => a.Books)) // Eager loading, getting it all
            {
                Console.WriteLine($"{author} wrote...");

                foreach(var book in author.Books)
                {
                    Console.WriteLine($"   {book.Title}");
                }

                Console.WriteLine();
            }
        }
    }
}