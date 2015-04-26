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
        protected void Page_Load(object sender, EventArgs e)
        {
           
            String MCUID = Request.QueryString["MCUID"];

            SqlConnection connect = new SqlConnection("Data Source=(LocalDb)\v11.0;AttachDbFilename=|DataDirectory|\aspnet-Agrivolution-20150404115207.mdf;Initial Catalog=aspnet-Agrivolution-20150404115207;Integrated Security=True");
            {
                SqlCommand com = new SqlCommand("select * from MCUlist where MCUID='" + MCUID + "'", connect);
                connect.Open();
                SqlDataReader read = com.ExecuteReader();
                if (read.Read())
                {
                    string boolean = read["Fan"].ToString();
                    if (boolean.Equals("True"))
                    {
                        fanSwitch.Text = "1";
                    }
                    else
                    {
                        fanSwitch.Text = "0";
                    }

                    boolean = read["Pump"].ToString();
                    if (boolean.Equals("True"))
                    {
                        pumpSwitch.Text = "1";
                    }
                    else
                    {
                        pumpSwitch.Text = "0";
                    }
                    lightTimerValue.Text = read["LightTimer"].ToString();
                }
                read.Close();
                connect.Close();
            }
        }

        protected void setMCUValues(object sender, EventArgs e)
        {
            String MCU = Request.QueryString["MCUID"];
            int bit;
            if (fanSwitch.Text.Equals("1"))
            {
                bit = 1;
            }
            else
            {
                bit = 0;
            }
            SqlConnection connect = new SqlConnection("Data Source=(LocalDb)\v11.0;AttachDbFilename=|DataDirectory|\aspnet-Agrivolution-20150404115207.mdf;Initial Catalog=aspnet-Agrivolution-20150404115207;Integrated Security=True");
            {
                connect.Open();
                SqlCommand com = new SqlCommand("update MCUlist set Fan=@Fan, LightTimer=@LightTimer, Pump=@Pump where MCUID=@initializerMCUID", connect);
                com.Parameters.AddWithValue("@Fan", bit);
                com.Parameters.AddWithValue("@Pump", bit);
                com.Parameters.AddWithValue("@LightTimer", lightTimerValue.Text);
                com.Parameters.AddWithValue("@initializerMCUID", MCU);
                com.ExecuteNonQuery();
                connect.Close();
            }
            //Response.Redirect();  fix this
        }
    }
}