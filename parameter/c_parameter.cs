using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace amt_test.parameter
{
    public class c_parameter
    {
    }
    public class adminlogin
    {
        public string emailid { get; set; }
        public string loginpassword { get; set; }
    }
    public class studentmaster
    {
        public string studid { get; set; }
        public string studfullname { get; set; }
        public string studusername { get; set; }
        public string studdob { get; set; }
        public string studeducation { get; set; }
        public string studgender { get; set; }
        public string studemailid { get; set; }
        public string studmobileno { get; set; }
        public string studpassword { get; set; }
        public string studdocumentname { get; set; }
        public string studdocumentfile { get; set; }
        public string studprofileimg { get; set; }
    }
}