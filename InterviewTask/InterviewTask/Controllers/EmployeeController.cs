using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using InterviewTask.Models;

namespace InterviewTask.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            EmployeeBusinessClass employeeBusinessClass = new EmployeeBusinessClass();
            ModelState.Clear();
            return View(employeeBusinessClass.ViewAllEmployees());
        }


        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeBusinessClass employeeBusinessClass = new EmployeeBusinessClass();
                    string error = string.Empty;

                    //HttpPostedFileBase filepath = employee.Files[0];
                    //string filename = Path.GetDirectoryName(filepath.FileName);
                    employee = employeeBusinessClass.SaveFiles(ref employee);



                    if (employeeBusinessClass.AddEmployee(employee))
                    {
                        ViewBag.Message = "Employee added successfully.";
                        ModelState.Clear();
                    }

                }
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
                return View();
            }
        }



        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            EmployeeBusinessClass employeeBusinessClass = new EmployeeBusinessClass();


            List<string> tblfiles = new List<string>();

            var employee = new Employee();
            employee = employeeBusinessClass.ViewAllEmployees().Find(emp => emp.EmpID == id);

            tblfiles = employeeBusinessClass.SplitOnComma(employee.FileName);

            employee.ListOfFiles = tblfiles;



            return View(employee);
        }



        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            try
            {
                // TODO: Add update logic here
                EmployeeBusinessClass employeeBusinessClass = new EmployeeBusinessClass();
                employee = employeeBusinessClass.SaveFiles(ref employee);
                employee.ListOfFiles = employeeBusinessClass.SplitOnComma(employee.FileName);
                if (employeeBusinessClass.UpdateEmployeeAndDetails(employee))
                {
                    ViewBag.Message = "Employee data updated successfully";
                    ModelState.Clear();
                }
                //return RedirectToAction("Index");
                return View(employee);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: Employee/Delete/5

        public ActionResult Delete(int EmpID)
        {
            try
            {
                EmployeeBusinessClass employeeBusinessClass = new EmployeeBusinessClass();
                // TODO: Add delete logic here
                Employee employee = new Employee();
                employee = employeeBusinessClass.ViewAllEmployees().Find(emp => emp.EmpID == EmpID);

                List<string> lstFileName = employeeBusinessClass.SplitOnComma(employee.FilePath);

                bool deleteFiles = employeeBusinessClass.deletefiles(lstFileName);

                if (employeeBusinessClass.DeleteEmployee(EmpID) && deleteFiles)
                {
                    ViewBag.Message = "Employee data deleted successfully";
                    ModelState.Clear();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        public ActionResult DownloadFiles(int EmpID)
        {
            List<string> ddlfiles = new List<string>();
            EmployeeBusinessClass employeeBusinessClass = new EmployeeBusinessClass();
            var employee = new Employee();
            employee = employeeBusinessClass.ViewAllEmployees().Find(emp => emp.EmpID == EmpID);

            ddlfiles = employeeBusinessClass.SplitOnComma(employee.FileName);

            employee.ListOfFiles = ddlfiles;
            return View(employee);
        }


        public ActionResult DownloadFile(string filename)
        {
            //EmployeeBusinessClass employeeBusinessClass = new EmployeeBusinessClass();
            //Build the File Path.
            string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/UploadedFiles/") + filename);

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.

            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);


        }
    }
}
