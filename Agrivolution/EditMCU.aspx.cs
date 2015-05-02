using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace Agrivolution
{
    public partial class EditMCU : System.Web.UI.Page
    {
        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {

            
            String MCUId = Request.QueryString["MCUId"];
            if (IsPostBack)
            {

            }
            else
            {
                MCULabel.Text = MCUId;

                try
                {
                    SqlConnection connect = new SqlConnection(connString);
                    {
                        SqlCommand com = new SqlCommand("select * from MCU where MCUId='" + MCUId + "'", connect);
                        connect.Open();
                        SqlDataReader read = com.ExecuteReader();
                        if (read.Read())
                        {
                            string boolean = read["FanStatus"].ToString();
                            if (boolean.Equals("True"))
                            {
                                fanSwitch.Text = "1";
                            }
                            else
                            {
                                fanSwitch.Text = "0";
                            }

                            lightTimerValue.Text = read["LightSchedule"].ToString();
                            cropType.Text = read["CropType"].ToString();
                        }
                        read.Close();
                        connect.Close();
                    }
                }
                catch (SqlException ex)
                {

                    Console.Write(ex.ToString());
                }
            }
            
        }

        protected void setMCUValues(object sender, EventArgs e)
        {
            String MCUId = Request.QueryString["MCUId"];

            try { 
            SqlConnection connect = new SqlConnection(connString);
            {
                connect.Open();
                SqlCommand com = new SqlCommand("update MCU set FanStatus=@FanStatus, LightSchedule=@LightSchedule, CropType=@CropType where MCUId=@initializerMCUId", connect);
                com.Parameters.AddWithValue("@FanStatus", Convert.ToInt32(fanSwitch.Text));
                com.Parameters.AddWithValue("@LightSchedule", lightTimerValue.Text);
                com.Parameters.AddWithValue("@CropType", cropType.Text);
                com.Parameters.AddWithValue("@initializerMCUId", MCUId);
                com.ExecuteNonQuery();
                connect.Close();
            }
            }
            catch (SqlException ex)
            {

                Console.Write(ex.ToString());
            }
        }

        protected void redirectToRoom(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Response.Redirect("EditRoom?Room=" + btn.Text);
        }

        protected void redirectToGroup(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            //Response.Redirect("SingleGroup?GroupName=" + btn.Text);      fix this
        }

        protected void returnClick(object sender, EventArgs e)
        {
            //Response.Redirect("Controls");      fix this
        }
    }
}