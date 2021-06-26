using amt_test.common;
using amt_test.Dataset;
using amt_test.parameter;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;

namespace amt_test.Controller
{
    public class adminController : ApiController
    {
        [HttpPost]
        public JObject adminlogin(adminlogin al)
        {
            c_common cc = new c_common();
            c_admin ca = new c_admin();
            DataSet ds = new DataSet();
            try
            {
                ds = ca.adminlogin(al);
                if (ds.Tables.Count == 1)
                {
                    StringBuilder Sb = new StringBuilder();
                    Sb.Append("{\"" + "success\":\"" + ds.Tables[0].Rows[0]["success"].ToString() + "\",\"error\":\"" + ds.Tables[0].Rows[0]["ReturnMsg"].ToString() + "\"}");
                    JObject json = JObject.Parse((string)Sb.ToString());
                    return json;
                }
                else
                {

                    ds.Tables[1].TableName = "userdata";
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        HttpContext.Current.Session["userid"] = ds.Tables[1].Rows[0]["userid"].ToString();
                        HttpContext.Current.Session["fullname"] = ds.Tables[1].Rows[0]["empname"].ToString();
                        return cc.GetJSONString(ds);
                    }
                    else
                    {
                        StringBuilder Sb = new StringBuilder();
                        Sb.Append("{\"" + "success\":\"0\",\"error\":\"Sorry !! No data available.\"}");
                        JObject json = JObject.Parse((string)Sb.ToString());
                        return json;
                    }

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

        [HttpPost]
        public JObject updatefiledata()
        {
            c_common cc = new c_common();
            DataSet ds = new DataSet();
            StringBuilder Sb = new StringBuilder();
            try
            {
                NameValueCollection collection = HttpContext.Current.Request.Form;
                var items = collection.AllKeys.SelectMany(collection.GetValues, (k, v) => new { key = k, value = v });
                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                if (items.Count() > 0)
                {
                    if (HttpContext.Current.Request.Files.Count > 0)
                    {
                        string extension = System.IO.Path.GetExtension(HttpContext.Current.Request.Files[0].FileName);
                        BinaryReader br = new BinaryReader(HttpContext.Current.Request.Files[0].InputStream);
                        byte[] binaryReader = br.ReadBytes((int)HttpContext.Current.Request.Files[0].InputStream.Length);

                        string base64String = Convert.ToBase64String(binaryReader, 0, binaryReader.Length);
                        DataTable table1 = new DataTable();
                        table1.Columns.Add("success");
                        table1.Columns.Add("error");
                        table1.Rows.Add("1", "");
                        ds.Tables.Add(table1);
                        DataTable table2 = new DataTable();
                        table2.Columns.Add("filename");
                        table2.Columns.Add("filedata");
                        table2.Columns.Add("fileext");
                        table2.Rows.Add(HttpContext.Current.Request.Files[0].FileName, base64String, extension);
                        ds.Tables.Add(table2);
                        ds.Tables[1].TableName = "filedata";
                        return cc.GetJSONString(ds);
                    }
                    else
                    {
                        Sb = new StringBuilder();
                        Sb.Append("{\"" + "success\":\"0\",\"error\":\"Sorry!! No file present to upload.\"}");
                        JObject json = JObject.Parse((string)Sb.ToString());
                        return json;
                    }
                }
                else
                {
                    Sb = new StringBuilder();
                    Sb.Append("{\"" + "success\":\"0\",\"error\":\"Sorry!! No file present to upload.\"}");
                    JObject json = JObject.Parse((string)Sb.ToString());
                    return json;
                }

            }
            catch (Exception ex)
            {
                Sb = new StringBuilder();
                Sb.Append("{\"" + "success\":\"-1\",\"error\":\"" + ex.Message + "\"}");
                JObject json = JObject.Parse((string)Sb.ToString());
                return json;

            }
        }

        [HttpPost]
        public JObject uploadstudentphoto()
        {
            c_common cc = new c_common();
            DataSet ds = new DataSet();
            StringBuilder Sb = new StringBuilder();
            try
            {
                NameValueCollection collection = HttpContext.Current.Request.Form;
                var items = collection.AllKeys.SelectMany(collection.GetValues, (k, v) => new { key = k, value = v });
                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                if (items.Count() > 0)
                {
                    if (HttpContext.Current.Request.Files.Count > 0)
                    {
                        string extension = System.IO.Path.GetExtension(HttpContext.Current.Request.Files[0].FileName);
                        BinaryReader br = new BinaryReader(HttpContext.Current.Request.Files[0].InputStream);
                        byte[] binaryReader = br.ReadBytes((int)HttpContext.Current.Request.Files[0].InputStream.Length);
                        string base64String = Convert.ToBase64String(binaryReader, 0, binaryReader.Length);
                        DataTable table1 = new DataTable();
                        table1.Columns.Add("success");
                        table1.Columns.Add("error");
                        table1.Rows.Add("1", "");
                        ds.Tables.Add(table1);
                        DataTable table2 = new DataTable();
                        table2.Columns.Add("filename");
                        table2.Columns.Add("filedata");
                        table2.Columns.Add("fileext");
                        table2.Rows.Add(HttpContext.Current.Request.Files[0].FileName, base64String, extension);
                        ds.Tables.Add(table2);
                        ds.Tables[1].TableName = "filedata";
                        return cc.GetJSONString(ds);
                    }
                    else
                    {
                        Sb = new StringBuilder();
                        Sb.Append("{\"" + "success\":\"0\",\"error\":\"Sorry!! No file present to upload.\"}");
                        JObject json = JObject.Parse((string)Sb.ToString());
                        return json;
                    }
                }
                else
                {
                    Sb = new StringBuilder();
                    Sb.Append("{\"" + "success\":\"0\",\"error\":\"Sorry!! No file present to upload.\"}");
                    JObject json = JObject.Parse((string)Sb.ToString());
                    return json;
                }


            }
            catch (Exception ex)
            {
                Sb = new StringBuilder();
                Sb.Append("{\"" + "success\":\"-1\",\"error\":\"" + ex.Message + "\"}");
                JObject json = JObject.Parse((string)Sb.ToString());
                return json;

            }
        }
    }
}