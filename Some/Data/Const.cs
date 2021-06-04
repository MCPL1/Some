using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.Models.DataModels;

namespace CourseProject.Data
{
    public  class Const
    {
        public readonly Role AdminRole;
        public readonly Role UserRole ;

        public readonly Status NewOrder = new Status() {Id = 1, Name = "NewOrder"};

        public const string Admin = "admin";
        public const string User = "user";

        public const string All = "admin, user";

        public Const()
        {
            AdminRole = new Role(1, "admin");
            UserRole = new Role(2, "user");
        }
    }
}