using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


namespace Agrivolution
{
    public partial class AddMCU : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserID.Value = User.Identity.Name;
        }

        protected void CreateMCU(object sender, EventArgs a)
        {
            if (Page.IsValid)
            {
                string query = "INSERT INTO MCU ([MCUId], [Room], [Facility], [UserName], [GroupName], [FanStatus], [LightStatus], [PumpStatus], [CropType], [StartDate], [LightOn], [LightOff], [TotalLight])" +
                    " VALUES (@MCUId, @Room, @Facility, @UserName, @GroupName, @FanStatus, @LightStatus, @PumpStatus, @CropType, @StartDate, @LightOn, @LightOff, @TotalLight)";
                string conString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection con = new SqlConnection(conString);
                SqlCommand cmd = new SqlCommand(query, con);

                //checking of these values are done via validationfields in the asp page
                cmd.Parameters.AddWithValue("@MCUId", ID_text.Text);

                if (roomList.Text.Equals("New Room"))
                {
                    cmd.Parameters.AddWithValue("@Room", newRoom.Text);
                }
                else { cmd.Parameters.AddWithValue("@Room", roomList.Text); }
                if (facilityList.Text.Equals("New Facility"))
                {
                    cmd.Parameters.AddWithValue("@Facility", newFacility.Text);
                }
                else { cmd.Parameters.AddWithValue("@Facility", facilityList.Text); }
                if (groupList.Text.Equals("New Group"))
                {
                    cmd.Parameters.AddWithValue("@GroupName", newGroup.Text);
                }
                else { cmd.Parameters.AddWithValue("@GroupName", groupList.Text); }
                    
                cmd.Parameters.AddWithValue("@CropType", CropType.Text);

                String time = parseTimeOn(LOn.Text);
                cmd.Parameters.AddWithValue("@LightOn", time);
                time = parseTimeOff(LOff.Text);
                cmd.Parameters.AddWithValue("@LightOff", time);
                int totalLight = calcTotalTime(LOn.Text, LOff.Text);
                cmd.Parameters.AddWithValue("@TotalLight", totalLight);

                //does not need checking as values are provided
                cmd.Parameters.AddWithValue("@UserName", User.Identity.Name);
                cmd.Parameters.AddWithValue("@FanStatus", "0");
                cmd.Parameters.AddWithValue("@LightStatus", "0");
                cmd.Parameters.AddWithValue("@PumpStatus", "0");
                cmd.Parameters.AddWithValue("@StartDate", DateTime.Today);


                try 
                 {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                 }
                  catch(SqlException e)
                  { Console.Write(e.ToString()); }
            }
            else { messageLabel.Text = "Page is not valid"; }
        }

        private bool isValidNumber(String s)
        {
            int temp;
            return int.TryParse(s, out temp);
        }

        protected void IdFormatCheck_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = isValidNumber(args.Value);
        }

        private int calcTotalTime(String start, String end)
        {
            DateTime startTime = DateTime.Parse(start);
            DateTime endTime = DateTime.Parse(end);
            TimeSpan temp = new TimeSpan(12, 0, 0);
            int totalmins = 0;
            //if the startTime is before endTime and
            if (startTime.CompareTo(endTime) < 0)
            {
                temp = endTime.Subtract(startTime);
            }
                //if startTime is after endTime and
                //startTime is after 12
            else if (startTime.Hour >= 12 && startTime.CompareTo(endTime) > 0)
            {
                endTime = endTime.Add(temp);
                temp = endTime.Subtract(startTime);
            }
            else 
            {
                startTime = startTime.Subtract(temp);
                startTime = startTime.AddHours(endTime.Hour);
                startTime = startTime.AddMinutes(endTime.Minute);
            }

            //convert to minutes
            totalmins = temp.Hours * 60;
            totalmins += temp.Minutes;
            return totalmins;
        }

        private String parseTimeOn(String t)
        {
            DateTime timeCheck = DateTime.Parse(t);
            //if the box is checked and the hour is less than 12...
            if (timeCheck.Hour <= 12 && timeOfDayLightOn.Checked)
            {
                timeCheck.Add(new TimeSpan(12, 0, 0));
            }
            String timeString = timeCheck.ToString("hh:mm:ss");
            return timeString;
        }

        private String parseTimeOff(String t)
        {
            DateTime timeCheck = DateTime.Parse(t);
            //if the box is checked and the hour is less than 12...
            if (timeCheck.Hour <= 12 && timeOfDayLightOff.Checked)
            {
                timeCheck.Add(new TimeSpan(12, 0, 0));
            }
            String timeString = timeCheck.ToString("hh:mm:ss");
            return timeString;
        }

        protected void facilityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (facilityList.SelectedValue.Equals("New Facility"))
            {
                
                newFacility.Visible = true;
            }
            else { newFacility.Visible = false; }
        }


        protected void roomList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(roomList.SelectedValue.Equals("New Room"))
            {
                newRoom.Visible = true;
            }
            else { newRoom.Visible = false; }
        }

        protected void groupList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (groupList.SelectedValue.Equals("New Group"))
            {
                newGroup.Visible = true;
            }
            else { newGroup.Visible = false; }
        }

    }
}