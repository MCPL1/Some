using System;
using CourseProject.Attributes;
using CourseProject.Models;
using CourseProject.Models.DataModels;

namespace CourseProject.Identity.Models
{
    [TableName("Roles")]
    public class Role :Entity
    {
        public string Name { get; set; }

        public Role()
        {
            Id = Guid.NewGuid().GetHashCode();
            Id = 3;
        }

        public Role(string name) : this()
        {
            Name = name;
        }

        public Role(int id, string name)
        {
            Name = name;
            Id = id;
        }
    }
}