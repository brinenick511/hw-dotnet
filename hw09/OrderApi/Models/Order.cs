namespace OrderApi.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public string Customer { get; set; }
        public List<OrderDetail> Details { get; set; }
        public Order()
        {
            OrderId = Guid.NewGuid().ToString();
            Details = new List<OrderDetail>();
        }
        public Order(string customer, List<OrderDetail> details) : this()
        {
            OrderId = Guid.NewGuid().ToString();
            Customer = customer;
            Details = details;
        }
    }
}
