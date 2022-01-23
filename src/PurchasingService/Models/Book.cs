namespace PurchasingService.Models
{
    public class Book
    {
        public string ISBN { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime Published { get; set; } = DateTime.Now;
        public string Publisher { get; set; } = string.Empty;
        public int Pages { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Categories { get; set; } = string.Empty;
        public int Copies { get; set; }
        public decimal Price { get; set; }
    }
}
