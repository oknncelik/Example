#region

using Example.Entities.Abstract;

#endregion

namespace Example.Entities.Entities
{
    public class OperationClaim : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}