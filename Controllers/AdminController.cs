using EmployeeSkillsManagement.Data;
using EmployeeSkillsManagement.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace EmployeeSkillsManagement.Controllers
 {
    public class AdminController : Controller
    {
        private  ApplicationDbContext _db ;
         public AdminController(ApplicationDbContext db)
       {
        _db = db;
       }
        //GET
         public IActionResult Login()
        {
          return View();
        }
    [HttpPost]
     public IActionResult Login(Admin model)
    {
        if(ModelState.IsValid){

            //  _db.Admins.Add(model);

            //   _db.SaveChanges();
            var data = _db.Admins.Where(m=>m.EMAIL==model.EMAIL).SingleOrDefault();
            if(data!= null){
               bool IsValid = (data.EMAIL == model.EMAIL && data.PASSWORD == model.PASSWORD);
               
                if(IsValid){
                    var identity =new ClaimsIdentity(new[] {new Claim(ClaimTypes.Name, model.EMAIL)},
                    CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    HttpContext.Session.SetString("EMAIL", model.EMAIL);
                    TempData["success"]="Login successfully";
                    return RedirectToAction("Index", "Home");

                }
                else{
                    TempData["errorPassword"] = "Invalid Password!";
                    return View(model);
                }
            }
           else{
                    TempData["errorMessage"] = "Invalid UserName!";
                    return View(model);
                }
         
       }
       else {
          return View(model);
       }
       
    }
     
     public IActionResult LogOut()
     {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        var storedCookies = Request.Cookies.Keys;
        foreach(var cookies in storedCookies)
        {
            Response.Cookies.Delete(cookies);
        }
        TempData["success"]="Logout successfully";
        return RedirectToAction("Login", "Admin");
     }
   
 }
}