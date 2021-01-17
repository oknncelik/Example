using Example.Entities.Abstract;

namespace Example.Entities.Dtos
{
    public class RegisterModel : IModel
    {
        public string UserName { get; set; }
        public string EMail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
