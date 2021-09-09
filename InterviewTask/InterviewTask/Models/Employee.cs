using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterviewTask.Models
{

    //    (e.g.EmpId, Name, Email, Designation)


    public class Employee
    {
        public int EmpID { get; set; }
        [Required(ErrorMessage = "Name cannot be empty.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email cannot be blank.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter the Designation.")]
        public string Designation { get; set; }
        //and Employee Details(e.g.EmpId, FileName, FilePath, CreatedDate)

        public string FileName { get; set; }

        public string FilePath { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Please select file.")]
        [Display(Name = "Browse File")]

        public HttpPostedFileBase[] Files { get; set; }

        public List<string> ListOfFiles { get; set; }


    }
}