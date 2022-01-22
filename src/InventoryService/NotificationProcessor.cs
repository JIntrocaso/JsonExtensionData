using InventoryService.Models;

namespace InventoryService
{
    public class NotificationProcessor
    {
        private readonly BookService _bookService;

        public NotificationProcessor(BookService bookService)
        {
            _bookService = bookService;
        }

        public void Process(Notification notification)
        {
            if (notification.Event == "update-inventory")
            {
                var isbn = "test";
                var change = 0;
                var book = _bookService.GetBookByISBN(isbn);
                if (book is not null)
                {
                    _bookService.UpdateInventory(book, change);
                }
            }
        }
    }
}
