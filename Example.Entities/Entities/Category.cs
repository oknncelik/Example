﻿using Example.Entities.Abstract;

namespace Example.Entities.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}