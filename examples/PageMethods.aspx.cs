using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestCalendarControl
{
    public partial class PageMethods : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [ScriptMethod, WebMethod]
        public static string GetLabelText(string FirstName,string LastName)
        {
            return FirstName +" "+ LastName +", " + "Hello";
        }
    }
}