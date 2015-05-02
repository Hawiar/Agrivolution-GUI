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
    public partial class EditRoom : System.Web.UI.Page
    {
        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            String room = Request.QueryString["Room"];
            roomLabel.Text = room;

            try
            {
                SqlConnection connect = new SqlConnection(connString);
                {
                    SqlCommand com = new SqlCommand("select * from HVAC where Room='" + room + "'", connect);
                    connect.Open();
                    SqlDataReader read = com.ExecuteReader();
                    if (read.Read())
                    {
                        temperature.Text = read["Temperature"].ToString();
                        co2.Text = read["CO2"].ToString();
                        humidity.Text = read[" Humidity"].ToString();
                        string boolean = read["PumpStatus"].ToString();
                        if (boolean.Equals("True"))
                        {
                            pumpSwitch.Text = "1";
                        }
                        else
                        {
                            pumpSwitch.Text = "0";
                        }
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

        protected void redirectToMCU(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Response.Redirect("EditMCU?MCUId=" + btn.Text);
        }

        protected void redirectToGroup(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            //Response.Redirect("SingleGroup?GroupName=" + btn.Text);      fix this
        }

        protected void setRoomValues(object sender, EventArgs e)
        {

        }

        protected void returnClick(object sender, EventArgs e)
        {
            //Response.Redirect("Controls");      fix this
        }
    }
}