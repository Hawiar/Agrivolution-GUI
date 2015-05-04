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
using System.Drawing;
using System.Text.RegularExpressions;

namespace Agrivolution.Grouping
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        String GroupName;

        //Will update comments on this section when i refactor post back
        protected void Page_Load(object sender, EventArgs a)
        {
            GroupName = Request.QueryString["GroupName"];
            if (IsPostBack)
            {
                lblWarningMessage.Visible = true;
            }
            else
            {
                lblWarningMessage.Visible = false;
                //Hidden value used to populate datagrid parameter value.
                //When page loads populates textboxes and DDL with the groups information from the database.
                UseName.Value = User.Identity.Name;
                try
                {
                    SqlConnection connect = new SqlConnection(connString);
                    {
                        SqlCommand com = new SqlCommand("select * from GroupsMasterList where GroupName='" + GroupName + "'", connect);
                        connect.Open();
                        SqlDataReader read = com.ExecuteReader();
                        if (read.Read())
                        {
                            txtGroupName.Text = read["GroupName"].ToString();
                            string FanStatus = read["Fan"].ToString();
                            if (FanStatus.Equals("True"))
                            {
                                ddlFan.Text = "1";
                            }
                            else
                            {
                                ddlFan.Text = "0";
                            }
                            DateTime StartTime = DateTime.Parse(read["LightOn"].ToString());
                            DateTime EndTime = DateTime.Parse(read["LightOff"].ToString());
                            DateTime twelve = Convert.ToDateTime("12:00");
                            DateTime zero = Convert.ToDateTime("00:00");
                            String LightStartTime;
                            String LightEndTime;
                            if(StartTime > twelve)
                            {
                                //When start time is between 12:01 to 23:59 shows the user the time as PM
                                StartTime = StartTime.AddHours(12);
                                LightStartTime = StartTime.ToString("HH:mm ") + "PM";
                            }
                            else if(StartTime == zero)
                            {
                                //When start time is midnight shows as 12:00 AM
                                StartTime = StartTime.AddHours(12);
                                LightStartTime = StartTime.ToString("HH:mm ") + "AM";
                            }
                            else
                            {
                                //Otherwise display time as normal
                                LightStartTime = StartTime.ToString("HH:mm tt");
                            }

                            if(EndTime > twelve)
                            {
                                //When End time is between 12:01 to 23:59 shows the user the time as PM
                                EndTime = EndTime.AddHours(12);
                                LightEndTime = EndTime.ToString("HH:mm ") + "PM";
                            }
                            else if(EndTime == zero)
                            {
                                //When end time is midnight shows as 12:00 AM
                                EndTime = EndTime.AddHours(12);
                                LightEndTime = EndTime.ToString("HH:mm ") + "AM";

                            }
                            else
                            {
                                //Otherwise display time as normal
                                LightEndTime = EndTime.ToString("HH:mm tt");
                            }

                            txtLightTimer.Text = LightStartTime;
                            txtLightTimerEnd.Text = LightEndTime;
                            read.Close();
                            connect.Close();
                        }
                    }
                }
                catch (SqlException e)
                {

                    Console.Write(e.ToString());
                    throw e;
                }
            }
        }

        //save button click event that pulls the values from the html controls from the user and updates the relevant database columns
        protected void btnSave_Click(object sender, EventArgs a)
        {
            bool isNum =  txtLightTimer.Text.All(Char.IsDigit);
            if (Regex.IsMatch(txtLightTimer.Text, @"^(0[1-9]|1[0-2]):[0-5][0-9] [ap]m$", RegexOptions.IgnoreCase) && Regex.IsMatch(txtLightTimerEnd.Text, @"^(0[1-9]|1[0-2]):[0-5][0-9] [ap]m$", RegexOptions.IgnoreCase))
            {
                bool nameChanged = false;
                SqlCommand com;
                //Checks to see if the user is attempting to change the Group name.
                if (!(GroupName.Equals(txtGroupName.Text)))
                {
                    nameChanged = true;
                }

                try
                {
                    DateTime StartTime = DateTime.Parse(txtLightTimer.Text);
                    DateTime EndTime = DateTime.Parse(txtLightTimerEnd.Text);
                    String LightStartTime = StartTime.ToString("HH:mm:ss");
                    String LightEndTime = EndTime.ToString("HH:mm:ss");
                    TimeSpan duration = EndTime - StartTime;
                    int LightTimeTotal = (int)duration.TotalHours;
                    if (LightTimeTotal < 0)
                    {
                        //Will result in negative if end time is greater than start time. Example Start = 10pm and End = 2am.
                        //Add 24 will make negative number positive and show actual difference.
                        LightTimeTotal = LightTimeTotal + 24;
                    }
                    LightTimeTotal = LightTimeTotal * 60;//Converts total from hours to mins
                    SqlConnection connect = new SqlConnection(connString);
                    {
                        connect.Open();
                        //If the user is trying to change the user name do this.
                        if (nameChanged == true)
                        {
                            com = new SqlCommand("Select GroupName from GroupsMasterList where GroupName=@GroupName", connect);
                            com.Parameters.AddWithValue("@GroupName", txtGroupName.Text);
                            Object groupCheck = com.ExecuteScalar();
                            //If true a name exist in the data base nothing happens warning message appears.
                            if (groupCheck != null)
                            {
                                lblWarningMessage.ForeColor = Color.Red;
                                lblWarningMessage.Text = "Warning: Please choose a Group Name that does not exist in the system.";
                                connect.Close();
                                Response.Redirect("~/Grouping/SingleGroup.aspx?GroupName=" + GroupName);
                            }
                            //Updates the MCUs that belong to this group to the new group and update control values for Fan status and Light.
                            com = getCommand(connect, LightStartTime, LightEndTime, LightTimeTotal);
                            com.ExecuteNonQuery();
                            //Updates the group table with the new group name and control values.
                            com = new SqlCommand("update GroupsMasterList set GroupName=@GroupName, Fan=@Fan, LightOn=@LightOn, LightOff=@LightOff, TotalLight=@TotalLight where GroupName=@initializerGroupName", connect);
                            com.Parameters.AddWithValue("@GroupName", txtGroupName.Text);
                        }
                        else
                        {
                            //If the group name has not changed just update the MCU table for the current group with the new control values.
                            com = getCommand(connect, LightStartTime, LightEndTime, LightTimeTotal);
                            com.ExecuteNonQuery();
                            //Update the group table with the new values for this group.
                            com = new SqlCommand("update GroupsMasterList set Fan=@Fan, LightOn=@LightOn, LightOff=@LightOff, TotalLight=@TotalLight where GroupName=@initializerGroupName", connect);
                        }
                        com.Parameters.AddWithValue("@Fan", Convert.ToInt32(ddlFan.Text));
                        com.Parameters.AddWithValue("@LightOn", LightStartTime);
                        com.Parameters.AddWithValue("@LightOff", LightEndTime);
                        com.Parameters.AddWithValue("@TotalLight", LightTimeTotal);
                        com.Parameters.AddWithValue("@initializerGroupName", GroupName);
                        com.ExecuteNonQuery();
                        connect.Close();
                    }
                }
                catch (SqlException e)
                {

                    Console.Write(e.ToString());
                    throw e;
                }
                Response.Redirect("~/Grouping/SingleGroup.aspx?GroupName=" + txtGroupName.Text);
            }
            else
            {
                lblWarningMessage.ForeColor = Color.Red;
                lblWarningMessage.Text = "Warning: Correct Start and End time format is hh:mm AM/PM";
            }
        }

        //Add Mcu button click event that verifys a row(MCU) check box is selected and if so updates the MCU table with the current group name.
        protected void BtnAddMcu_Click(object sender, EventArgs a)
        {
            DateTime StartTime = DateTime.Parse(txtLightTimer.Text);
            DateTime EndTime = DateTime.Parse(txtLightTimerEnd.Text);
            String LightStartTime = StartTime.ToString("HH:mm:ss");
            String LightEndTime = EndTime.ToString("HH:mm:ss");
            TimeSpan duration = EndTime - StartTime;
            int LightTimeTotal = (int)duration.TotalHours;
            if (LightTimeTotal < 0)
            {
                //Will result in negative if end time is greater than start time. Example Start = 10pm and End = 2am.
                //Add 24 will make negative number positive and show actual difference.
                LightTimeTotal = LightTimeTotal + 24;
            }
            LightTimeTotal = LightTimeTotal * 60;//Converts total from hours to mins

            //Loops through each row in the database to verify if check box has been selected.
            for (int i = 0; i < GridAddMcu.Rows.Count; i++)
            {
                //Update the specific MCU row.
                if (((CheckBox)GridAddMcu.Rows[i].FindControl("ChkAdd")).Checked == true)
                {
                    try
                    {
                        SqlConnection connect = new SqlConnection(connString);
                        {
                            int MCUId = Convert.ToInt32(((Label)GridAddMcu.Rows[i].FindControl("lblMcuId")).Text);
                            connect.Open();
                            SqlCommand com = new SqlCommand("update MCU set GroupName=@GroupName, FanStatus=@Fan, LightOn=@LightOn, LightOff=@LightOff, TotalLight=@TotalLight where MCUId=@currentMCUID", connect);
                            com.Parameters.AddWithValue("@GroupName", txtGroupName.Text);
                            com.Parameters.AddWithValue("@Fan", Convert.ToInt32(ddlFan.Text));
                            com.Parameters.AddWithValue("@LightOn", LightStartTime);
                            com.Parameters.AddWithValue("@LightOff", LightEndTime);
                            com.Parameters.AddWithValue("@TotalLight", LightTimeTotal);
                            com.Parameters.AddWithValue("@currentMCUID", MCUId);
                            com.ExecuteNonQuery();
                            connect.Close();
                        }
                    }
                    catch (SqlException e)
                    {

                        Console.Write(e.ToString());
                        throw e;
                    }
                }
            }
            Response.Redirect("~/Grouping/SingleGroup.aspx?GroupName=" + GroupName);
        }

        //Remove Mcu button click event that verifys a row(MCU) check box is selected and if so updates the MCU table with a null value for the group value.
        protected void btnRemove_Click(object sender, EventArgs a)
        {
            //Loops through each row in the database to verify if check box has been selected.
            for (int i = 0; i < GridRemoveMcu.Rows.Count; i++)
            {
                //temp label and check box to be able to access values for sql params
                Label lbHolder = (Label)GridRemoveMcu.Rows[i].FindControl("lblMCUId2");
                CheckBox chk = (CheckBox)GridRemoveMcu.Rows[i].FindControl("ChkRemove");
                //If check box in data table is selected update database group and MCU table to reflect removal changes.
                if (chk.Checked == true)
                {
                    try
                    {
                        SqlConnection connect = new SqlConnection(connString);
                        {
                            connect.Open();
                            SqlCommand com = new SqlCommand("update MCU set GroupName=@GroupName where MCUId=@currentMCUID", connect);
                            com.Parameters.AddWithValue("@GroupName", DBNull.Value);
                            com.Parameters.AddWithValue("@currentMCUID", lbHolder.Text);
                            com.ExecuteNonQuery();
                            connect.Close();
                        }
                    }
                    catch (SqlException e)
                    {

                        Console.Write(e.ToString());
                        throw e;
                    }
                }
            }
            Response.Redirect("~/Grouping/SingleGroup.aspx?GroupName=" + GroupName);
        }

        //Method used to build a sql command that was being used in several parts of this class.
        public SqlCommand getCommand(SqlConnection connect, String StartTime, String EndTime, int duration)
        {
            SqlCommand com;
            try
            {
                com = new SqlCommand("update MCU set GroupName=@GroupName, FanStatus=@FanStatus, LightOn=@LightOn, LightOff=@LightOff, TotalLight=@TotalLight where GroupName=@initializerGroupName", connect);
                com.Parameters.AddWithValue("@GroupName", txtGroupName.Text);
                com.Parameters.AddWithValue("@FanStatus", Convert.ToInt32(ddlFan.Text));
                com.Parameters.AddWithValue("@LightOn", StartTime);
                com.Parameters.AddWithValue("@LightOff", EndTime);
                com.Parameters.AddWithValue("@TotalLight", duration);
                com.Parameters.AddWithValue("@initializerGroupName", GroupName);
                return com;
            }
            catch (SqlException e)
            {
                Console.Write(e.ToString());
                throw e;
            }
        }
    }
}