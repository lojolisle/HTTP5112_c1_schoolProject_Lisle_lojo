using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication_HTTP5112_SchoolProject.Models;
using System.Diagnostics;
using System.Web.Http.Cors;


namespace WebApplication_HTTP5112_SchoolProject.Controllers
{
    [EnableCors(origins: "*", methods: "*", headers: "*")]
    public class TeacherController : Controller
    {
        // GET: Teacher/List
        [HttpGet]
        [Route("Teacher/List")]

        public ActionResult List()
        {
            TeacherDataController Controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = Controller.ListTeachers();
            return View(Teachers);
        }

        // GET: Teacher/Show/{id}
        [HttpGet]
        [Route("Teacher/Show/{id}")]
        public ActionResult Show(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Teacher SelectedTeacher = Controller.FindTeacher(id);
            return View(SelectedTeacher);
        }

        //GET: Teacher/New
        [HttpGet]
        [Route("Teacher/New")]
        public ActionResult New()
        {
            return View();
        }

        //POST: Teacher/Create
        [HttpPost]
        [Route("Teacher/Create")]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public ActionResult Create(string TeacherfName, string TeacherlName, string empNo, string Salary, string hireDate)
        {
            Debug.WriteLine("---------see "+ TeacherfName + " Sal  " + Salary + "date "+ DateTime.Now.ToString("yyyy-m-d"));
            TeacherDataController Controller = new TeacherDataController();
            Teacher NewTeacher = new Teacher();
            NewTeacher.Teacherfname = TeacherfName;
            NewTeacher.Teacherlname = TeacherlName;
            NewTeacher.Employeenumber = empNo;
            NewTeacher.HireDate = hireDate;
            NewTeacher.Salary = Salary;

            Controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }


        //GET: Teacher/DeleteConfirm/{id}
        [HttpGet]
        [Route("Teacher/DeleteConfirm/{id}")]
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Teacher SelectedTeacher = Controller.FindTeacher(id);
            return View(SelectedTeacher);
        }

        //POST: Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

    }
}