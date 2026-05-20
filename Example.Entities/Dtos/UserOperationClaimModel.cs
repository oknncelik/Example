using Example.Entities.Abstract;

namespace Example.Entities.Dtos;

public class UserOperationClaimModel : IModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }
}
