
using server.Models.DB;

namespace server.DTO
{
    public class OrderDto
    {
        public int UserId { get; set; }
        public string? PaymentType {  get; set; }
        public int? Total { get; set; }
        public List<OrderItemInputDto> OrderedItems { get; set; }
    }

    public class OrderReturnDto
    {
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public int? PaymentId { get; set; }
        public DateOnly? OrderDate { get; set; }
        public int? Total { get; set; }

    }

    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public int? OrderId { get; set; }
        public int? BookId { get; set; }
        public int? Quantity { get; set; }
    }

    public class OrderItemInputDto
    {
        public int? BookId { get; set; }
        public int? Quantity { get; set; }
    }
}


