using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Agrivolution
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        //Page load event
        protected void Page_Load(object sender, EventArgs a)
        {
            //Sets hidden field for data paramater through html
            UseName.Value = User.Identity.Name;
        }

        //Click event to redirect to the new group template.
        protected void btnCreateGroup_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Grouping/NewGroup.aspx?Msg=New");
        }

        //Click event to remove one or multiple groups from the system.
        protected void btnRemove_Click(object sender, EventArgs a)
        {
            String connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            for (int i = 0; i < GridViewCurrentGroups.Rows.Count; i++)
            {
                //temp label and check box to be able to access values for sql params
                Label lbHolder = (Label)GridViewCurrentGroups.Rows[i].FindControl("lblGroupName");
                CheckBox chk = (CheckBox)GridViewCurrentGroups.Rows[i].FindControl("ChkRemove");
                //If check box in data table is selected update database group and MCU table to reflect removal changes.
                if (chk.Checked == true)
                {
                    try
                    {
                        SqlConnection connect = new SqlConnection(connString);
                        {
                            connect.Open();
                            SqlCommand com = new SqlCommand("update MCU set GroupName=@GroupName where GroupName=@GName", connect);
                            com.Parameters.AddWithValue("@GroupName", DBNull.Value);
                            com.Parameters.AddWithValue("@GName", lbHolder.Text);
                            com.ExecuteNonQuery();
                            com = new SqlCommand("Delete from GroupsMasterList where GroupName=@GroupName", connect);
                            com.Parameters.AddWithValue("@GroupName", lbHolder.Text);
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
            Response.Redirect("~/Grouping/GroupSearch.aspx");
        }
    }
}