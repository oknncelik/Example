using Example.Entities.Abstract;

namespace Example.Entities.Dtos
{
    public class CategoryModel : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}