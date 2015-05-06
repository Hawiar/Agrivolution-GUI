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
using System.Windows;

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
        private static readonly Decimal TEMP_MAX = 100;
        private static readonly Decimal TEMP_MIN = 50;
        private static readonly Decimal CO2_MAX = 100;
        private static readonly Decimal CO2_MIN = 50;
        private static readonly Decimal HUMIDITY_MAX = 100;
        private static readonly Decimal HUMIDITY_MIN = 50;

        protected void Page_Load(object sender, EventArgs e)
        {
            UseName.Value = User.Identity.Name;
            if (IsPostBack)
            {

            }
            else
            {
                String room = Request.QueryString["Room"];
                roomLabel.Text = room;

                try
                {
                    SqlConnection connect = new SqlConnection(connString);
                    {
                        SqlCommand com = new SqlCommand("select * from ROOM where Room='" + room + "' AND UserName=@UserName", connect);
                        connect.Open();
                        com.Parameters.AddWithValue("UserName", User.Identity.Name);
                        SqlDataReader read = com.ExecuteReader();
                        if (read.Read())
                        {
                            temperature.Text = read["Temperature"].ToString();
                            co2.Text = read["CO2"].ToString();
                            humidity.Text = read["Humidity"].ToString();
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
                        SqlCommand checkExists = new SqlCommand("select * from MCU where Room='" + room + "' AND UserName=@UserName", connect);
                        com.Parameters.AddWithValue("UserName", User.Identity.Name);
                        Object exists = checkExists.ExecuteScalar();
                        if (exists == null)
                        {
                            deleteRoom.Visible = true;
                        }
                        else
                        {
                            deleteRoom.Visible = false;
                        }
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
         * Updates the HVAC data table with any values chanded on the EditRoom page
         * when the user hits the "Save Changes" button (ID="setRoomButon" on the asp page)
         */
        protected void setRoomValues(object sender, EventArgs e)
        {
            try
            {
                Decimal Temp = Convert.ToDecimal(temperature.Text);
                Decimal CO2 = Convert.ToDecimal(co2.Text);
                Decimal Humidity = Convert.ToDecimal(humidity.Text);
                if (checkErrors(Temp, CO2, Humidity))
                {
                    return;
                }
                else
                {
                    String Room = Request.QueryString["Room"];

                    try
                    {
                        SqlConnection connect = new SqlConnection(connString);
                        {
                            connect.Open();
                            SqlCommand existsCheck = new SqlCommand("select * from ROOM where Room='" + Room + "'", connect);
                            Object exists = existsCheck.ExecuteScalar();
                            if (exists == null)
                            {
                                SqlCommand com = new SqlCommand("INSERT INTO dbo.ROOM (Room,Temperature,CO2,Humidity,PumpStatus,UserName) VALUES(@initializerRoom,@Temperature,@CO2,@Humidity,@PumpStatus,@UserName)", connect);
                                com.Parameters.AddWithValue("@PumpStatus", Convert.ToInt32(pumpSwitch.Text));
                                com.Parameters.AddWithValue("@Temperature", temperature.Text);
                                com.Parameters.AddWithValue("@CO2", co2.Text);
                                com.Parameters.AddWithValue("@Humidity", humidity.Text);
                                com.Parameters.AddWithValue("@initializerRoom", Room);
                                com.Parameters.AddWithValue("@UserName", User.Identity.Name);
                                com.ExecuteNonQuery();
                            }
                            else
                            {
                                SqlCommand com = new SqlCommand("update Room set Temperature=@Temperature, CO2=@CO2, Humidity=@Humidity where Room=@initializerRoom", connect);
                                com.Parameters.AddWithValue("@PumpStatus", Convert.ToInt32(pumpSwitch.Text));
                                com.Parameters.AddWithValue("@Temperature", temperature.Text);
                                com.Parameters.AddWithValue("@CO2", co2.Text);
                                com.Parameters.AddWithValue("@Humidity", humidity.Text);
                                com.Parameters.AddWithValue("@initializerRoom", Room);
                                com.ExecuteNonQuery();
                            }

                            connect.Close();
                        }
                        // add trigger here
                    }
                    catch (SqlException ex)
                    {
                        Console.Write(ex.ToString());
                    }
                }
            }
            catch (FormatException ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts",
                        "<script>alert('Input for temperature, CO2, and Humidity cannont contain charicters.');</script>");
            }

        }

        /**
         * helper methrod for setRoomValues().  Checks the temperature, CO2, and
         * humidity textbox for correct data format and restrictions.  If an error
         * is detected, the user is notified and this method returns True.  If there
         * are no errors, returns false
         */
        private Boolean checkErrors(Decimal Temp, Decimal CO2, Decimal Humidity)
        {
            if (Temp > TEMP_MAX || Temp < TEMP_MIN)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts",
                        "<script>alert('Infput for temperature must be between " + TEMP_MIN + " and " + TEMP_MIN + ".');</script>");
                return true;
            }
            else if (CO2 > CO2_MAX || CO2 < CO2_MIN)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts",
                        "<script>alert('Input for CO2 must be between " + CO2_MIN + " and " + CO2_MIN + ".');</script>");
                return true;
            }
            else if (Humidity > HUMIDITY_MAX || Humidity < HUMIDITY_MIN)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts",
                        "<script>alert('Input for humidity must be between " + HUMIDITY_MIN + " and " + HUMIDITY_MAX + ".');</script>");
                return true;
            }
            return false;
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
         * redirects the user back to the main controls page
         */
        protected void returnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Controls");
        }

        /**
         * Deletes the room specified by the quary string
         */
        protected void delete(object sender, EventArgs e)
        {
            String Room = Request.QueryString["Room"];

            try
            {
                SqlConnection connect = new SqlConnection(connString);
                {
                    connect.Open();

                    SqlCommand com = new SqlCommand("DELETE from ROOM WHERE Room=@initializerRoom", connect);
                    com.Parameters.AddWithValue("@initializerRoom", Room);
                    com.ExecuteNonQuery();

                    connect.Close();
                    allRooms.DataBind();
                    Response.Redirect("EditRoom");
                }
            }
            catch (SqlException ex)
            {
                Console.Write(ex.ToString());
            }

        }
    }
}