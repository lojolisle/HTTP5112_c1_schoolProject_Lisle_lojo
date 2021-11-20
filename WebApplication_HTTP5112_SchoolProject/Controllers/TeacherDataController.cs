using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication_HTTP5112_SchoolProject.Models;
using MySql.Data.MySqlClient;

namespace WebApplication_HTTP5112_SchoolProject.Controllers
{
    public class TeacherDataController : ApiController
    {

        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// GET method to return a list of teachers
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        public List<Teacher> ListTeachers()
        {
            //Creates an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int Id = (int)ResultSet["teacherId"];
                string TeacherfName = ResultSet["teacherfname"].ToString();
                string TeacherlName = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                string HireDate = ResultSet["hiredate"].ToString();
                string Salary = ResultSet["salary"].ToString();

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = Id;
                NewTeacher.Teacherfname = TeacherfName;
                NewTeacher.Teacherlname = TeacherlName;
                NewTeacher.Employeenumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                //add Teacher object to List
                Teachers.Add(NewTeacher);

            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Teachers
            return Teachers;
        }

        /// <summary>
        /// Get method to fetch a teachers details from id passed
        /// </summary>
        /// <param name="TeacherId">The database Id of the teacher</param>
        /// <returns></returns>

        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{TeacherId}")]
        public Teacher FindTeacher(int TeacherId)
        {
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where teacherid=" + TeacherId;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers
            Teacher SelectedTeacher = new Teacher();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int Id = (int)ResultSet["teacherId"];
                string TeacherfName = ResultSet["teacherfname"].ToString();
                string TeacherlName = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                string HireDate = ResultSet["hiredate"].ToString();
                string Salary = ResultSet["salary"].ToString();

        
                SelectedTeacher.TeacherId = Id;
                SelectedTeacher.Teacherfname = TeacherfName;
                SelectedTeacher.Teacherlname = TeacherlName;
                SelectedTeacher.Employeenumber = EmployeeNumber;
                SelectedTeacher.HireDate = HireDate;
                SelectedTeacher.Salary = Salary;
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the a Teacher details from given id
            return SelectedTeacher;

        }
    }
}
