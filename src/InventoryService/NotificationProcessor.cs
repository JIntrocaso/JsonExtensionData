using InventoryService.Models;
using System.Text.Json;

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
                var extensionData = notification.Properties;
                var payload = extensionData.GetValueOrDefault("Payload");
                var payloadDictionary = GetDictionaryFromPayload(payload!);
                var change = GetIntegerOrDefaultFromPayload(payloadDictionary, "Copies");
                var isbn = GetStringOrDefaultFromPayload(payloadDictionary, "ISBN");
                var book = _bookService.GetBookByISBN(isbn);
                if (book is not null)
                {
                    _bookService.UpdateInventory(book, change);
                }
            }
        }

        private Dictionary<string, object> GetDictionaryFromPayload(object payload)
            => payload is JsonElement { } element ? JsonSerializer.Deserialize<Dictionary<string, object>>(element) ?? new Dictionary<string, object>() : new Dictionary<string, object>();

        private static int GetIntegerOrDefaultFromPayload(Dictionary<string, object>? dictionary, string key)
        {
            if (dictionary?.TryGetValue(key, out var value) == true && value is JsonElement { ValueKind: JsonValueKind.Number } && Int32.TryParse(value.ToString(), out var number))
            {
                return number;
            }
            return 0;
        }

        private static string? GetStringOrDefaultFromPayload(Dictionary<string, object>? dictionary, string key)
            => dictionary?.TryGetValue(key, out var stringValue) == true && stringValue is JsonElement { ValueKind: JsonValueKind.String } ? stringValue.ToString() : null;
    }
}
