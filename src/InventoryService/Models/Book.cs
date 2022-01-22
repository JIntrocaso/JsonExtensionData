namespace InventoryService.Models
{
    public class Book
    {
        public string ISBN { get; set; } = string.Empty;
        public int Copies { get; set; } = 0;
        public string Row { get; set; } = string.Empty;

    }
}
