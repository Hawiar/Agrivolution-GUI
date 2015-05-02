using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace Agrivolution
{
    public partial class MCUSearch : System.Web.UI.Page
    {
        //Key used as unique variable for handling on further pages
        public static string keyBreak;
        //The type of the unique variable for easy handling.
        public static string typeBreak;
        //MCUID of the selected reduction.
        public static string mcuidSelected;
        //Humidity of current item
        public static Label humidity;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //Default Page Title
                Page.Title = "All";
                keyBreak = null;
                typeBreak = null;
                mcuidSelected = null;
            }
            else
            {
                //Post-back Specific Stuff
            }
        }

        /*
            * Generates a collection of information (static MCU-related information) based on the key/type
            * 
            **/
        protected void generateStatsTable()
        {
            DataTable dt = new DataTable();
            String connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connect = new SqlConnection(connString);
            SqlCommand com = new SqlCommand("SELECT * FROM [dbo].[MCUList] WHERE " + '"' + typeBreak + '"' + "='" + keyBreak + "' AND MCUID=" + mcuidSelected, connect);
            //com.Parameters.AddWithValue("@key", keyBreak);
            //com.Parameters.AddWithValue("@type", typeBreak);
            try
            {
                //Generate the connection and query string to pull the table
                connect.Open();
                SqlDataReader reader = com.ExecuteReader();


                //Generate SQL Statements within If blocks
                //That way I only pull exactly what I need


                //Create the DataTable to display on the page
                //0 Humidity
                //1 Temp
                //2 CO2 
                //3 Light Status
                //4 Crop Type
                switch (typeBreak)
                {
                    case "Group":
                        StatsBlock.Columns[0].Visible = false;
                        StatsBlock.Columns[1].Visible = false;
                        StatsBlock.Columns[2].Visible = false;
                        StatsBlock.Columns[3].Visible = true;
                        break;
                    case "MCUID":
                        StatsBlock.Columns[0].Visible = true;
                        StatsBlock.Columns[1].Visible = true;
                        StatsBlock.Columns[2].Visible = true;
                        StatsBlock.Columns[3].Visible = true;
                        break;
                    case "Room":
                        StatsBlock.Columns[0].Visible = true;
                        StatsBlock.Columns[1].Visible = true;
                        StatsBlock.Columns[2].Visible = false;
                        StatsBlock.Columns[3].Visible = false;
                        break;
                    case "Facility":
                        StatsBlock.Columns[0].Visible = false;
                        StatsBlock.Columns[1].Visible = false;
                        StatsBlock.Columns[2].Visible = false;
                        StatsBlock.Columns[3].Visible = false;
                        break;
                    case null:
                        StatsBlock.Columns[0].Visible = false;
                        StatsBlock.Columns[1].Visible = false;
                        StatsBlock.Columns[2].Visible = false;
                        StatsBlock.Columns[3].Visible = false;
                        break;
                }

                //Fill DataTable
                //Only want one row of information.
                if (reader.HasRows)
                {
                    try
                    {
                        dt.Load(reader);
                    }
                    //Catch for invalid data pulled by Reader
                    catch (Exception e)
                    {
                    }
                }
            }
            //Catch for issuing connection string
            catch (Exception e)
            {
            }
            finally
            {
                StatsBlock.DataSource = dt;
                StatsBlock.DataBind();
                connect.Close();
            }
        }


        /*
        * On Click of an MCUID, simplify the total list of MCUs being displayed to just itself.
            * Sets page title and key/type for breaking out of the page.
            * */
        protected void mcuidClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            foreach (GridViewRow rw in resultsGrid.Rows)
            {
                if (((Button)rw.FindControl("MCUID")).Text == btn.Text)
                {
                    //Keep or mark matching items as visible
                    rw.Visible = true;
                    mcuidSelected = ((Button)rw.FindControl("MCUID")).Text;
                }
                else
                {
                    //Hide unrelated objects
                    rw.Visible = false;
                }
            }
            Page.Title = "MCU: " + btn.Text;
            keyBreak = btn.Text;
            typeBreak = "MCUID";
            generateStatsTable();
        }


        /*
            * On Click of a Room, simplify the total list of MCUs being displayed to just those within the room.
        * Sets page title and key/type for breaking out of the page.
        * */
        protected void roomClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            foreach (GridViewRow rw in resultsGrid.Rows)
            {
                if (((Button)rw.FindControl("Room")).Text == btn.Text)
                {
                    //Keep or mark matching items as visible
                    rw.Visible = true;
                    mcuidSelected = ((Button)rw.FindControl("MCUID")).Text;
                }
                else
                {
                    //Hide unrelated objects
                    rw.Visible = false;
                }
            }
            Page.Title = "Room: " + btn.Text;
            keyBreak = btn.Text;
            typeBreak = "Room";
            generateStatsTable();
        }

        /*
        * On Click of a Facility, simplify the total list of MCUs being displayed to just those within the facility.
            * Sets page title and key/type for breaking out of the page.
            * */
        protected void facilityClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            foreach (GridViewRow rw in resultsGrid.Rows)
            {
                if (((Button)rw.FindControl("Facility")).Text == btn.Text)
                {
                    //Keep or mark matching items as visible
                    rw.Visible = true;
                    mcuidSelected = ((Button)rw.FindControl("MCUID")).Text;
                }
                else
                {
                    //Hide unrelated objects
                    rw.Visible = false;
                }
            }
            Page.Title = "Facility: " + btn.Text;
            keyBreak = btn.Text;
            typeBreak = "Facility";
            generateStatsTable();
        }

        /*
        * On Click of a Group, simplify the total list of MCUs being displayed to just those within the group.
        * Sets page title and key/type for breaking out of the page.
        * */
        protected void groupClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            foreach (GridViewRow rw in resultsGrid.Rows)
            {
                if (((Button)rw.FindControl("Group")).Text == btn.Text)
                {
                    //Keep or mark matching items as visible
                    rw.Visible = true;
                    mcuidSelected = ((Button)rw.FindControl("MCUID")).Text;
                }
                else
                {
                    //Hide unrelated objects
                    rw.Visible = false;
                }
            }
            Page.Title = "Group: " + btn.Text;
            keyBreak = btn.Text;
            typeBreak = "Group";
            generateStatsTable();
        }

        /*
            * Resets the Visibility status of every MCU to true to "reset" the list.
            * Reset the page title and also clear the key/type used to break from the page
            * */
        protected void resetVisibility(object sender, EventArgs e)
        {
            foreach (GridViewRow rw in resultsGrid.Rows)
            {
                rw.Visible = true;
            }
            Page.Title = "All";
            keyBreak = null;
            typeBreak = null;
            mcuidSelected = null;
            generateStatsTable();
        }

        /*
            * Evaluate the type to ensure that one exists.  
            * Key is not currently checked.  It is technically possible, because of this, to edit
            * all MCUs that are not part of a group.
            * 
            * This functionality will be reevaluated later in the design to judge its usefulness.
            * 
            * If there is no type set, Postback will reset the page title. Ensure that it maintains uniformity.
            * We can also assume at this point that no MCU has been selected, so the list should not contain any
            * hidden MCUs.
            * */
        protected void editRemaining(object sender, EventArgs e)
        {
            if (typeBreak != null)
            {
                Response.Redirect("editpage.aspx?type=" + typeBreak + "&key=" + keyBreak);
            }
            else
            {
                Page.Title = "All";
            }
        }
    }
}