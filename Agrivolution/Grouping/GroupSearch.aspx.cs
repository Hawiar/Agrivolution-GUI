using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agrivolution
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        //Page load event
        protected void Page_Load(object sender, EventArgs a)
        {
            //Sets hidden field for data paramater through html
            UseName.Value = User.Identity.Name;
        }

        //Click event to redirect to the new group template.
        protected void btnCreateGroup_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Grouping/NewGroup.aspx?Msg=New");
        }
    }
}