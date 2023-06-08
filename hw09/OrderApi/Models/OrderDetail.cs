namespace OrderApi.Models
{
    public class OrderDetail
    {
        public string Id { get; set; }
        //public int Index { get; set; }
        public string Good { get; set; }
        public double Price { get; set; }
        public string OrderId { get; set; }
        //public Order Order { get; set; }
        public OrderDetail()
        {
            Id = Guid.NewGuid().ToString();
        }
        public OrderDetail(string good, double price)
        {
            Id = Guid.NewGuid().ToString();
            //Index = index;
            Good = good;
            Price = price;
        }
        public override string ToString()
        {
            return $"[Id.:{Id},goods:{Good},price:{Price}]";
        }
    }
}
