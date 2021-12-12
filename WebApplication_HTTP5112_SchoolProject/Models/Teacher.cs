using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_HTTP5112_SchoolProject.Models
{
    public class Teacher
    {
        // The following fields defines a Teacher
        public int TeacherId { get; set; }
        [Required]
        public string Teacherfname { get; set; }
        [Required]
        public string Teacherlname { get; set; }
        [Required]
        public string Employeenumber { get; set; }
      
        public string HireDate { get; set; }
        public string Salary { get; set; }

        //parameter-less consructor
        public Teacher() { }
    }

  
}