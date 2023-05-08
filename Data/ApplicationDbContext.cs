using EmployeeSkillsManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSkillsManagement.Data;
public class ApplicationDbContext :DbContext{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)

    {
        
    }
   public DbSet<Employee> Employees { get; set;}
   public DbSet<Skill> Skills { get; set;}
   public DbSet<Admin> Admins { get; set;}
   public DbSet<EmployeeSkill> EmployeeSkills { get; set;}
   
}