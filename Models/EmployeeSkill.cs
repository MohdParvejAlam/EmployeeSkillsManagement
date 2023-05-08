using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeSkillsManagement.Models
{
    public class EmployeeSkill
    {
        
    [Key]
    public int Id { get; set; }
    [ForeignKey("Skill")]
    public int? SkillId { get; set;}
     public Skill Skill { get; set;}
    [ForeignKey("Employee")]
    public int EmployeeId { get; set;}
    public Employee Employee {get; set;}
    [Required]
    [Display(Name ="Expert Level")]
    public string? Expert_Level { get; set;}
    [Required]
    public int Experience { get; set;}
    // public Level Level { get; set;}
     
    }
    // public enum Level
    // {
    //     beginner, 
    //     developer, 
    //     Sr_developer, 
    //     architect, 
    //     others
    // }
    
}