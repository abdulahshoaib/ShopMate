namespace ShopMate.DTOs
{
    public class CustomerDTO
    {
        public CustomerDTO()
        {
            Name = Phone = Gender = Address = "";
            Age = 5;
        }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
    }

}


