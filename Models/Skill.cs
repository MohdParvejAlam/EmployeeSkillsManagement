using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmployeeSkillsManagement.Models
{
    public class Skill
    {
        
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Skills { get; set; }
    }
}