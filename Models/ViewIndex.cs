using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSkillsManagement.Models
{
    public class ViewIndex
    {
        public List<Employee> Employees{get; set;}
       public List<Skill> Skills{get; set;}
      public EmployeeSkill EmployeeSkills {get; set;}
    }
}