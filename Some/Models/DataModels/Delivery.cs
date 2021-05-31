using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.Attributes;

namespace CourseProject.Models.DataModels
{
    [TableName("Delivery")]
    public class Delivery
    {
        public string Adress { get; set; }

        public DateTime Date { get; set; }
    }
}
