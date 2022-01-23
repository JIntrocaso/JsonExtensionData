using PurchasingService.Models;
using System.Text.Json;

namespace PurchasingService
{
    public class BookService
    {
        private readonly NotificationService _notificationService;
        private static readonly JsonSerializerOptions serializerOptions = new() { PropertyNameCaseInsensitive = true };

        public BookService(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public IEnumerable<Book> GetBooks()
        {
            var inputFileName = "books.json";

            string fileText = File.ReadAllText(inputFileName);

            return JsonSerializer.Deserialize<IEnumerable<Book>>(fileText, serializerOptions) ?? new List<Book>();
        }

        public void UpdateInventory(Book book, int changeInUnits)
        {
            book.Copies += changeInUnits;
            var notification = new BookMessage(sender: Constants.Sender, eventType: "update-inventory", book);
            _notificationService.SendNotification(notification);
        }
    }
}
