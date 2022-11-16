using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace queries.Data.Tables
{
    internal class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public virtual List<Grade> Grades { get; set; }
    }
}
