using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.Attributes;

namespace CourseProject.Models.DataModels
{
    [TableName("Status")]
    public class Status : Entity
    {
        public string Name { get; set; }
    }
}