using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EY.Core.Domain
{
    [Table("Employes")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        [RegularExpression("[a-zA-Z]+")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(15)]
        [RegularExpression("[a-zA-Z]+")]
        public string LastName { get; set; }
       
        [Required]
        [StringLength(9)]
        [RegularExpression("[0-9]{9,9}")]
        public string SocialId { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime? EndDate { get; set; } 

        public string WorkDescription { get; set; }

        //public int CurrentDepartmentId { get; set; }
        public Department CurrentDepartment { get; set; }
    }
}
