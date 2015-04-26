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

            }
            else
            {
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
                            txtLightTimer.Text = read["LightTimer"].ToString();
                            read.Close();
                            connect.Close();
                        }
                    }
                }
                catch (SqlException e)
                {

                    Console.Write(e.ToString());
                }
            }
        }

        //save button click event that pulls the values from the html controls from the user and updates the relevant database columns
        protected void btnSave_Click(object sender, EventArgs a)
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
                SqlConnection connect = new SqlConnection(connString);
                {
                    connect.Open();
                    //If the user is trying to change the user name do this.
                    if (nameChanged == true)
                    {
                        //Updates the MCUs that belong to this group to the new group and update control values for Fan status and Light.
                        com = new SqlCommand("update MCU set GroupName=@GroupName, FanStatus=@FanStatus, LightSchedule=@LightSchedule where GroupName=@initializerGroupName", connect);
                        com.Parameters.AddWithValue("@GroupName", txtGroupName.Text);
                        com.Parameters.AddWithValue("@FanStatus", Convert.ToInt32(ddlFan.Text));
                        com.Parameters.AddWithValue("@LightSchedule", txtLightTimer.Text);
                        com.Parameters.AddWithValue("@initializerGroupName", GroupName);
                        com.ExecuteNonQuery();
                        //Updates the group table with the new group name and control values.
                        com = new SqlCommand("update GroupsMasterList set GroupName=@GroupName, Fan=@Fan, LightTimer=@LightTimer where GroupName=@initializerGroupName", connect);
                        com.Parameters.AddWithValue("@GroupName", txtGroupName.Text);
                    }
                    else
                    {
                        //If the group name has not changed just update the MCU table for the current group with the new control values.
                        com = new SqlCommand("update MCU set FanStatus=@Fan, LightSchedule=@LightTimer where GroupName=@initializerGroupName", connect);
                        com.Parameters.AddWithValue("@GroupName", txtGroupName.Text);
                        com.Parameters.AddWithValue("@Fan", Convert.ToInt32(ddlFan.Text));
                        com.Parameters.AddWithValue("@LightTimer", txtLightTimer.Text);
                        com.Parameters.AddWithValue("@initializerGroupName", GroupName);
                        com.ExecuteNonQuery();
                        //Update the group table with the new values for this group.
                        com = new SqlCommand("update GroupsMasterList set Fan=@Fan, LightTimer=@LightTimer where GroupName=@initializerGroupName", connect);
                    }
                    com.Parameters.AddWithValue("@Fan", Convert.ToInt32(ddlFan.Text));
                    com.Parameters.AddWithValue("@LightTimer", txtLightTimer.Text);
                    com.Parameters.AddWithValue("@initializerGroupName", GroupName);
                    com.ExecuteNonQuery();
                    connect.Close();
                }
            }
            catch (SqlException e)
            {

                Console.Write(e.ToString());
            }
            Response.Redirect("~/Grouping/SingleGroup.aspx?GroupName=" + txtGroupName.Text);
        }

        //Add Mcu button click event that verifys a row(MCU) check box is selected and if so updates the MCU table with the current group name.
        protected void BtnAddMcu_Click(object sender, EventArgs a)
        {
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
                            SqlCommand com = new SqlCommand("update MCU set GroupName=@GroupName, FanStatus=@Fan, LightSchedule=@LightTimer where MCUId=@currentMCUID", connect);
                            com.Parameters.AddWithValue("@GroupName", txtGroupName.Text);
                            com.Parameters.AddWithValue("@Fan", Convert.ToInt32(ddlFan.Text));
                            com.Parameters.AddWithValue("@LightTimer", txtLightTimer.Text);
                            com.Parameters.AddWithValue("@currentMCUID", MCUId);
                            com.ExecuteNonQuery();
                            connect.Close();
                        }
                    }
                    catch (SqlException e)
                    {

                        Console.Write(e.ToString());
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
                    }
                }
            }
            Response.Redirect("~/Grouping/SingleGroup.aspx?GroupName=" + GroupName);
        }

        SqlCommand getCommand(SqlConnection connect, String chooseCommand)
        {
            SqlCommand com;
            com = new SqlCommand("update MCU set GroupName=@GroupName, FanStatus=@FanStatus, LightSchedule=@LightSchedule where GroupName=@initializerGroupName", connect);
            com.Parameters.AddWithValue("@GroupName", txtGroupName.Text);
            com.Parameters.AddWithValue("@FanStatus", Convert.ToInt32(ddlFan.Text));
            com.Parameters.AddWithValue("@LightSchedule", txtLightTimer.Text);
            com.Parameters.AddWithValue("@initializerGroupName", GroupName);
            return com;
        }
    }
}