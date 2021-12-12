using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication_HTTP5112_SchoolProject.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Web.Http.Cors;


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
        [Route("api/TeacherData/ListTeacher")]
        [EnableCors(origins: "*", methods: "*", headers: "*")]

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

        /// <summary>
        /// Adds a new Teacher data into the system
        /// This is also the API end point used by client server for adding new Teacher row in table Teachers
        /// API receives data data as a json object. Front end validations for teachers first and last name and employee number
        /// </summary>
        /// 
        /// <param name="NewTeacher">Teacher Object</param>
        /// 
        /// <example>
        /// 
        /// </example>
        [HttpPost]
        [Route("api/TeacherData/AddTeacher")]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            MySqlConnection Conn = School.AccessDatabase();
            //Open the connection between the web server and database
            Conn.Open();

            if (ModelState.IsValid)
            {
                Debug.WriteLine("Teacher first name: " + NewTeacher.Teacherfname + " Sal  " + NewTeacher.Salary + "Hdate " + NewTeacher.HireDate);

                //SQL QUERY
                string query = "insert into teachers(TeacherfName, TeacherlName, Employeenumber, HireDate, Salary)  values (@fname, @lname, @empNo, @hireDate, @salary)";

                //Establish a new command (query) for our database
                MySqlCommand cmd = Conn.CreateCommand();
                cmd.CommandText = query;
                if (!(string.IsNullOrEmpty(NewTeacher.Teacherfname)) && !(string.IsNullOrEmpty(NewTeacher.Teacherlname)))
                {

                    cmd.Parameters.AddWithValue("@fname", NewTeacher.Teacherfname);
                    cmd.Parameters.AddWithValue("@lname", NewTeacher.Teacherlname);
                    cmd.Parameters.AddWithValue("@empNo", NewTeacher.Employeenumber);

                    // validate when empty date received. Use current date
                    if (string.IsNullOrEmpty(NewTeacher.HireDate))
                    {
                        NewTeacher.HireDate = DateTime.Now.ToString("yyyy-M-d H:m:ss");
                    }

                    // validate for empty salary and add  zero if empty
                    if (string.IsNullOrEmpty(NewTeacher.Salary))
                    {
                        NewTeacher.Salary = "0";
                    }

                    cmd.Parameters.AddWithValue("@hireDate", Convert.ToDateTime(NewTeacher.HireDate));
                    cmd.Parameters.AddWithValue("@salary", Decimal.Parse(NewTeacher.Salary));

                    cmd.ExecuteNonQuery();
                }

                // Unable to return the last inserted ID

                Debug.WriteLine(" check ------------------------ id " + NewTeacher.TeacherId);
            } 

            Conn.Close();
        }



        /// <summary>
        /// Deletes a teacher by given id
        /// </summary>
        /// <param name="id">is the primary key of the Teacher</param>
        [HttpGet]
        [Route("api/TeacherData/DeleteTeacher/{id}")]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void DeleteTeacher(int id )
        {
            MySqlConnection Conn = School.AccessDatabase();
            //Open the connection between the web server and database
            Conn.Open();

            //SQL QUERY
            string query = "delete from teachers where TeacherId=@id";
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            Conn.Close();
        }
    }
}
