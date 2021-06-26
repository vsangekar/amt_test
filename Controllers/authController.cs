using amt_test.common;
using amt_test.Dataset;
using amt_test.parameter;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;

namespace amt_test.Controllers
{
    public class authController : ApiController
    {
        public JObject insertupdatestudentprofile(studentmaster sdata)
        {
            c_common cc = new c_common();
            c_master ca = new c_master();
            DataSet ds = new DataSet();
            try
            {
                DataSet dscheck = ca.checkforrecordexists(sdata.studemailid, sdata.studmobileno, 0, "cust");
                if (dscheck.Tables[0].Rows[0][0].ToString() == "1")
                {
                    studentmaster sm = new studentmaster();
                    ds = ca.insertupdatestudentprofile(sm);
                    if (ds.Tables.Count > 0)
                    {
                        StringBuilder Sb = new StringBuilder();
                        Sb.Append("{\"" + "success\":\"" + ds.Tables[0].Rows[0]["success"].ToString() + "\",\"error\":\"" + ds.Tables[0].Rows[0]["ReturnMsg"].ToString() + "\"}");
                        JObject json = JObject.Parse((string)Sb.ToString());
                        return json;
                    }
                    else
                    {
                        StringBuilder Sb = new StringBuilder();
                        Sb.Append("{\"" + "success\":\"" + dscheck.Tables[0].Rows[0][0].ToString() + "\",\"error\":\"" + dscheck.Tables[0].Rows[0][1].ToString() + "\"}");
                        JObject json = JObject.Parse((string)Sb.ToString());
                        return json;
                    }
                }
                else
                {
                    StringBuilder Sb = new StringBuilder();
                    Sb.Append("{\"" + "success\":\"" + dscheck.Tables[0].Rows[0][0].ToString() + "\",\"error\":\"" + dscheck.Tables[0].Rows[0][1].ToString() + "\"}");
                    JObject json = JObject.Parse((string)Sb.ToString());
                    return json;
                }

            }
            catch (Exception ex)
            {
                StringBuilder Sb = new StringBuilder();
                Sb.Append("{\"" + "success\":\"-1\",\"error\":\"" + ex.Message + "\"}");
                JObject json = JObject.Parse((string)Sb.ToString());
                return json;

            }
        }

    }
}