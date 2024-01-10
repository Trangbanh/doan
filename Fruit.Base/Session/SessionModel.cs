namespace FruitKha.Base.Session
{
    public class AdminSession
    {
        public string SessionId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
    }
    public class CustomerSession
    {
        public string SessionId { get; set; }
        public int CustomerId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
    public class CartObject
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string Images { get; set; }
    }
    public class Cart
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
