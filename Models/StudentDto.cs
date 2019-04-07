using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RegistrationForm.Models
{
    public class StudentDto
    {
        private SqlConnection con;
        private void Connection()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["StudentCon"].ConnectionString);
        }
        public List<StudentModel> GetStudent()
        {
            List<StudentModel> studentList = new List<StudentModel>();
            Connection();
            SqlCommand cmd = new SqlCommand("GetStudentData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            sd.Fill(dt);
            con.Close();
            foreach(DataRow dr in dt.Rows)
            {
                studentList.Add(
                    new StudentModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        Country = Convert.ToString(dr["Country"]),
                        Gender = Convert.ToString(dr["Gender"]),
                        Status = Convert.ToInt32(dr["Status"]) == 1 ? true : false
                    });
            }
            return studentList;
        }
        
        public bool AddStudent(StudentModel student)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("AddStudentData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", student.Name);
            cmd.Parameters.AddWithValue("@Country", student.Country);
            cmd.Parameters.AddWithValue("@Gender", student.Gender);
            cmd.Parameters.AddWithValue("@Status", student.Status ? 1 : 0);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }

        public bool UpdateStudent(StudentModel student)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("UpdateStudentData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", student.Id);
            cmd.Parameters.AddWithValue("@Name", student.Name);
            cmd.Parameters.AddWithValue("@Country", student.Country);
            cmd.Parameters.AddWithValue("@Gender", student.Gender);
            cmd.Parameters.AddWithValue("@Status", student.Status ? 1 : 0);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }

        public bool DeleteStudent(int id)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("DeleteStudentData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }

        public List<CountryModel> GetCountry()
        {
            Connection();
            List<CountryModel> countryList = new List<CountryModel>();
            SqlCommand cmd = new SqlCommand("GetCountryDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            sd.Fill(dt);
            con.Close();
            foreach(DataRow dr in dt.Rows)
            {
                countryList.Add(new CountryModel
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = Convert.ToString(dr["Name"])
                });
            }
            return countryList;
        }
    }
}