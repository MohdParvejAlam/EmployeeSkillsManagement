using EmployeeSkillsManagement.Data;
using EmployeeSkillsManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSkillsManagement.Controllers{
    public class EmployeeController : Controller{
        private readonly ApplicationDbContext _db;
       public EmployeeController(ApplicationDbContext db)
       {
        _db = db;
       }
        public IActionResult Index()
        {
            ViewIndex Vi = new ViewIndex();
            Vi.Employees= _db.Employees.ToList();
            Vi.Skills = _db.Skills.ToList();
            Vi.EmployeeSkills = new EmployeeSkill();
            // IEnumerable<Employee> objEmployeeList = _db.Employees.ToList(); 
            return View(Vi);
        }
        //GET
        public IActionResult Create()
        {
           // IEnumerable<Employee> objEmployeeList = _db.Employees.ToList(); 
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee obj)
        {
           if(ModelState.IsValid)
           {
           _db.Employees.Add(obj);
           _db.SaveChanges();
           TempData["success"]="Employee created successfully";
            return RedirectToAction("Index");
           }
            return View(obj);
        }
        //GET
        public IActionResult Edit(int? id)
        {
           if(id==null || id ==0){
                return NotFound();
            }
            var employeeFromDb = _db.Employees.Find(id);
          
            if(employeeFromDb == null){
                return NotFound();
            }
            return View(employeeFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee obj)
        {
           if(ModelState.IsValid)
           {
           _db.Employees.Update(obj);
           _db.SaveChanges();
           TempData["success"]="Employee edited successfully";
            return RedirectToAction("Index");
           }
            return View(obj);
        }
        //GET
        public IActionResult Delete(int? id)
        {
           if(id==null || id ==0){
                return NotFound();
            }
            var employeeFromDb = _db.Employees.Find(id);
          
            if(employeeFromDb == null){
                return NotFound();
            }
            return View(employeeFromDb);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Employees.Find(id);
            if(obj == null){
                return NotFound();
            }
          
           _db.Employees.Remove(obj);
           _db.SaveChanges();
           TempData["success"]="Employee deleted successfully";
            return RedirectToAction("Index");
           
        }
    }
}