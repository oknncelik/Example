#region

using Example.Entities.Abstract;

#endregion

namespace Example.Entities.Dtos
{
    public class CategoryModel : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}