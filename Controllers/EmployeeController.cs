using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_CRUD_Application.Models;
using System.Data.Entity;

namespace MVC_CRUD_Application.Controllers
{
    public class EmployeeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
        //Method for populating the jquery data table using the namespace/class that has the task to get the data from the database
        public ActionResult GetData()
        {
            using (CrudDBEntities dataBase = new CrudDBEntities())
            {
                List<EmployeeDetail> employeeList = dataBase.EmployeeDetails.ToList<EmployeeDetail>();
                return Json(new { data = employeeList }, JsonRequestBehavior.AllowGet);
            }

        }
        //Insert GET method
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            //Adding a new employee
            if (id == 0)
            {
                return View(new EmployeeDetail());
            }
            //Editing an already existing employee
            else
            {
                using (CrudDBEntities dataBase = new CrudDBEntities())
                {
                    return View(dataBase.EmployeeDetails.Where(x => x.EmployeeID == id).FirstOrDefault<EmployeeDetail>());

                }
            }

        }

        //Insert POST method which updates the database depending on the add/edit function
        [HttpPost]
        public ActionResult AddOrEdit(EmployeeDetail emp)
        {
            using (CrudDBEntities dataBase = new CrudDBEntities())
            {
                //Adding a new employee
                if (emp.EmployeeID == 0)
                {
                    dataBase.EmployeeDetails.Add(emp);
                    dataBase.SaveChanges();
                    return Json(new { success = true, message = "Employee saved successfully" }, JsonRequestBehavior.AllowGet);
                }
                //Editing an already existing employee
                else
                {
                    dataBase.Entry(emp).State = EntityState.Modified;
                    dataBase.SaveChanges();
                    return Json(new { success = true, message = "Employee updated successfully" }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        //Delete POST method
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (CrudDBEntities db = new CrudDBEntities())
            {
                EmployeeDetail emp = db.EmployeeDetails.Where(x => x.EmployeeID == id).FirstOrDefault<EmployeeDetail>();
                db.EmployeeDetails.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Employee deleted successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}