using System;
using System.Runtime.InteropServices.JavaScript;

namespace MyApp.Models
{
    public class User : ModelBase
    {
        public string Forename { get; set; }

        public string Surname { get; set; }

        public bool IsActive { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}