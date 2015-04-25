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
        //Button click event that takes user inputs from html controls and passes them as parameters to a database using a secure connection string. 
        protected void btnSaveGroup_Click(object sender, EventArgs a)
        {
            //int bit = Convert.ToInt32(ddlFan.Text);
            //Try catch statement that sets up a SQL connector and pulls a SQL connection string from the web.config file. Uses Parameters to prevent sql injection attacks.
            try
            {
                String connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection connect = new SqlConnection(connString);
                {
                    SqlCommand com = new SqlCommand("Insert into GroupsMasterList(GroupName, Fan, LightTimer, UserName) Values(@GroupName, @Fan, @LightTimer, @UserName)", connect);
                    com.Parameters.AddWithValue("@GroupName", txtGroupName.Text);
                    com.Parameters.AddWithValue("@Fan", Convert.ToInt32(ddlFan.Text));
                    com.Parameters.AddWithValue("@LightTimer", txtLightTimer.Text);
                    com.Parameters.AddWithValue("@UserName", User.Identity.Name);
                    connect.Open();
                    com.ExecuteNonQuery();
                    connect.Close();
                }
            }
            catch(SqlException e)
            { 
                Console.Write(e.ToString());
            }
            //Redirects to a groups single page passing a query string of the Groups name.
            Response.Redirect("~/Grouping/SingleGroup.aspx?GroupName=" + txtGroupName.Text);
        }
    }
}