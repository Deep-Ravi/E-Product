namespace Assignment.Contracts.Data.Entities
{
    public class Product:BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
