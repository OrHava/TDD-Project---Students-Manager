using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_Project
{
    public  class Student
    {
    

        public Student(string id, string firstName, string lastName, string email, string grade1, string grade2, string grade3, string grade4, string grade5)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            FiveGradesAndAvg=new List<string> { grade1, grade2, grade3, grade4, grade5 };   
         
        }


        public Student(string id, string firstName, string lastName, string email, string grade1, string grade2, string grade3, string grade4, string grade5, string avg)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            FiveGradesAndAvg = new List<string> { grade1, grade2, grade3, grade4, grade5, avg };

        }

        public string? ID { get; set; }

        public string? FirstName { get; set; }
 
        public string? LastName { get; set; }
    
        public string? Email { get; set; }
 

        public List<String>? FiveGradesAndAvg { get; set; }
  

     
    }
}
