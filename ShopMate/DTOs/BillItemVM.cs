namespace ShopMate.DTOs
{
    public class BillItemVM
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        private int qty;

        public int Quantity
        {
            get => qty;
            set
            {
                qty = value;
                Total = UnitPrice * qty;
            }
        }

        public decimal Total { get; private set; }
    }
}