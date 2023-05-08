using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeSkillsManagement.Data;
using EmployeeSkillsManagement.Models;

namespace EmployeeSkillsManagement.Controllers
{
    // [Route("[controller]")]
    public class EmployeeSkillController : Controller
    {
        static int empId;
        private readonly ApplicationDbContext _db;

        public EmployeeSkillController(ApplicationDbContext db)
        {
             _db = db;
        }

        public IActionResult Index(int id)
        {
            // IEnumerable<EmployeeSkill> objEmployeeSkillList = _db.EmployeeSkills.ToList(); 
            var esData = _db.EmployeeSkills.ToList();
            var sData = _db.Skills.ToList();
            List<ViewSkill> vs = new List<ViewSkill>();
            foreach (EmployeeSkill item in esData)
            {
                if(item.EmployeeId == id)
                {
                    ViewSkill temp = new ViewSkill();
                    temp.empSkillId = item.Id;
                    temp.experience = item.Experience;
                    temp.level = item.Expert_Level;
                    var t = sData.FirstOrDefault(s => s.Id == item.SkillId);
                    temp.name = t.Skills;
                    vs.Add(temp);
                }
            }
         
            return View(vs);
            
        }
        
          public IActionResult Create(int Id)
        {
            empId = Id;
            return View(_db.Skills.ToList());
            //  ViewIndex Vi = new ViewIndex();
            // Vi.Employees= _db.Employees.ToList();
            // Vi.Skills = _db.Skills.ToList();
            // Vi.EmployeeSkills = new EmployeeSkill();
            // EmployeeSkill employeeSkill = new EmployeeSkill();
            // return PartialView("Create",employeeSkill);
            
            
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int skillId, int experience, string level)
        {
            EmployeeSkill es = new EmployeeSkill();
            es.EmployeeId= empId;
            es.SkillId = skillId;
            es.Experience = experience;
            es.Expert_Level = level;
           _db.EmployeeSkills.Add(es);
           _db.SaveChanges();
           TempData["success"]="Skill added successfully";
            return RedirectToAction("Index","Employee");
           
        }
        //GET
        public IActionResult Delete(int? id)
        {
           if(id==null || id ==0){
                return NotFound();
            }
            var employeeSkillFromDb = _db.EmployeeSkills.Find(id);
          
            if(employeeSkillFromDb == null){
                return NotFound();
            }
            _db.EmployeeSkills.Remove(employeeSkillFromDb);
           _db.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.EmployeeSkills.Find(id);
            if(obj == null){
                return NotFound();
            }
          
           _db.EmployeeSkills.Remove(obj);
           _db.SaveChanges();
           TempData["success"]="Skill deleted successfully";
            return RedirectToAction("Index","Employee");
           
        }
    }
}