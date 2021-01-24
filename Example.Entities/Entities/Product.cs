using Example.Entities.Abstract;

namespace Example.Entities.Entities
{
    public class Product: IEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}