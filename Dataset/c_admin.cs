using amt_test.parameter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace amt_test.Dataset
{
    public class c_admin
    {
        string connstr = "";


        public c_admin()
        {
            connstr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        }

        public DataSet adminlogin(adminlogin al)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand sqlComm = new SqlCommand("pr_nm_adminlogin", conn);
                sqlComm.Parameters.AddWithValue("@adminemail", al.emailid);
                sqlComm.Parameters.AddWithValue("@adminpassword", al.loginpassword);
                sqlComm.CommandTimeout = 0;
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
            }
            return ds;
        }
    }
}