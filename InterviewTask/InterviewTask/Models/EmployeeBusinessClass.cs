using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace InterviewTask.Models
{
    public class EmployeeBusinessClass
    {
        private SqlConnection conn;
        private void connection()
        {
            string connectionstr = ConfigurationManager.ConnectionStrings["EmployeeConnectionString"].ToString();
            conn = new SqlConnection(connectionstr);
        }
        public bool AddEmployee(Employee employee)
        {
            connection();

            SqlCommand cmd = new SqlCommand("sp_AddEmployee", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Email", employee.Email);
            cmd.Parameters.AddWithValue("@Designation", employee.Designation);

            cmd.Parameters.AddWithValue("@FileName", employee.FileName.TrimStart(','));
            cmd.Parameters.AddWithValue("@FilePath", employee.FilePath);

            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i >= 1;
        }

        public List<Employee> ViewAllEmployees()
        {
            connection();

            SqlCommand cmd = new SqlCommand("sp_GetAllEmployees", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            conn.Open();
            da.Fill(dt);
            conn.Close();

            List<Employee> lstEmployeeData = new List<Employee>();
            if (dt != null || dt.Rows.Count > 1)
                lstEmployeeData = (from DataRow dr in dt.Rows
                                   select new Employee()
                                   {
                                       EmpID = Convert.ToInt32(dr["EmpID"]),
                                       Name = dr["Name"].ToString(),
                                       Email = dr["Email"].ToString(),
                                       Designation = dr["Designation"].ToString(),
                                       FileName = dr["FileName"].ToString(),
                                       FilePath = dr["FilePath"].ToString(),
                                       CreatedDate = Convert.ToDateTime(dr["CreatedDate"])
                                   }).ToList();

            return lstEmployeeData;
        }


        public bool UpdateEmployeeAndDetails(Employee employee)
        {
            connection();
            SqlCommand cmd = new SqlCommand("sp_EditEmployeeAndDetails", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpID", employee.EmpID);
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Email", employee.Email);
            cmd.Parameters.AddWithValue("@Designation", employee.Designation);
            cmd.Parameters.AddWithValue("@FileName", employee.FileName.TrimStart(','));
            cmd.Parameters.AddWithValue("@FilePath", employee.FilePath);
            cmd.Parameters.AddWithValue("@CreatedDate", employee.CreatedDate);


            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();


            return i >= 1;

        }

        public bool DeleteEmployee(int EmpId)
        {
            connection();
            SqlCommand cmd = new SqlCommand("sp_DeleteEmployeeAndDetails", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpID", EmpId);

            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();

            return i >= 1;
        }

        public Employee SaveFiles(ref Employee employee)
        {

            StringBuilder strFileNameBuilder = new StringBuilder();
            StringBuilder strFilePathBuilder = new StringBuilder();


            if (employee.Files != null)
            {
                foreach (HttpPostedFileBase file in employee.Files)
                {
                    if (file != null)
                    {

                        var InputFileName = Path.GetFileName(file.FileName);
                        var serverSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles/") + InputFileName);


                        EncryptAndDecrypt encryptAndDecrypt = new EncryptAndDecrypt();
                        if (File.Exists(serverSavePath))
                        {
                            List<string> lstpath = new List<string>();
                            lstpath.Add(serverSavePath);
                            deletefiles(lstpath);
                        }
                        encryptAndDecrypt.EncryptFile(serverSavePath, serverSavePath);
                        //file.SaveAs(serverSavePath);
                        strFileNameBuilder.Append("," + InputFileName);
                        strFilePathBuilder.Append("," + serverSavePath);

                    }
                }
                employee.FileName = strFileNameBuilder.ToString();
                employee.FilePath = strFilePathBuilder.ToString();
            }

            return employee;
        }




        public bool deletefiles(List<string> lstFileName)
        {
            int i = 0;
            FileInfo fileinfo;
            if (lstFileName != null)
            {
                foreach (string item in lstFileName)
                {
                    if (item != string.Empty)
                    {
                        fileinfo = new FileInfo(item);
                        fileinfo.Delete();
                        i = i++;
                    }
                }
            }

            return i > 0;
        }
        public List<string> SplitOnComma(string strSplitString)
        {
            List<string> lstSplitString = new List<string>();
            if (strSplitString != null)
                lstSplitString = strSplitString.Split(',').ToList();
            return lstSplitString;
        }

    }
}