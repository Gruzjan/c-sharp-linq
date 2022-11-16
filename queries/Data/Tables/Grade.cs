using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace queries.Data.Tables
{
    internal class Grade
    {
        [Key]
        public int GradeId { get; set; }
        public DateTime Date { get; set; }
        public int Value { get; set; }
        public string StudentId { get; set; }
        public virtual Student Student { get; set; }
        public int SubjectId { get; set; }
        public virtual Subject Subject{ get; set; }

    }
}
