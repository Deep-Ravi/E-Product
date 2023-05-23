namespace Assignment.Contracts.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
