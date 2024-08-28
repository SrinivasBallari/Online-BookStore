
namespace server.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public int? PaymentId { get; set; }
        public DateOnly? OrderDate { get; set; }
        public int? Total { get; set; }
    }

    public class OrderItemDTO
    {
        public int OrderItemId { get; set; }
        public int? OrderId { get; set; }
        public int? BookId { get; set; }
        public int? Quantity { get; set; }
    }
}


