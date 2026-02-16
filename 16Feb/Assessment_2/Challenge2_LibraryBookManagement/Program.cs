using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenge2_LibraryBookManagement
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public bool IsAvailable { get; set; }

        public Book(string isbn, string title, string author, string genre)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
            Genre = genre;
            IsAvailable = true;
        }

        public override string ToString()
        {
            return $"{Title} by {Author} [{Genre}] - {(IsAvailable ? "Available" : "Borrowed")}";
        }
    }

    // Generic catalog class
    public class Catalog<T> where T : Book
    {
        private List<T> _items = new List<T>();
        private HashSet<string> _isbnSet = new HashSet<string>();
        private SortedDictionary<string, List<T>> _genreIndex = new SortedDictionary<string, List<T>>();

        // Add item with genre indexing
        public bool AddItem(T item)
        {
            if (item == null)
            {
                Console.WriteLine("Error: Cannot add null item.");
                return false;
            }

            // Check ISBN uniqueness
            if (_isbnSet.Contains(item.ISBN))
            {
                Console.WriteLine($"Error: Book with ISBN {item.ISBN} already exists.");
                return false;
            }

            // Add to main list
            _items.Add(item);
            _isbnSet.Add(item.ISBN);

            // Add to genre index
            if (!_genreIndex.ContainsKey(item.Genre))
            {
                _genreIndex[item.Genre] = new List<T>();
            }
            _genreIndex[item.Genre].Add(item);

            Console.WriteLine($"Added: {item.Title}");
            return true;
        }

        // Get books by genre using indexer
        public List<T> this[string genre]
        {
            get
            {
                if (_genreIndex.ContainsKey(genre))
                {
                    return new List<T>(_genreIndex[genre]);
                }
                return new List<T>();
            }
        }

        // Find books using LINQ and lambda expressions
        public IEnumerable<T> FindBooks(Func<T, bool> predicate)
        {
            return _items.Where(predicate);
        }

        // Additional methods
        public IEnumerable<T> GetAllBooks()
        {
            return _items;
        }

        public IEnumerable<string> GetAllGenres()
        {
            return _genreIndex.Keys;
        }

        public bool RemoveBook(string isbn)
        {
            var book = _items.FirstOrDefault(b => b.ISBN == isbn);
            if (book == null)
                return false;

            _items.Remove(book);
            _isbnSet.Remove(isbn);
            _genreIndex[book.Genre].Remove(book);

            if (_genreIndex[book.Genre].Count == 0)
            {
                _genreIndex.Remove(book.Genre);
            }

            return true;
        }

        public int Count => _items.Count;

        public IEnumerable<T> GetAvailableBooks()
        {
            return _items.Where(b => b.IsAvailable);
        }

        public IEnumerable<T> SearchByTitle(string keyword)
        {
            return _items.Where(b => b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Library Book Management System ===\n");

            Catalog<Book> library = new Catalog<Book>();

            // Test Case 1: Add books
            Console.WriteLine("--- Adding Books ---");
            Book book1 = new Book("978-3-16-148410-0", "C# Programming", "John Sharp", "Programming");
            Book book2 = new Book("978-1-23-456789-0", "Design Patterns", "Gang of Four", "Programming");
            Book book3 = new Book("978-0-12-345678-9", "Clean Code", "Robert Martin", "Programming");
            Book book4 = new Book("978-9-87-654321-0", "The Great Gatsby", "F. Scott Fitzgerald", "Fiction");
            Book book5 = new Book("978-5-55-555555-5", "1984", "George Orwell", "Fiction");

            library.AddItem(book1);
            library.AddItem(book2);
            library.AddItem(book3);
            library.AddItem(book4);
            library.AddItem(book5);

            // Test Case 2: Try to add duplicate ISBN
            Console.WriteLine("\n--- Testing Duplicate ISBN ---");
            Book duplicate = new Book("978-3-16-148410-0", "Duplicate Book", "Test Author", "Test");
            library.AddItem(duplicate);

            // Test Case 3: Get books by genre using indexer
            Console.WriteLine("\n--- Books by Genre (Programming) ---");
            var programmingBooks = library["Programming"];
            Console.WriteLine($"Programming books count: {programmingBooks.Count}"); // Should output: 3
            foreach (var book in programmingBooks)
            {
                Console.WriteLine($"  - {book}");
            }

            // Test Case 4: Find books using lambda expressions
            Console.WriteLine("\n--- Finding Books by Author (Contains 'John') ---");
            var johnsBooks = library.FindBooks(b => b.Author.Contains("John"));
            Console.WriteLine($"Books by authors containing 'John': {johnsBooks.Count()}"); // Should output: 1
            foreach (var book in johnsBooks)
            {
                Console.WriteLine($"  - {book}");
            }

            // Test Case 5: Find books by multiple criteria
            Console.WriteLine("\n--- Programming Books by Specific Authors ---");
            var specificBooks = library.FindBooks(b => 
                b.Genre == "Programming" && 
                (b.Author.Contains("Martin") || b.Author.Contains("Gang")));
            
            foreach (var book in specificBooks)
            {
                Console.WriteLine($"  - {book}");
            }

            // Test Case 6: Borrow books (change availability)
            Console.WriteLine("\n--- Borrowing Books ---");
            book1.IsAvailable = false;
            book4.IsAvailable = false;
            Console.WriteLine($"Borrowed: {book1.Title}");
            Console.WriteLine($"Borrowed: {book4.Title}");

            // Test Case 7: Get available books
            Console.WriteLine("\n--- Available Books ---");
            var availableBooks = library.GetAvailableBooks();
            Console.WriteLine($"Available books count: {availableBooks.Count()}");
            foreach (var book in availableBooks)
            {
                Console.WriteLine($"  - {book}");
            }

            // Test Case 8: Search by title keyword
            Console.WriteLine("\n--- Search by Title (keyword: 'Code') ---");
            var codeBooks = library.SearchByTitle("Code");
            foreach (var book in codeBooks)
            {
                Console.WriteLine($"  - {book}");
            }

            // Test Case 9: Get all genres
            Console.WriteLine("\n--- All Genres ---");
            var genres = library.GetAllGenres();
            foreach (var genre in genres)
            {
                Console.WriteLine($"  - {genre} ({library[genre].Count} books)");
            }

            // Test Case 10: Complex LINQ query
            Console.WriteLine("\n--- Fiction Books Sorted by Title ---");
            var fictionSorted = library["Fiction"]
                .OrderBy(b => b.Title)
                .Select(b => b.Title);
            
            foreach (var title in fictionSorted)
            {
                Console.WriteLine($"  - {title}");
            }

            Console.WriteLine("\n=== Demo Completed Successfully ===");
        }
    }
}
