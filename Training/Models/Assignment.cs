﻿using System.Collections.Generic;
using Training.Data;

namespace Training.Models
{
    public class Assignment : BaseEntity, IAssignment
    {
        public string Name { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public string CorrectAnswer { get; set; }

        public int Grade { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}