namespace ECommerceAPI.Core.Entities
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }          // Primary Key
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }  // Foreign Key
        public virtual Category Category { get; set; }
    }
}
