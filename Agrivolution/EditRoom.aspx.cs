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
    /**
     * This partial class handles the code behind the EditRoom page.  It's fuctionality allows users
     * to edit individual Rooms and will also redirect users to the MCU or group associated with That
     * MCU if there is one
     */
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
                    SqlCommand com = new SqlCommand("select * from Room where Room='" + room + "'", connect);
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

        /**
         * Updates the HVAC data table with any values chanded on the EditRoom page 
         * when the user hits the "Save Changes" button (ID="setRoomButon" on the asp page)
         */
        protected void setRoomValues(object sender, EventArgs e)
        {
           /* if(temperature.Text.All(Char.IsDigit) || co2.Text.All(Char.IsDigit) || humidity.Text.All(Char.IsDigit))
            {
                // do error stuff
            }
            else if(//constraints stuff)
            {
                // do error stuff
            }
            else
            {
                String Room = Request.QueryString["Room"];

                try
                {
                    SqlConnection connect = new SqlConnection(connString);
                    {
                        connect.Open();
                        SqlCommand com = new SqlCommand("update Room set Temperature=@Temperature, CO2=@CO2, Humidity=@Humidity where Room=@initializerRoom", connect);
                        com.Parameters.AddWithValue("@PumpStatus", Convert.ToInt32(pumpSwitch.Text));
                        com.Parameters.AddWithValue("@Temperature", temperature.Text);
                        com.Parameters.AddWithValue("@CO2", co2.Text);
                        com.Parameters.AddWithValue("@Humidity", humidity.Text);
                        com.Parameters.AddWithValue("@initializerRoom", Room);
                        com.ExecuteNonQuery();
                        connect.Close();
                    }
                }
                catch (SqlException ex)
                {
                    Console.Write(ex.ToString());
                }
            }*/
        }

        /**
         * Redirects the user to the EditMCU page with a quary string
         * "MCUId='the text on the button pushed'"
         * 
         * ex) pushing the "560" button will take the user to the EditMCU
         *      page with the quary string "MCUId=560"
         */
        protected void redirectToMCU(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Response.Redirect("EditMCU?MCUId=" + btn.Text);
        }

        /**
         * Redirects the user to the SingleGroup page with a quary string
         * "GroupName='the text on the button pushed'"
         * 
         * ex) pushing the "Group A" button will take the user to the SingleGroup
         *      page with the quary string "GroupName=Group A"
         */
        protected void redirectToGroup(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            //Response.Redirect("SingleGroup?GroupName=" + btn.Text);      fix this
        }

        /**
         * redirects the user back to the main controls page
         */
        protected void returnClick(object sender, EventArgs e)
        {
            //Response.Redirect("Controls");      fix this
        }
    }
}