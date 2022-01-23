
namespace PurchasingService.Models
{
    public class BookMessage : PublishMessage
    {

        public BookMessage(string sender, string eventType, Book book) : base(sender, eventType, book)
        {
            Sender = sender;
            Event = eventType;
            Payload = book;
        }

    }
}
