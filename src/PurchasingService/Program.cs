using PurchasingService;

var notificationService = new NotificationService();
var bookService = new BookService(notificationService);

var books = bookService.GetBooks();

foreach (var book in books)
{
    bookService.UpdateInventory(book, 20);
}

Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();