using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace queries.Data.Tables
{
    internal class Student
    {
        [Key]
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Class { get; set; }
        public virtual List<Grade> Grades { get; set; }
    }
}
