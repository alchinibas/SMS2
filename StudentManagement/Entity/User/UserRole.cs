using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.Entity.Users
{
    public class UserRole
    {
        [Key]
        public Guid Id { get; set; }
        [Required()]
        public string Name { get; set; }
        public bool RecordStatus { get; set; }
    }
}