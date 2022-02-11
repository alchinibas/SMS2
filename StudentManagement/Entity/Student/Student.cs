using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentManagement.Entity.Students
{
    public class Student
    {
        [Key]
        public Guid Id { get; set; }
        [Required()]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [Required()]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        [Required()]
        public string Mobile { get; set; }
        [Required()]
        [DisplayName("Enrolled Year")]
        public string EnrolledYear { get; set; }
        [Required()]
        [DisplayName("Department")]
        public Guid DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
        [DisplayName("First Name")]
        public string GuardianFirstName { get; set; }
        [DisplayName("Middle Name")]
        public string GuardianMiddleName { get; set; }
        [DisplayName("Last name")]
        public string GuardianLastName { get; set; }
        [DisplayName("Guardian Full Name")]
        public string GuardianFullName { get; set; }
        [DisplayName("Mobile")]
        public string GuardianContact { get; set; }
        public bool RecordStatus { get; set; }
    }
}