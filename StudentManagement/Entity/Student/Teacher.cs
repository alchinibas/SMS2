using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentManagement.Entity.Students
{
    public class Teacher
    {
        [Key]
        public Guid Id { get; set; }
        [Required()]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required()]
        public string LastName { get; set; }
        public string FullName { get; set; }
        [Required()]
        [DisplayName("Major Subject")]
        public Guid MajorSubjectId { get; set; }
        [ForeignKey("MajorSubjectId")]
        public virtual MajorSubject MajorSubject { get; set; }
        [Required()]
        [DisplayName("Designation")]
        public Guid DesignationId { get; set; }
        [ForeignKey("DesignationId")]
       
        public virtual Designation Designation { get; set; }
        public string StartYear { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        [Required()]
        public string Mobile { get; set; }
        public bool RecordStatus { get; set; }
    }
}