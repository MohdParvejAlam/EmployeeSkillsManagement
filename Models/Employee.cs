
using System.ComponentModel.DataAnnotations;


namespace EmployeeSkillsManagement.Models
{

    public class Employee
    {

    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(25)]
    public string? FirstName { get; set; }
    [Required]
    [StringLength(25)]
    public string? LastName { get; set; }
     [Required]
     [DataType(DataType.Date)]
     [Display(Name ="Date of Joining")]
    
    public DateTime DOJ { get; set; }
    [Required]
    
    public string? Designation { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    
    public string? Email { get; set; }
     public List<Skill>? Skills{get; set;}
   }
}


