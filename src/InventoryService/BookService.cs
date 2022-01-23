using InventoryService.Models;
using System.Text.Json;

namespace InventoryService
{
    public class BookService
    {
        private static readonly IEnumerable<Book> books = GetBooks();
        private static readonly JsonSerializerOptions serializerOptions = new() { PropertyNameCaseInsensitive = true };
        public static IEnumerable<Book> GetBooks()
        {
            var inputFileName = "books.json";

            string fileText = File.ReadAllText(inputFileName);

            return JsonSerializer.Deserialize<IEnumerable<Book>>(fileText, serializerOptions) ?? new List<Book>();
        }

        public Book? GetBookByISBN(string isbn)
            => books.FirstOrDefault(b => b.ISBN == isbn);

        public void UpdateInventory(Book book, int changeInInventory)
        {
            book.Copies += changeInInventory;
            Console.WriteLine(CreateInventoryMessage(book));
        }

        private string CreateInventoryMessage(Book book)
            => book.Copies switch
            {
                < 10 => $"DANGER: Low inventory for book ISBN: {book.ISBN}!! Only {book.Copies} are left!!",
                _ => $"Inventory updated for book ISBN {book.ISBN}. Copies: {book.Copies}"
            };
    }
}
