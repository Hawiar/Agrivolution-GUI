using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Agrivolution.Grouping
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSaveGroup_Click(object sender, EventArgs e)
        {
            int bit;
            if(ddlFan.Text.Equals("1"))
            {
                bit = 1;
            }
            else
            {
                bit = 0;
            }
            SqlConnection connect = new SqlConnection("Data Source=Mateo94\\sqlexpress;Initial Catalog=aspnet-Agrivolution-20150404115207;Integrated Security=True");
            {
                SqlCommand com = new SqlCommand("Insert into GroupsMasterList(GroupName, Fan, LightTimer) Values('@GroupName', @Fan, @LightTimer)", connect);
                com.Parameters.AddWithValue("@GroupName", txtGroupName.Text);
                com.Parameters.AddWithValue("@Fan", bit);
                com.Parameters.AddWithValue("@LightTimer", txtLightTimer.Text);

                connect.Open();
                com.ExecuteNonQuery();
                connect.Close();
            }
            Response.Redirect("~/Grouping/SingleGroup.aspx?GroupName=" + txtGroupName.Text);
        }
    }
}