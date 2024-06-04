using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class Student
    {
        public int StudentId { get; set; }
        [StringLength(50)] 
        public string? Name { get; set; }
        public bool Registered { get; set; }
        public ICollection<Attendance> Attendances { get; set;}
    }
}
