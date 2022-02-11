using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentManagement.Entity.Users
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        [Required()]
        public string Mobile { get; set; }
        [DisplayName("Role")]
        public Guid RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual UserRole Role { get; set; }
        [Required()]
        public string Email { get; set; }
        [Required()]
        public string Password { get; set; }
        public bool RecordStatus { get; set; }
    }
}