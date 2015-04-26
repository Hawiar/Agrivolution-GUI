using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;

namespace Agrivolution.Grouping
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        //Page load event
         protected void Page_Load(object sender, EventArgs a)
        {
            String warning = Request.QueryString["Msg"];
            lblWarning.ForeColor = Color.Red;
             //If an error message appears it reads the query string of the message provided and displays a message to the user.
            if (warning.Equals("Name"))
            {
                lblWarning.Text = "Warning: This group name has already been taken.";
            }
            if (warning.Equals("Timer"))
            {
                lblWarning.Text = "Warning: Please enter a positive integer value for the light cycle.";
            }
        }

        //Button click event that takes user inputs from html controls and passes them as parameters to a database using a secure connection string. 
        protected void btnSaveGroup_Click(object sender, EventArgs a)
        {
            //Try catch statement that sets up a SQL connector and pulls a SQL connection string from the web.config file. Uses Parameters to prevent sql injection attacks.
            try
            {
                String connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection connect = new SqlConnection(connString);
                {
                    connect.Open();
                    SqlCommand com;
                    com = new SqlCommand("Select GroupName from GroupsMasterList where GroupName=@GroupName", connect);
                    com.Parameters.AddWithValue("@GroupName", txtGroupName.Text);
                    Object groupCheck = com.ExecuteScalar();
                    //If true a name exist in the data base nothing happens warning message appears.
                    if (groupCheck != null)
                    {
                         Response.Redirect("~/Grouping/NewGroup.aspx?Msg=Name");
                    }
                    //If the input from user for light schedule is not an int gives error message
                    else if (!(txtLightTimer.Text.All(Char.IsDigit)))
                    {
                        Response.Redirect("~/Grouping/NewGroup.aspx?Msg=Timer");
                    }
                    //If value validation checking is correct create the group and redirect to its single group page.
                    else
                    {
                        com = new SqlCommand("Insert into GroupsMasterList(GroupName, Fan, LightTimer, UserName) Values(@GroupName, @Fan, @LightTimer, @UserName)", connect);
                        com.Parameters.AddWithValue("@GroupName", txtGroupName.Text);
                        com.Parameters.AddWithValue("@Fan", Convert.ToInt32(ddlFan.Text));
                        com.Parameters.AddWithValue("@LightTimer", txtLightTimer.Text);
                        com.Parameters.AddWithValue("@UserName", User.Identity.Name);
                        com.ExecuteNonQuery();
                        connect.Close();
                        //Redirects to a groups single page passing a query string of the Groups name.
                        Response.Redirect("~/Grouping/SingleGroup.aspx?GroupName=" + txtGroupName.Text);
                    }
                }
            }
            catch(SqlException e)
            { 
                Console.Write(e.ToString());
                throw e;
            }
        }
    }
}