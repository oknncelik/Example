﻿#region

using Example.Entities.Abstract;

#endregion

namespace Example.Entities.Dtos
{
    public class UserInfoModel : IModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string EMail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}