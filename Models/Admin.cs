using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmployeeSkillsManagement.Models
{
    public class Admin
    {
        [Key]
    public int ADMINID { get; set; }
    [Required]
    [Display(Name="Username")]
    [DataType(DataType.EmailAddress)]
    public string? EMAIL { get; set; }
    [Required]
    [Display(Name="Password")]
    [DataType(DataType.Password)]
    public string? PASSWORD { get; set; }

    }
}