using Sender.Models;
using System.Text.Json;

namespace Sender
{
    public class BookService
    {
        private readonly NotificationService _notificationService;
        public BookService(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public IEnumerable<Book> GetBooks()
        {
            var inputFileName = "books.json";

            string fileText = File.ReadAllText(inputFileName);

            return JsonSerializer.Deserialize<IEnumerable<Book>>(fileText) ?? new List<Book>();
        }

        public void UpdateInventory(Book book, int changeInUnits)
        {
            book.Copies += changeInUnits;
            var notification = new PublishMessage { Event = "update-inventory", Payload = book };
            _notificationService.SendNotification(notification);
        }
    }
}
