﻿using Example.Entities.Abstract;

namespace Example.Entities.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string EMail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool ActiveFlg { get; set; }
    }
}
