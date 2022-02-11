using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentManagement.Entity.Students
{
    public class Department
    {
        [Key]
        public Guid Id { get; set; }
        [Required()]
        public string Name { get; set; }
        [DisplayName("Department Head")]
        public Guid? DepartmentHeadId { get; set; }
        [ForeignKey("DepartmentHeadId")]
        public virtual  Teacher DepartmentHead { get; set; }
        public bool RecordStatus { get; set; }
    }
}