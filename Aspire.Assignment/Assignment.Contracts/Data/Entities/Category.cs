namespace Assignment.Contracts.Data.Entities
{
    public class Category:BaseEntity
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
    }
}
