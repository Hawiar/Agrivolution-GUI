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
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
            {

            }
            else
            {
                String GroupName = Request.QueryString["GroupName"];

                SqlConnection connect = new SqlConnection("Data Source=Mateo94\\sqlexpress;Initial Catalog=aspnet-Agrivolution-20150404115207;Integrated Security=True");
                {
                    SqlCommand com = new SqlCommand("select * from GroupsMasterList where GroupName='" + GroupName + "'", connect);
                    connect.Open();
                    SqlDataReader read = com.ExecuteReader();
                    if (read.Read())
                    {
                        txtGroupName.Text = read["GroupName"].ToString();
                        string boool = read["Fan"].ToString();
                        if (boool.Equals("True"))
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
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            String GroupName = Request.QueryString["GroupName"];
            int bit;
            if (ddlFan.Text.Equals("1"))
            {
                bit = 1;
            }
            else
            {
                bit = 0;
            }
            SqlConnection connect = new SqlConnection("Data Source=Mateo94\\sqlexpress;Initial Catalog=aspnet-Agrivolution-20150404115207;Integrated Security=True");
            {
                connect.Open();
                SqlCommand com = new SqlCommand("update GroupsMasterList set GroupName=@GroupName, Fan=@Fan, LightTimer=@LightTimer where GroupName=@initializerGroupName", connect);
                com.Parameters.AddWithValue("@GroupName", txtGroupName.Text);
                com.Parameters.AddWithValue("@Fan", bit);
                com.Parameters.AddWithValue("@LightTimer", txtLightTimer.Text);
                com.Parameters.AddWithValue("@initializerGroupName", GroupName);
                com.ExecuteNonQuery();
                connect.Close();
            }
            Response.Redirect("~/Grouping/SingleGroup.aspx?GroupName=" + txtGroupName.Text);
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String GroupName = Request.QueryString["GroupName"];
            for(int i = 0; i<GridView1.Rows.Count; i++)
            {
                Label lbHolder = (Label)GridView1.Rows[i].FindControl("lblMcuId");
                CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
                if(chk.Checked == true)
                {
                    SqlConnection connect = new SqlConnection("Data Source=Mateo94\\sqlexpress;Initial Catalog=aspnet-Agrivolution-20150404115207;Integrated Security=True");
                    {
                        connect.Open();
                        SqlCommand com = new SqlCommand("update MCUList set [Group]=@GroupName where MCUID=@currentMCUID", connect);
                        com.Parameters.AddWithValue("@GroupName", GroupName);
                        com.Parameters.AddWithValue("@currentMCUID", lbHolder.Text);
                        com.ExecuteNonQuery();
                        connect.Close();
                    }
                }
            }
            Response.Redirect("~/Grouping/SingleGroup.aspx?GroupName=" + GroupName);
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            String GroupName = Request.QueryString["GroupName"];
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                Label lbHolder = (Label)GridView2.Rows[i].FindControl("lblMCUId2");
                CheckBox chk = (CheckBox)GridView2.Rows[i].FindControl("ChkRemove");
                if (chk.Checked == true)
                {
                    SqlConnection connect = new SqlConnection("Data Source=Mateo94\\sqlexpress;Initial Catalog=aspnet-Agrivolution-20150404115207;Integrated Security=True");
                    {
                        connect.Open();
                        SqlCommand com = new SqlCommand("update MCUList set [Group]=@GroupName where MCUID=@currentMCUID", connect);
                        com.Parameters.AddWithValue("@GroupName", DBNull.Value);
                        com.Parameters.AddWithValue("@currentMCUID", lbHolder.Text);
                        com.ExecuteNonQuery();
                        connect.Close();
                    }
                }
            }
            Response.Redirect("~/Grouping/SingleGroup.aspx?GroupName=" + GroupName);
        }
    }
}