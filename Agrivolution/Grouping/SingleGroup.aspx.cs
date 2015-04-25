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

namespace Agrivolution.Grouping
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        //Will update comments on this section when i refactor post back
        protected void Page_Load(object sender, EventArgs a)
        {
            //UseName.value = User.Identity.Name;
            if (IsPostBack)
            {

            }
            else
            {
                //Hidden value used to populate datagrid parameter value.
                UseName.Value = User.Identity.Name;
                String GroupName = Request.QueryString["GroupName"];
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
            String GroupName = Request.QueryString["GroupName"];

            try
            {
                SqlConnection connect = new SqlConnection(connString);
                {
                    connect.Open();
                    SqlCommand com = new SqlCommand("update GroupsMasterList set GroupName=@GroupName, Fan=@Fan, LightTimer=@LightTimer where GroupName=@initializerGroupName", connect);
                    com.Parameters.AddWithValue("@GroupName", txtGroupName.Text);
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
            String GroupName = Request.QueryString["GroupName"];
            //Loops through each row in the database to verify if check box has been selected.
            for (int i = 0; i < GridAddMcu.Rows.Count; i++)
            {
                if (((CheckBox)GridAddMcu.Rows[i].FindControl("ChkAdd")).Checked == true)
                {
                    try
                    {
                        SqlConnection connect = new SqlConnection(connString);
                        {
                            int test = Convert.ToInt32(((Label)GridAddMcu.Rows[i].FindControl("lblMcuId")).Text);
                            connect.Open();
                            SqlCommand com = new SqlCommand("update MCU set GroupName=@GroupName where MCUId=@currentMCUID", connect);
                            com.Parameters.AddWithValue("@GroupName", GroupName);
                            com.Parameters.AddWithValue("@currentMCUID", test);
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
            String GroupName = Request.QueryString["GroupName"];

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

        protected void GridAddMcu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}