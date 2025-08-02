using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForms
{
    public partial class Default1 : System.Web.UI.Page
    {
        public bool animation=true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["AnimacionMostrada"] == null)
            {
                animation = true;
                Session["AnimacionMostrada"] = true;
            }
            else
            {
                animation = false;
            }
        }
    }
}