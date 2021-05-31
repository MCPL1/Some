using CourseProject.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models.DataModels
{
    [TableName("Provider")]
    public class Provider : Entity
    {
        public string Name { get; set; }

    }
}
