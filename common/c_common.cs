using nc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace amt_test.common
{
    public class c_common
    {

        string connstr = "";


        public c_common()
        {
            connstr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        }

        public JObject GetJSONString(DataSet ds)
        {
            DataTable Dt = new DataTable();
            StringBuilder Sb = new StringBuilder();
            int rowcount = 0;
            int success = 0;
            if (ds.Tables.Count > 0)
            {
                for (int j = 0; j < ds.Tables.Count; j++)
                {
                    if (j > 0)
                    {
                        if (ds.Tables[j].Rows.Count > 0)
                        {
                            rowcount++;
                        }
                    }
                }

                if (rowcount >= 0)
                {
                    success = 1;
                }
                if (success == 1)
                {
                    for (int k = 0; k < ds.Tables.Count; k++)
                    {
                        if (k == 0)
                        {
                            Sb.Append("{\"" + "success\":\"" + ds.Tables[0].Rows[0][0].ToString() + "\",\"error\":\"" + ds.Tables[0].Rows[0][1].ToString() + "\",");
                        }
                        if (k > 0)
                        {
                            Dt = new DataTable();
                            Dt = ds.Tables[k];
                            if (Dt.Rows.Count > 0)
                            {
                                string[] StrDc = new string[Dt.Columns.Count];
                                string HeadStr = string.Empty;

                                for (int i = 0; i < Dt.Columns.Count; i++)
                                {
                                    StrDc[i] = Dt.Columns[i].Caption.ToLower();
                                    HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";
                                }

                                HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);

                                Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [");

                                for (int i = 0; i < Dt.Rows.Count; i++)
                                {
                                    string TempStr = HeadStr;
                                    Sb.Append("{");
                                    for (int j = 0; j < Dt.Columns.Count; j++)
                                    {
                                        TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower() + "¾", Dt.Rows[i][j].ToString().Replace(@"\", "\\\\").Replace("\"", "\\\"").Replace("\'", "\\\'"));
                                    }
                                    Sb.Append(TempStr + "},");
                                }

                                Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));
                                if (k == ds.Tables.Count - 1)
                                {
                                    Sb.Append("]");
                                }
                                else
                                {
                                    Sb.Append("],");
                                }
                            }
                            else
                            {
                                if (k == ds.Tables.Count - 1)
                                {
                                    Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [ ]");
                                }
                                else
                                {
                                    Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [ ],");
                                }
                            }

                        }
                    }
                    Sb.Append("}");
                }
                else
                {
                    Sb.Append("{\"" + "success\":\"0\",\"error\":\"No data available.\"}");
                }
            }
            else
            {
                Sb.Append("{\"" + "success\":\"0\",\"error\":\"No data available.\"}");
            }
            JObject json = JObject.Parse((string)Sb.ToString());
            return json;
        }



        public JObject getjsonstringinstitutiondata(DataSet ds)
        {
            c_common cobj = new c_common();
            DataTable Dt = new DataTable();
            StringBuilder Sb = new StringBuilder();
            int rowcount = 0;
            int success = 0;
            if (ds.Tables.Count > 0)
            {
                for (int j = 0; j < ds.Tables.Count; j++)
                {
                    if (j > 0)
                    {
                        if (ds.Tables[j].Rows.Count > 0)
                        {
                            rowcount++;
                        }
                    }
                }

                if (rowcount > 0)
                {
                    success = 1;
                }
                if (success == 1)
                {
                    for (int k = 0; k < ds.Tables.Count; k++)
                    {
                        if (k == 0)
                        {
                            Sb.Append("{\"" + "success\":\"" + ds.Tables[0].Rows[0][0].ToString() + "\",\"error\":\"" + ds.Tables[0].Rows[0][1].ToString() + "\",");
                        }
                        if (k == 1)
                        {
                            Dt = new DataTable();
                            Dt = ds.Tables[k];
                            if (Dt.Rows.Count > 0)
                            {
                                string[] StrDc = new string[Dt.Columns.Count];
                                string HeadStr = string.Empty;

                                for (int i = 0; i < Dt.Columns.Count; i++)
                                {
                                    StrDc[i] = Dt.Columns[i].Caption.ToLower();
                                    if (i != Dt.Columns.Count - 1)
                                        HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";
                                    else
                                        HeadStr += "\"" + StrDc[i] + "\" : " + StrDc[i] + i.ToString() + ",";

                                }

                                HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);
                                Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [");

                                for (int i = 0; i < Dt.Rows.Count; i++)
                                {
                                    string TempStr = HeadStr;
                                    Sb.Append("{");
                                    for (int j = 0; j < Dt.Columns.Count; j++)
                                    {
                                        if (j != Dt.Columns.Count - 1)
                                        {
                                            TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower() + "¾", Dt.Rows[i][j].ToString().Replace(@"\", "\\\\").Replace("\"", "\\\"").Replace("\'", "\\\'"));
                                        }
                                        else
                                        {
                                            DataTable dtTable = ds.Tables[2];
                                            DataRow[] drs = dtTable.Select("instid = " + Dt.Rows[i]["instid"].ToString());
                                            StringBuilder str = cobj.showrowtable(dtTable, drs);
                                            //TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower(), str.ToString().Replace(@"\", "\\\\").Replace("\"", "\\\"").Replace("\'", "\\\'"));
                                            TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower(), "[");
                                            if (str.Length > 0)
                                            {
                                                TempStr = TempStr + str.ToString();
                                            }
                                            TempStr = TempStr + "]";

                                        }

                                    }
                                    Sb.Append(TempStr + "},");
                                }

                                Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));
                                DataTable dtDelete = ds.Tables[2];
                                ds.Tables.Remove(dtDelete);
                                if (k == ds.Tables.Count - 1)
                                {
                                    Sb.Append("]");
                                }
                                else
                                {
                                    Sb.Append("],");
                                }
                            }
                            else
                            {
                                if (k == ds.Tables.Count - 1)
                                {
                                    Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [ ]");
                                }
                                else
                                {
                                    Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [ ],");
                                }
                            }
                        }
                    }
                    Sb.Append("}");
                }
                else
                {
                    Sb.Append("{\"" + "success\":\"0\",\"error\":\"No data available.\"}");
                }
            }
            else
            {
                Sb.Append("{\"" + "success\":\"0\",\"error\":\"No data available.\"}");
            }
            JObject json = JObject.Parse((string)Sb.ToString());
            return json;
        }



        public JObject getjsonstringstudentcourselist(DataSet ds)
        {
            c_common cobj = new c_common();
            DataTable Dt = new DataTable();
            StringBuilder Sb = new StringBuilder();
            int rowcount = 0;
            int success = 0;
            if (ds.Tables.Count > 0)
            {
                for (int j = 0; j < ds.Tables.Count; j++)
                {
                    if (j > 0)
                    {
                        if (ds.Tables[j].Rows.Count > 0)
                        {
                            rowcount++;
                        }
                    }
                }

                if (rowcount > 0)
                {
                    success = 1;
                }
                if (success == 1)
                {
                    for (int k = 0; k < ds.Tables.Count; k++)
                    {
                        if (k == 0)
                        {
                            Sb.Append("{\"" + "success\":\"" + ds.Tables[0].Rows[0][0].ToString() + "\",\"error\":\"" + ds.Tables[0].Rows[0][1].ToString() + "\",");
                        }
                        if (k == 1)
                        {
                            Dt = new DataTable();
                            Dt = ds.Tables[k];
                            if (Dt.Rows.Count > 0)
                            {
                                string[] StrDc = new string[Dt.Columns.Count];
                                string HeadStr = string.Empty;

                                for (int i = 0; i < Dt.Columns.Count; i++)
                                {
                                    StrDc[i] = Dt.Columns[i].Caption.ToLower();
                                    if (i != Dt.Columns.Count - 1)
                                        HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";
                                    else
                                        HeadStr += "\"" + StrDc[i] + "\" : " + StrDc[i] + i.ToString() + ",";

                                }

                                HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);
                                Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [");

                                for (int i = 0; i < Dt.Rows.Count; i++)
                                {
                                    string TempStr = HeadStr;
                                    Sb.Append("{");
                                    for (int j = 0; j < Dt.Columns.Count; j++)
                                    {
                                        if (j != Dt.Columns.Count - 1)
                                        {
                                            TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower() + "¾", Dt.Rows[i][j].ToString().Replace(@"\", "\\\\").Replace("\"", "\\\"").Replace("\'", "\\\'"));
                                        }
                                        else
                                        {
                                            DataTable dtTable = ds.Tables[2];
                                            DataRow[] drs = dtTable.Select("studid = " + Dt.Rows[i]["studid"].ToString());
                                            StringBuilder str = cobj.showrowtable(dtTable, drs);
                                            //TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower(), str.ToString().Replace(@"\", "\\\\").Replace("\"", "\\\"").Replace("\'", "\\\'"));
                                            TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower(), "[");
                                            if (str.Length > 0)
                                            {
                                                TempStr = TempStr + str.ToString();
                                            }
                                            TempStr = TempStr + "]";

                                        }

                                    }
                                    Sb.Append(TempStr + "},");
                                }

                                Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));
                                DataTable dtDelete = ds.Tables[2];
                                ds.Tables.Remove(dtDelete);
                                if (k == ds.Tables.Count - 1)
                                {
                                    Sb.Append("]");
                                }
                                else
                                {
                                    Sb.Append("],");
                                }
                            }
                            else
                            {
                                if (k == ds.Tables.Count - 1)
                                {
                                    Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [ ]");
                                }
                                else
                                {
                                    Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [ ],");
                                }
                            }
                        }
                    }
                    Sb.Append("}");
                }
                else
                {
                    Sb.Append("{\"" + "success\":\"0\",\"error\":\"No data available.\"}");
                }
            }
            else
            {
                Sb.Append("{\"" + "success\":\"0\",\"error\":\"No data available.\"}");
            }
            JObject json = JObject.Parse((string)Sb.ToString());
            return json;
        }



        public JObject getjsonstringstudentcourselistforcoach(DataSet ds)
        {
            c_common cobj = new c_common();
            DataTable Dt = new DataTable();
            StringBuilder Sb = new StringBuilder();
            int rowcount = 0;
            int success = 0;
            if (ds.Tables.Count > 0)
            {
                for (int j = 0; j < ds.Tables.Count; j++)
                {
                    if (j > 0)
                    {
                        if (ds.Tables[j].Rows.Count > 0)
                        {
                            rowcount++;
                        }
                    }
                }

                if (rowcount > 0)
                {
                    success = 1;
                }
                if (success == 1)
                {
                    for (int k = 0; k < ds.Tables.Count; k++)
                    {
                        if (k == 0)
                        {
                            Sb.Append("{\"" + "success\":\"" + ds.Tables[0].Rows[0][0].ToString() + "\",\"error\":\"" + ds.Tables[0].Rows[0][1].ToString() + "\",");
                        }
                        if (k == 1)
                        {
                            Dt = new DataTable();
                            Dt = ds.Tables[k];
                            if (Dt.Rows.Count > 0)
                            {
                                string[] StrDc = new string[Dt.Columns.Count];
                                string HeadStr = string.Empty;

                                for (int i = 0; i < Dt.Columns.Count; i++)
                                {
                                    StrDc[i] = Dt.Columns[i].Caption.ToLower();
                                    if (i != Dt.Columns.Count - 1)
                                        HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";
                                    else
                                        HeadStr += "\"" + StrDc[i] + "\" : " + StrDc[i] + i.ToString() + ",";

                                }

                                HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);
                                Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [");

                                for (int i = 0; i < Dt.Rows.Count; i++)
                                {
                                    string TempStr = HeadStr;
                                    Sb.Append("{");
                                    for (int j = 0; j < Dt.Columns.Count; j++)
                                    {
                                        if (j != Dt.Columns.Count - 1)
                                        {
                                            TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower() + "¾", Dt.Rows[i][j].ToString().Replace(@"\", "\\\\").Replace("\"", "\\\"").Replace("\'", "\\\'"));
                                        }
                                        else
                                        {
                                            DataTable dtTable = ds.Tables[2];
                                            DataRow[] drs = dtTable.Select("courseid = " + Dt.Rows[i]["courseid"].ToString());
                                            StringBuilder str = cobj.showrowtable(dtTable, drs);
                                            //TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower(), str.ToString().Replace(@"\", "\\\\").Replace("\"", "\\\"").Replace("\'", "\\\'"));
                                            TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower(), "[");
                                            if (str.Length > 0)
                                            {
                                                TempStr = TempStr + str.ToString();
                                            }
                                            TempStr = TempStr + "]";

                                        }

                                    }
                                    Sb.Append(TempStr + "},");
                                }

                                Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));
                                DataTable dtDelete = ds.Tables[2];
                                ds.Tables.Remove(dtDelete);
                                if (k == ds.Tables.Count - 1)
                                {
                                    Sb.Append("]");
                                }
                                else
                                {
                                    Sb.Append("],");
                                }
                            }
                            else
                            {
                                if (k == ds.Tables.Count - 1)
                                {
                                    Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [ ]");
                                }
                                else
                                {
                                    Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [ ],");
                                }
                            }
                        }
                    }
                    Sb.Append("}");
                }
                else
                {
                    Sb.Append("{\"" + "success\":\"0\",\"error\":\"No data available.\"}");
                }
            }
            else
            {
                Sb.Append("{\"" + "success\":\"0\",\"error\":\"No data available.\"}");
            }
            JObject json = JObject.Parse((string)Sb.ToString());
            return json;
        }


        public JObject getjsonstringforduefee(DataSet ds)
        {
            c_common cobj = new c_common();
            DataTable Dt = new DataTable();
            StringBuilder Sb = new StringBuilder();
            int rowcount = 0;
            int success = 0;
            if (ds.Tables.Count > 0)
            {
                for (int j = 0; j < ds.Tables.Count; j++)
                {
                    if (j > 0)
                    {
                        if (ds.Tables[j].Rows.Count > 0)
                        {
                            rowcount++;
                        }
                    }
                }

                if (rowcount > 0)
                {
                    success = 1;
                }
                if (success == 1)
                {
                    for (int k = 0; k < ds.Tables.Count; k++)
                    {
                        if (k == 0)
                        {
                            Sb.Append("{\"" + "success\":\"" + ds.Tables[0].Rows[0][0].ToString() + "\",\"error\":\"" + ds.Tables[0].Rows[0][1].ToString() + "\",");
                        }
                        if (k == 1)
                        {
                            Dt = new DataTable();
                            Dt = ds.Tables[k];
                            if (Dt.Rows.Count > 0)
                            {
                                string[] StrDc = new string[Dt.Columns.Count];
                                string HeadStr = string.Empty;

                                for (int i = 0; i < Dt.Columns.Count; i++)
                                {
                                    StrDc[i] = Dt.Columns[i].Caption.ToLower();
                                    if (i != Dt.Columns.Count - 1)
                                        HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";
                                    else
                                        HeadStr += "\"" + StrDc[i] + "\" : " + StrDc[i] + i.ToString() + ",";

                                }

                                HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);
                                Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [");

                                for (int i = 0; i < Dt.Rows.Count; i++)
                                {
                                    string TempStr = HeadStr;
                                    Sb.Append("{");
                                    for (int j = 0; j < Dt.Columns.Count; j++)
                                    {
                                        if (j != Dt.Columns.Count - 1)
                                        {
                                            TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower() + "¾", Dt.Rows[i][j].ToString().Replace(@"\", "\\\\").Replace("\"", "\\\"").Replace("\'", "\\\'"));
                                        }
                                        else
                                        {
                                            DataTable dtTable = ds.Tables[2];
                                            DataRow[] drs = dtTable.Select("courseid = " + Dt.Rows[i]["courseid"].ToString() + " and studid=" + Dt.Rows[i]["studid"].ToString());
                                            StringBuilder str = cobj.showrowtable(dtTable, drs);
                                            //TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower(), str.ToString().Replace(@"\", "\\\\").Replace("\"", "\\\"").Replace("\'", "\\\'"));
                                            TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower(), "[");
                                            if (str.Length > 0)
                                            {
                                                TempStr = TempStr + str.ToString();
                                            }
                                            TempStr = TempStr + "]";

                                        }

                                    }
                                    Sb.Append(TempStr + "},");
                                }

                                Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));
                                DataTable dtDelete = ds.Tables[2];
                                ds.Tables.Remove(dtDelete);
                                if (k == ds.Tables.Count - 1)
                                {
                                    Sb.Append("]");
                                }
                                else
                                {
                                    Sb.Append("],");
                                }
                            }
                            else
                            {
                                if (k == ds.Tables.Count - 1)
                                {
                                    Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [ ]");
                                }
                                else
                                {
                                    Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [ ],");
                                }
                            }
                        }
                    }
                    Sb.Append("}");
                }
                else
                {
                    Sb.Append("{\"" + "success\":\"0\",\"error\":\"No data available.\"}");
                }
            }
            else
            {
                Sb.Append("{\"" + "success\":\"0\",\"error\":\"No data available.\"}");
            }
            JObject json = JObject.Parse((string)Sb.ToString());
            return json;
        }



        public JObject getjsonstringforcampstudentlist(DataSet ds)
        {
            c_common cobj = new c_common();
            DataTable Dt = new DataTable();
            StringBuilder Sb = new StringBuilder();
            int rowcount = 0;
            int success = 0;
            if (ds.Tables.Count > 0)
            {
                for (int j = 0; j < ds.Tables.Count; j++)
                {
                    if (j > 0)
                    {
                        if (ds.Tables[j].Rows.Count > 0)
                        {
                            rowcount++;
                        }
                    }
                }

                if (rowcount > 0)
                {
                    success = 1;
                }
                if (success == 1)
                {
                    for (int k = 0; k < ds.Tables.Count; k++)
                    {
                        if (k == 0)
                        {
                            Sb.Append("{\"" + "success\":\"" + ds.Tables[0].Rows[0][0].ToString() + "\",\"error\":\"" + ds.Tables[0].Rows[0][1].ToString() + "\",");
                        }
                        if (k == 1)
                        {
                            Dt = new DataTable();
                            Dt = ds.Tables[k];
                            if (Dt.Rows.Count > 0)
                            {
                                string[] StrDc = new string[Dt.Columns.Count];
                                string HeadStr = string.Empty;

                                for (int i = 0; i < Dt.Columns.Count; i++)
                                {
                                    StrDc[i] = Dt.Columns[i].Caption.ToLower();
                                    if (i != Dt.Columns.Count - 1)
                                        HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";
                                    else
                                        HeadStr += "\"" + StrDc[i] + "\" : " + StrDc[i] + i.ToString() + ",";

                                }

                                HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);
                                Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [");

                                for (int i = 0; i < Dt.Rows.Count; i++)
                                {
                                    string TempStr = HeadStr;
                                    Sb.Append("{");
                                    for (int j = 0; j < Dt.Columns.Count; j++)
                                    {
                                        if (j != Dt.Columns.Count - 1)
                                        {
                                            TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower() + "¾", Dt.Rows[i][j].ToString().Replace(@"\", "\\\\").Replace("\"", "\\\"").Replace("\'", "\\\'"));
                                        }
                                        else
                                        {
                                            DataTable dtTable = ds.Tables[2];
                                            DataRow[] drs = dtTable.Select("campid = " + Dt.Rows[i]["campid"].ToString());
                                            StringBuilder str = cobj.showrowtable(dtTable, drs);
                                            //TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower(), str.ToString().Replace(@"\", "\\\\").Replace("\"", "\\\"").Replace("\'", "\\\'"));
                                            TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower(), "[");
                                            if (str.Length > 0)
                                            {
                                                TempStr = TempStr + str.ToString();
                                            }
                                            TempStr = TempStr + "]";

                                        }

                                    }
                                    Sb.Append(TempStr + "},");
                                }

                                Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));
                                DataTable dtDelete = ds.Tables[2];
                                ds.Tables.Remove(dtDelete);
                                if (k == ds.Tables.Count - 1)
                                {
                                    Sb.Append("]");
                                }
                                else
                                {
                                    Sb.Append("],");
                                }
                            }
                            else
                            {
                                if (k == ds.Tables.Count - 1)
                                {
                                    Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [ ]");
                                }
                                else
                                {
                                    Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [ ],");
                                }
                            }
                        }
                    }
                    Sb.Append("}");
                }
                else
                {
                    Sb.Append("{\"" + "success\":\"0\",\"error\":\"No data available.\"}");
                }
            }
            else
            {
                Sb.Append("{\"" + "success\":\"0\",\"error\":\"No data available.\"}");
            }
            JObject json = JObject.Parse((string)Sb.ToString());
            return json;
        }


        public JObject getjsonstringforteststudentlist(DataSet ds)
        {
            c_common cobj = new c_common();
            DataTable Dt = new DataTable();
            StringBuilder Sb = new StringBuilder();
            int rowcount = 0;
            int success = 0;
            if (ds.Tables.Count > 0)
            {
                for (int j = 0; j < ds.Tables.Count; j++)
                {
                    if (j > 0)
                    {
                        if (ds.Tables[j].Rows.Count > 0)
                        {
                            rowcount++;
                        }
                    }
                }

                if (rowcount > 0)
                {
                    success = 1;
                }
                if (success == 1)
                {
                    for (int k = 0; k < ds.Tables.Count; k++)
                    {
                        if (k == 0)
                        {
                            Sb.Append("{\"" + "success\":\"" + ds.Tables[0].Rows[0][0].ToString() + "\",\"error\":\"" + ds.Tables[0].Rows[0][1].ToString() + "\",");
                        }
                        if (k == 1)
                        {
                            Dt = new DataTable();
                            Dt = ds.Tables[k];
                            if (Dt.Rows.Count > 0)
                            {
                                string[] StrDc = new string[Dt.Columns.Count];
                                string HeadStr = string.Empty;

                                for (int i = 0; i < Dt.Columns.Count; i++)
                                {
                                    StrDc[i] = Dt.Columns[i].Caption.ToLower();
                                    if (i != Dt.Columns.Count - 1)
                                        HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";
                                    else
                                        HeadStr += "\"" + StrDc[i] + "\" : " + StrDc[i] + i.ToString() + ",";

                                }

                                HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);
                                Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [");

                                for (int i = 0; i < Dt.Rows.Count; i++)
                                {
                                    string TempStr = HeadStr;
                                    Sb.Append("{");
                                    for (int j = 0; j < Dt.Columns.Count; j++)
                                    {
                                        if (j != Dt.Columns.Count - 1)
                                        {
                                            TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower() + "¾", Dt.Rows[i][j].ToString().Replace(@"\", "\\\\").Replace("\"", "\\\"").Replace("\'", "\\\'"));
                                        }
                                        else
                                        {
                                            DataTable dtTable = ds.Tables[2];
                                            DataRow[] drs = dtTable.Select("testid = " + Dt.Rows[i]["testid"].ToString());
                                            StringBuilder str = cobj.showrowtable(dtTable, drs);
                                            //TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower(), str.ToString().Replace(@"\", "\\\\").Replace("\"", "\\\"").Replace("\'", "\\\'"));
                                            TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower(), "[");
                                            if (str.Length > 0)
                                            {
                                                TempStr = TempStr + str.ToString();
                                            }
                                            TempStr = TempStr + "]";

                                        }

                                    }
                                    Sb.Append(TempStr + "},");
                                }

                                Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));
                                DataTable dtDelete = ds.Tables[2];
                                ds.Tables.Remove(dtDelete);
                                if (k == ds.Tables.Count - 1)
                                {
                                    Sb.Append("]");
                                }
                                else
                                {
                                    Sb.Append("],");
                                }
                            }
                            else
                            {
                                if (k == ds.Tables.Count - 1)
                                {
                                    Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [ ]");
                                }
                                else
                                {
                                    Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [ ],");
                                }
                            }
                        }
                    }
                    Sb.Append("}");
                }
                else
                {
                    Sb.Append("{\"" + "success\":\"0\",\"error\":\"No data available.\"}");
                }
            }
            else
            {
                Sb.Append("{\"" + "success\":\"0\",\"error\":\"No data available.\"}");
            }
            JObject json = JObject.Parse((string)Sb.ToString());
            return json;
        }



        public JObject getjsonstringfortournamentstudentlist(DataSet ds)
        {
            c_common cobj = new c_common();
            DataTable Dt = new DataTable();
            StringBuilder Sb = new StringBuilder();
            int rowcount = 0;
            int success = 0;
            if (ds.Tables.Count > 0)
            {
                for (int j = 0; j < ds.Tables.Count; j++)
                {
                    if (j > 0)
                    {
                        if (ds.Tables[j].Rows.Count > 0)
                        {
                            rowcount++;
                        }
                    }
                }

                if (rowcount > 0)
                {
                    success = 1;
                }
                if (success == 1)
                {
                    for (int k = 0; k < ds.Tables.Count; k++)
                    {
                        if (k == 0)
                        {
                            Sb.Append("{\"" + "success\":\"" + ds.Tables[0].Rows[0][0].ToString() + "\",\"error\":\"" + ds.Tables[0].Rows[0][1].ToString() + "\",");
                        }
                        if (k == 1)
                        {
                            Dt = new DataTable();
                            Dt = ds.Tables[k];
                            if (Dt.Rows.Count > 0)
                            {
                                string[] StrDc = new string[Dt.Columns.Count];
                                string HeadStr = string.Empty;

                                for (int i = 0; i < Dt.Columns.Count; i++)
                                {
                                    StrDc[i] = Dt.Columns[i].Caption.ToLower();
                                    if (i != Dt.Columns.Count - 1)
                                        HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";
                                    else
                                        HeadStr += "\"" + StrDc[i] + "\" : " + StrDc[i] + i.ToString() + ",";

                                }

                                HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);
                                Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [");

                                for (int i = 0; i < Dt.Rows.Count; i++)
                                {
                                    string TempStr = HeadStr;
                                    Sb.Append("{");
                                    for (int j = 0; j < Dt.Columns.Count; j++)
                                    {
                                        if (j != Dt.Columns.Count - 1)
                                        {
                                            TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower() + "¾", Dt.Rows[i][j].ToString().Replace(@"\", "\\\\").Replace("\"", "\\\"").Replace("\'", "\\\'"));
                                        }
                                        else
                                        {
                                            DataTable dtTable = ds.Tables[2];
                                            DataRow[] drs = dtTable.Select("tournamentid = " + Dt.Rows[i]["tournamentid"].ToString());
                                            StringBuilder str = cobj.showrowtable(dtTable, drs);
                                            //TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower(), str.ToString().Replace(@"\", "\\\\").Replace("\"", "\\\"").Replace("\'", "\\\'"));
                                            TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower(), "[");
                                            if (str.Length > 0)
                                            {
                                                TempStr = TempStr + str.ToString();
                                            }
                                            TempStr = TempStr + "]";

                                        }

                                    }
                                    Sb.Append(TempStr + "},");
                                }

                                Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));
                                DataTable dtDelete = ds.Tables[2];
                                ds.Tables.Remove(dtDelete);
                                if (k == ds.Tables.Count - 1)
                                {
                                    Sb.Append("]");
                                }
                                else
                                {
                                    Sb.Append("],");
                                }
                            }
                            else
                            {
                                if (k == ds.Tables.Count - 1)
                                {
                                    Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [ ]");
                                }
                                else
                                {
                                    Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [ ],");
                                }
                            }
                        }
                    }
                    Sb.Append("}");
                }
                else
                {
                    Sb.Append("{\"" + "success\":\"0\",\"error\":\"No data available.\"}");
                }
            }
            else
            {
                Sb.Append("{\"" + "success\":\"0\",\"error\":\"No data available.\"}");
            }
            JObject json = JObject.Parse((string)Sb.ToString());
            return json;
        }


        public JObject getjsonstringforcoursewiseplanlist(DataSet ds)
        {
            c_common cobj = new c_common();
            DataTable Dt = new DataTable();
            StringBuilder Sb = new StringBuilder();
            int rowcount = 0;
            int success = 0;
            if (ds.Tables.Count > 0)
            {
                for (int j = 0; j < ds.Tables.Count; j++)
                {
                    if (j > 0)
                    {
                        if (ds.Tables[j].Rows.Count > 0)
                        {
                            rowcount++;
                        }
                    }
                }

                if (rowcount > 0)
                {
                    success = 1;
                }
                if (success == 1)
                {
                    for (int k = 0; k < ds.Tables.Count; k++)
                    {
                        if (k == 0)
                        {
                            Sb.Append("{\"" + "success\":\"" + ds.Tables[0].Rows[0][0].ToString() + "\",\"error\":\"" + ds.Tables[0].Rows[0][1].ToString() + "\",");
                        }
                        if (k == 1)
                        {
                            Dt = new DataTable();
                            Dt = ds.Tables[k];
                            if (Dt.Rows.Count > 0)
                            {
                                string[] StrDc = new string[Dt.Columns.Count];
                                string HeadStr = string.Empty;

                                for (int i = 0; i < Dt.Columns.Count; i++)
                                {
                                    StrDc[i] = Dt.Columns[i].Caption.ToLower();
                                    if (i != Dt.Columns.Count - 1)
                                        HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";
                                    else
                                        HeadStr += "\"" + StrDc[i] + "\" : " + StrDc[i] + i.ToString() + ",";

                                }

                                HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);
                                Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [");

                                for (int i = 0; i < Dt.Rows.Count; i++)
                                {
                                    string TempStr = HeadStr;
                                    Sb.Append("{");
                                    for (int j = 0; j < Dt.Columns.Count; j++)
                                    {
                                        if (j != Dt.Columns.Count - 1)
                                        {
                                            TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower() + "¾", Dt.Rows[i][j].ToString().Replace(@"\", "\\\\").Replace("\"", "\\\"").Replace("\'", "\\\'"));
                                        }
                                        else
                                        {
                                            DataTable dtTable = ds.Tables[2];
                                            DataRow[] drs = dtTable.Select("courseid = " + Dt.Rows[i]["courseid"].ToString());
                                            StringBuilder str = cobj.showrowtable(dtTable, drs);
                                            //TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower(), str.ToString().Replace(@"\", "\\\\").Replace("\"", "\\\"").Replace("\'", "\\\'"));
                                            TempStr = TempStr.Replace((Dt.Columns[j] + j.ToString()).ToLower(), "[");
                                            if (str.Length > 0)
                                            {
                                                TempStr = TempStr + str.ToString();
                                            }
                                            TempStr = TempStr + "]";

                                        }

                                    }
                                    Sb.Append(TempStr + "},");
                                }

                                Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));
                                DataTable dtDelete = ds.Tables[2];
                                ds.Tables.Remove(dtDelete);
                                if (k == ds.Tables.Count - 1)
                                {
                                    Sb.Append("]");
                                }
                                else
                                {
                                    Sb.Append("],");
                                }
                            }
                            else
                            {
                                if (k == ds.Tables.Count - 1)
                                {
                                    Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [ ]");
                                }
                                else
                                {
                                    Sb.Append("\"" + Dt.TableName.ToLower() + "\" : [ ],");
                                }
                            }
                        }
                    }
                    Sb.Append("}");
                }
                else
                {
                    Sb.Append("{\"" + "success\":\"0\",\"error\":\"No data available.\"}");
                }
            }
            else
            {
                Sb.Append("{\"" + "success\":\"0\",\"error\":\"No data available.\"}");
            }
            JObject json = JObject.Parse((string)Sb.ToString());
            return json;
        }




        public StringBuilder showrowtable(DataTable dt, DataRow[] dr)
        {
            StringBuilder Sb = new StringBuilder();
            if (dr.Length > 0)
            {
                string[] strdc = new string[dt.Columns.Count];
                string headstr = string.Empty;

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    strdc[i] = dt.Columns[i].Caption.ToLower();
                    headstr += "\"" + strdc[i] + "\" : \"" + strdc[i] + i.ToString() + "¾" + "\",";
                }


                headstr = headstr.Substring(0, headstr.Length - 1);
                foreach (DataRow dr1 in dr)
                {
                    string tempstr = headstr;
                    Sb.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        tempstr = tempstr.Replace((dt.Columns[j] + j.ToString()).ToLower() + "¾", dr1[j].ToString().Replace(@"\", "\\\\").Replace("\"", "\\\"").Replace("\'", "\\\'"));

                    }
                    Sb.Append(tempstr + "},");
                }
                Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));

            }
            else
            {

            }
            return Sb;


        }


        public void getsetnotificationdata(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string DeviceID = dt.Rows[i]["devicekey"].ToString();
                    string MsgTitle = dt.Rows[i]["msgtitle"].ToString();
                    string Msg = dt.Rows[i]["notification_text"].ToString();
                    string regid = dt.Rows[i]["refid"].ToString();
                    string userid = dt.Rows[i]["userid"].ToString();
                    string MsgID = dt.Rows[i]["MsgID"].ToString();
                    string activity = dt.Rows[i]["Activity"].ToString();
                    string devicetype = dt.Rows[i]["devicetype"].ToString();
                    string reftype = dt.Rows[i]["reftype"].ToString();
                    string SERVER_API_KEY = dt.Rows[i]["SERVER_API_KEY"].ToString();
                    string SENDER_ID = dt.Rows[i]["SENDER_ID"].ToString();
                    string MSG_ICON = dt.Rows[i]["MSG_ICON"].ToString();
                    string MSG_SOUND = dt.Rows[i]["MSG_SOUND"].ToString();
                    string MSG_COLOR = dt.Rows[i]["MSG_COLOR"].ToString();
                    pushandroidnotification(SERVER_API_KEY, SENDER_ID, MSG_ICON, MSG_SOUND, MSG_COLOR, DeviceID, MsgTitle, Msg, regid, activity, MsgID, devicetype, userid);

                }
            }
        }



        public string pushandroidnotification(string SERVER_API_KEY, string SENDER_ID, string MSG_ICON, string MSG_SOUND, string MSG_COLOR, string DeviceID, string MsgTitle, string Msg, string rid, string activity, string mid, string devicetype, string uid)
        {
            String sResponseFromServer = "";
            try
            {
                DataSet dsNoticeConfig = new DataSet();


                var requestUri = "https://fcm.googleapis.com/fcm/send";

                WebRequest webRequest = WebRequest.Create(requestUri);
                webRequest.Method = "POST";
                webRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));
                webRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
                webRequest.ContentType = "application/json";

                var data = new
                {
                    // to = YOUR_FCM_DEVICE_ID, // Uncoment this if you want to test for single device
                    to = DeviceID, // this is for topic
                    priority = "high",
                    content_available = true,
                    notification = new
                    {
                        title = MsgTitle,
                        click_action = "FLUTTER_NOTIFICATION_CLICK",
                        body = Msg,
                        icon = MSG_ICON,
                        sound = MSG_SOUND,
                        color = MSG_COLOR,
                        action_click = activity,
                        badge = 0

                    },
                    data = new
                    {
                        refid = rid,
                        click_action = "FLUTTER_NOTIFICATION_CLICK",
                        msgid = mid,
                        title = MsgTitle,
                        body = Msg,
                        userid = uid,
                        icon = MSG_ICON,
                        sound = MSG_SOUND,
                        color = MSG_COLOR,
                        action_click = activity

                    }

                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);

                Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                webRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = webRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse webResponse = webRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = webResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                sResponseFromServer = tReader.ReadToEnd();

                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {

            }
            return sResponseFromServer;
        }

        public void inserterrorlog(ErrorLog objerror)
        {
            try
            {
                nc.AccessComponent.Insert(objerror, connstr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}