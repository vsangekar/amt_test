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
    public class c_master
    {
        string connstr = "";
        public c_master()
        {
            connstr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        }
        public void insertupdatestudent(studentmaster sm, ref int docid)
        {
            try
            {
                if (sm.studid==null)
                {
                    nc.AccessComponent.Insert(sm, connstr, ref docid);
                }
                else
                {
                    string condition = " studid=" + sm.studid;
                    nc.AccessComponent.Update(sm, condition, connstr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet checkforrecordexists(string mobileno, string email, int refid, string reftype)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {

                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand sqlComm = new SqlCommand("pr_t_checkforrecordexists", conn);
                sqlComm.Parameters.AddWithValue("mobileno", mobileno);
                sqlComm.Parameters.AddWithValue("emailid", email);
                sqlComm.Parameters.AddWithValue("refid", refid);
                sqlComm.Parameters.AddWithValue("reftype", reftype);
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