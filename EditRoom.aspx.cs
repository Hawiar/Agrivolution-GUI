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
    public partial class EditRoom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            String Room = Request.QueryString["Room"];

            SqlConnection connect = new SqlConnection("Data Source=(LocalDb)\v11.0;AttachDbFilename=|DataDirectory|\aspnet-Agrivolution-20150404115207.mdf;Initial Catalog=aspnet-Agrivolution-20150404115207;Integrated Security=True");
            {
                SqlCommand com = new SqlCommand("select * from RoomList where Room='" + Room + "'", connect);
                connect.Open();
                SqlDataReader read = com.ExecuteReader();
                if (read.Read())
                {
                    temperatureValue.Text = read["Temperature"].ToString();
                }
                read.Close();
                connect.Close();
            }
        }
        protected void setRoomValues(object sender, EventArgs e)
        {
            String Room = Request.QueryString["Room"];
            SqlConnection connect = new SqlConnection("Data Source=(LocalDb)\v11.0;AttachDbFilename=|DataDirectory|\aspnet-Agrivolution-20150404115207.mdf;Initial Catalog=aspnet-Agrivolution-20150404115207;Integrated Security=True");
            {
                connect.Open();
                SqlCommand com = new SqlCommand("update RoomList set Temperature=@Temperature where Room=@initializerRoom", connect);
                com.Parameters.AddWithValue("@Temperature", temperatureValue.Text);
                com.Parameters.AddWithValue("@initializerRoom", Room);
                com.ExecuteNonQuery();
                connect.Close();
            }
            //Response.Redirect();  fix this
        }
    }
}