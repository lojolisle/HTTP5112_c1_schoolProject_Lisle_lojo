using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MySql.Data.MySqlClient;

namespace WebApplication_HTTP5112_SchoolProject.Models
{
    public class SchoolDbContext
    {

        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "school_f2021"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3307"; } }

        protected static string ConnectionString
        {
            get
            {
                //convert zero datetime is a db connection setting which returns NULL if the date is 0000-00-00
                //this can allow C# to have an easier interpretation of the date (no date instead of 0 BCE)

                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password
                    + "; convert zero datetime = True";
            }
        }
        //method used to get the database
        /// <summary>
        /// Returns a connection to the teacher database.
        /// </summary>
        /// <example>
        /// private TeacherDbContext Teacher = new TeacherDbContext();
        /// MySqlConnection Conn = Teacher.AccessDatabase();
        /// </example>
        /// <returns>A MySqlConnection Object</returns>
        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }
    }


}