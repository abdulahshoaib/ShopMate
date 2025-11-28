namespace ShopMate.DTOs
{
    public class CustomerDTO
    {
        public CustomerDTO()
        {
            Name = Phone = Email = Address = "";

        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
    }

}


