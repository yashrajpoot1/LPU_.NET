using System.ComponentModel.DataAnnotations;

namespace MVC_Core_WebApp1.Models
{
    public class Student
    {
        [Required(ErrorMessage = "Roll No Cant be left blank.")]
        public int RollNo { get; set; }

        [Required(ErrorMessage = "Name Cant be left blank.")]
        [StringLength(15,MinimumLength =2,  ErrorMessage ="Name's min length is 2 Char and maxlength is 15")]
        public string Name { get; set; }

        [Range(18, 60, ErrorMessage ="Age is Invalid")]
        public int Age { get; set; }

        public string Address { get; set; }
    }
}
