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

            
            //String MCUId = Request.QueryString["MCUId"];
            String MCUId = "1";
            
                try
                {
                    SqlConnection connect = new SqlConnection(connString);
                    {
                        SqlCommand com = new SqlCommand("select * from MCUlist where MCUId='" + MCUId + "'", connect);
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

                            boolean = read["PumpStatus"].ToString();
                            if (boolean.Equals("True"))
                            {
                                pumpSwitch.Text = "1";
                            }
                            else
                            {
                                pumpSwitch.Text = "0";
                            }
                            lightTimerValue.Text = read["LightSchedule"].ToString();
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

        protected void setMCUValues(object sender, EventArgs e)
        {
            //String MCUId = Request.QueryString["MCUId"];
            String MCUId = "1";

            int fanBit;
            int pumpBit;
            if (fanSwitch.Text.Equals("1"))
            {
                fanBit = 1;
            }
            else
            {
                fanBit = 0;
            }
            if (pumpSwitch.Text.Equals("1"))
            {
                pumpBit = 1;
            }
            else
            {
                pumpBit = 0;
            }
            try { 
            SqlConnection connect = new SqlConnection(connString);
            {
                connect.Open();
                SqlCommand com = new SqlCommand("update MCU set FanStatus=@FanStatus, LightSchedule=@LightSchedule, PumpStatus=@PumpStatus where MCUId=@initializerMCUId", connect);
                com.Parameters.AddWithValue("@FanStatus", fanBit);
                com.Parameters.AddWithValue("@PumpStatus", pumpBit);
                com.Parameters.AddWithValue("@LightSchedule", lightTimerValue.Text);
                com.Parameters.AddWithValue("@initializerMCUId", MCUId);
                com.ExecuteNonQuery();
                connect.Close();
            }
            }
            catch (SqlException ex)
            {

                Console.Write(ex.ToString());
            }
            //Response.Redirect();  fix this
        }
    }
}