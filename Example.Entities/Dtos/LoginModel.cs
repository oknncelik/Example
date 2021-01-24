#region

using Example.Entities.Abstract;

#endregion

namespace Example.Entities.Dtos
{
    public class LoginModel : IModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}