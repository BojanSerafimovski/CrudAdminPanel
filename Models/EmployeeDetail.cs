namespace MVC_CRUD_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class EmployeeDetail
    {
        public int EmployeeID { get; set; }
        [Required(ErrorMessage = "Please fill this field.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please fill this field.")]
        public string Position { get; set; }
        public string City { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<int> Salary { get; set; }
    }
}
