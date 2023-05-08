using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EmployeeSkillsManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using EmployeeSkillsManagement.Data;
using ClosedXML.Excel;
using System.Data;
using System.Reflection;
using System.IO;
using EmployeeSkillsManagement.Models;

namespace EmployeeSkillsManagement.Controllers
{
    //  [Authorize]
    public class HomeController : Controller
    {
        // private readonly ILogger<HomeController> _logger;

        // public HomeController(ILogger<HomeController> logger)
        // {
        //     _logger = logger;
        // }
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        // public IActionResult Index()
        // {
        //    IEnumerable<Employee> objEmployeeList = _db.Employees.ToList(); 
        //         return View(objEmployeeList);
        // }
        static List<Employee> print;

        public IActionResult Index(string name, string skillId, string Level)
        {
            HomeIndex hi;
            hi = new HomeIndex();
            hi.skills = _db.Skills.ToList().OrderBy(s => s.Skills).ToList();
            hi.employees = _db.Employees.ToList();

            var temp = hi.employees;
            if (name != null)

            {

                temp = temp.Where(t => (t.FirstName.ToLower().Contains(name.ToLower()) || t.LastName.ToLower().Contains(name.ToLower()))).ToList();

                hi.employees = temp;

            }

            if (skillId != null || Level != null)

            {

                var employeeSkill = _db.EmployeeSkills.ToList();

                if (skillId != null)

                {

                    employeeSkill = employeeSkill.Where(s => s.SkillId.ToString() == skillId).ToList();

                }

                if (Level != null)
                {

                    employeeSkill = employeeSkill.Where(s => s.Expert_Level.ToLower() == Level.ToLower()).ToList();
                }

                List<Employee> e = new List<Employee>();

                foreach (var ek in employeeSkill)

                {

                    var entry = temp.Where(q => q.Id == ek.EmployeeId).FirstOrDefault();

                    if (entry != null && !e.Contains(entry))
                    {

                        e.Add(entry);

                    }

                }

                hi.employees = e;

            }

            print = hi.employees;

            return View(hi);

        }

        public IActionResult ExportExcel()
        {

            try
            {
                var data = print;
                if (data != null & data.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(ToConvertDataTable(data.ToList()));
                        using (MemoryStream strem = new MemoryStream())
                        {
                            wb.SaveAs(strem);
                            string fileName = $"EmployeeList_{DateTime.Now.ToString("dd/MM/yyyy")}.xlsx";
                            return File(strem.ToArray(), "application/vnd.openxmlformats-officedocuments.spreadsheetml.sheet", fileName);
                        }
                    }
                }
                TempData["Error"] = "Data Not Found!";
            }

            catch (Exception ex)
            {

            }
            return RedirectToAction("Index");
        }

        public DataTable ToConvertDataTable<T>(List<T> items)
        {
            DataTable dt = new DataTable(typeof(T).Name);
            PropertyInfo[] propInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in propInfo)
            {
                dt.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[propInfo.Length];
                for (int i = 0; i < propInfo.Length; i++)
                {
                    values[i] = propInfo[i].GetValue(item, null);

                }
                dt.Rows.Add(values);
            }
            return dt;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
