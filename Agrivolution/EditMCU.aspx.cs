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
     * This partial class handles the code behind the EditMCU page.  It's fuctionality allows users
     * to edit individual MCUs and will also redirect users to the Room or group associated with 
     * That MCU if there is one
     */
    public partial class EditMCU : System.Web.UI.Page
    {
        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        
        /**
         * loads the page for the user by preppulating peramiters in their respective field.
         */
        protected void Page_Load(object sender, EventArgs e)
        {

            
            String MCUId = Request.QueryString["MCUId"];
            if (IsPostBack)
            {
                startTime.Text = startSelect.Text;
            }
            else //page loads for the first time
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
                            string boolean = read["FanStatus"].ToString(); //loads fan paramiters
                            if (boolean.Equals("True"))
                            {
                                fanSwitch.Text = "1";
                            }
                            else
                            {
                                fanSwitch.Text = "0";
                            }

                            //populates light schedual values

                            DateTime start = DateTime.Parse(read["LightOn"].ToString());
                            DateTime end = DateTime.Parse(read["LightOff"].ToString());
                            DateTime i12 = Convert.ToDateTime("12:00");
                            DateTime i0 = Convert.ToDateTime("00:00");

                            if (start > i12)
                            {
                                startTime.Text = start.AddHours(12).ToString("HH:mm ") + "PM";
                            }
                            else if (start == i0)
                            {
                                startTime.Text = start.AddHours(12).ToString("HH:mm ") + "AM";
                            }
                            else
                            {
                                startTime.Text = start.ToString("HH:mm tt");
                            }

                            if (end > i12)
                            {
                                endTime.Text = end.AddHours(12).ToString("HH:mm ") + "PM";
                            }
                            else if (end == i0)
                            {
                               endTime.Text = end.AddHours(12).ToString("HH:mm ") + "AM";
                            }
                            else
                            {
                                endTime.Text = end.ToString("HH:mm tt");
                            }

                            String group = read["GroupName"].ToString();
                            if(group.Equals("")) //disables fuctionality if there's a group associated with
                            {                    //this MCU and displays a message informing the user that    
                                                 //furctionality is unavalible

                                functionalityUnavailable.Visible = false;
                            }
                            else  //all functionality is abalibe to the user
                            {
                                setMCUButon.Visible = false;
                                functionalityUnavailable.Visible = true;
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
            
        }

        /**
         * This method updates the MCU data table with any values chanded on the EditMCU page 
         * when the user hits the "Save Changes" button (ID="setRoom" on the asp page)
         */
        protected void setMCUValues(object sender, EventArgs e)
        {
            String MCUId = Request.QueryString["MCUId"];

            try { 
            SqlConnection connect = new SqlConnection(connString);
            {
                connect.Open();
                SqlCommand com = new SqlCommand("update MCU set FanStatus=@FanStatus, LightOn=@StartTime, LightOff=@EndTime, CropType=@CropType where MCUId=@initializerMCUId", connect);
                com.Parameters.AddWithValue("@FanStatus", Convert.ToInt32(fanSwitch.Text));
                com.Parameters.AddWithValue("@StartTime", startSelect.Text);
                com.Parameters.AddWithValue("@EndTime", endSelect.Text);
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

        /**
         * Redirects the user to the EditRoom page with a quary string
         * "Room='the text on the button pushed'"
         * 
         * ex) pushing the Room 307 button will take the user to the editRoom
         *      page with the quary string "Room=Room 307"
         */
        protected void redirectToRoom(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Response.Redirect("EditRoom?Room=" + btn.Text);
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