using EmployeeSkillsManagement.Data;
using EmployeeSkillsManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSkillsManagement.Controllers
{
    public class SkillController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SkillController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Skill> objSkillList = _db.Skills.ToList();
            return View(objSkillList);
        }

        //GET
        public IActionResult Create()
        {
            // IEnumerable<Employee> objEmployeeList = _db.Employees.ToList(); 
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Skill obj)
        {
            if (ModelState.IsValid)
            {
                var data = _db.Skills.ToList();
                var check = data.Where(s => s.Skills.ToLower().Contains(obj.Skills.ToLower())).FirstOrDefault();
                if (check == null)
                {
                    _db.Skills.Add(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Skills created successfully";
                    return RedirectToAction("Index");
                }
                else
                {

                    TempData["warning"] = "Skill already exists";
                }
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var skillFromDb = _db.Skills.Find(id);

            if (skillFromDb == null)
            {
                return NotFound();
            }
            return View(skillFromDb);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {


            var obj = _db.Skills.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            var es = _db.EmployeeSkills.FirstOrDefault(es => es.SkillId == id);

            if (es != null)

            {

                TempData["warning"] = "Skill is assigned to employee";

                return Redirect(Request.Headers["Referer"].ToString());

            }
            _db.Skills.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Skills deleted successfully";
            return RedirectToAction("Index");

        }
    }

}