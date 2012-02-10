using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yom.Web
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string TestValue
        {
            get {
                return System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            }
        }
    }
}