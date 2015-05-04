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
        private static DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //Parse query strings
                typeBreak = Request.QueryString["type"];
                keyBreak = Request.QueryString["key"];
               
                //Builds views
                fillGroupDDL();
                fillFacilitiesDDL();
                fillRoomDDL();
                generateGridView();
                generateStatsTable();
                //Page Title handling
                if (keyBreak == null)
                {
                    Page.Title = "All";
                }
                else
                {
                    Page.Title = typeBreak + " : " + keyBreak;
                }
            }
            else
            {
            }
        }

        /**
         * Generates the main grid view.
         * Sortable and dynamic.  Each clickable cell responds and generates a new grid based on the input.
         * */
        protected void generateGridView()
        {
            dt = new DataTable();
            String connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connect = new SqlConnection(connString);
            SqlCommand com;

            if (keyBreak == null || typeBreak == null)
            {
                com = new SqlCommand("SELECT * FROM [dbo].[MCU] WHERE UserName = " + "'" + User.Identity.Name + "'", connect);
            }
            else
            {
                com = new SqlCommand("SELECT * FROM [dbo].[MCU] WHERE " + typeBreak + " = @Key AND UserName = " + "'" + User.Identity.Name + "'", connect);

                //MCUID is the only time where the Parameter may be an integer. Parse accordingly.
                if (typeBreak == "MCUId")
                {
                    com.Parameters.AddWithValue("@Key", Convert.ToInt32(keyBreak));
                }
                else
                {
                    com.Parameters.AddWithValue("@Key", keyBreak);
                }
            }

            try
            {
                //Generate the connection and query string to pull the table
                connect.Open();
                SqlDataReader reader = com.ExecuteReader();

                //Fill DataTable
                if (reader.HasRows)
                {
                    try
                    {
                        dt.Load(reader);
                    }
                    //Catch for invalid data pulled by Reader
                    catch (Exception e)
                    {
                        Console.Write(e.ToString());
                    }
                }
            }
            //Catch for issuing connection string
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
            finally
            {
                resultsGrid.DataSource = dt;
                resultsGrid.DataBind();
                connect.Close();
            }
        }

        /*
         * Supports the paging of the GridView
         * */
        protected void resultsGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            resultsGrid.PageIndex = e.NewPageIndex;
            resultsGrid.DataBind();
        }

        /**
         * Sorts the current viewed Grid.
         * Works ascending and descending. 
         * */
        protected void resultsGrid_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToString(ViewState["sort"]) == "Asc")
                {
                    dt.DefaultView.Sort = e.SortExpression + " Desc";
                    ViewState["sort"] = "Desc";
                }
                else
                {
                    dt.DefaultView.Sort = e.SortExpression + " Asc";
                    ViewState["sort"] = "Asc";
                }
                resultsGrid.DataSource = dt;
                resultsGrid.DataBind();

                //Page Title handling
                if (keyBreak == null)
                {
                    Page.Title = "All";
                }
                else
                {
                    Page.Title = typeBreak + " : " + keyBreak;
                }
            }
        }

        /*
            * Generates a collection of information (static MCU-related information) based on the key/type
            * 
            **/
        protected void generateStatsTable()
        {
            //Do not show
            if (keyBreak == null || typeBreak == null)
            {

            }
            else
            {
                DataTable dt = new DataTable();
                String connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection connect = new SqlConnection(connString);
                SqlCommand com;
                com = new SqlCommand("SELECT * FROM [dbo].[MCU] INNER JOIN [dbo].[MCUData] WHERE " + typeBreak + "= @Key AND UserName = " + "'" + User.Identity.Name + "'", connect);


                //MCUID is the only time where the Parameter may be an integer. Parse accordingly.
                if (typeBreak == "MCUId")
                {
                    com.Parameters.AddWithValue("@Key", Convert.ToInt32(keyBreak));
                }
                else
                {
                    com.Parameters.AddWithValue("@Key", keyBreak);
                }
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
                    switch (typeBreak)
                    {
                        case "GroupName":
                            StatsBlock.Columns[0].Visible = false;
                            StatsBlock.Columns[1].Visible = false;
                            StatsBlock.Columns[2].Visible = false;
                            StatsBlock.Columns[3].Visible = true;
                            break;
                        case "MCUId":
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
                            Console.Write(e.ToString());
                        }
                    }
                }
                //Catch for issuing connection string
                catch (Exception e)
                {
                    Console.Write(e.ToString());
                }
                finally
                {
                    StatsBlock.DataSource = dt;
                    StatsBlock.DataBind();
                    connect.Close();
                }
            }
        }


        /*
        * On Click of an MCUID, simplify the total list of MCUs being displayed to just itself.
            * Sets page title and key/type for breaking out of the page.
            * */
        protected void mcuidClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            keyBreak = btn.Text;
            typeBreak = "MCUId";
            Response.Redirect("~/Controls.aspx?type=" + typeBreak + "&key=" + keyBreak);
        }


        /*
            * On Click of a Room, simplify the total list of MCUs being displayed to just those within the room.
        * Sets page title and key/type for breaking out of the page.
        * */
        protected void roomClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            keyBreak = btn.Text;
            typeBreak = "Room";
            Response.Redirect("~/Controls.aspx?type=" + typeBreak + "&key=" + keyBreak);
        }

        /*
        * On Click of a Facility, simplify the total list of MCUs being displayed to just those within the facility.
            * Sets page title and key/type for breaking out of the page.
            * */
        protected void facilityClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            keyBreak = btn.Text;
            typeBreak = "Facility";
            Response.Redirect("~/Controls.aspx?type=" + typeBreak + "&key=" + keyBreak);
        }

        /*
        * On Click of a Group, simplify the total list of MCUs being displayed to just those within the group.
        * Sets page title and key/type for breaking out of the page.
        * */
        protected void groupClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            keyBreak = btn.Text;
            typeBreak = "GroupName";
            Response.Redirect("~/Controls.aspx?type=" + typeBreak + "&key=" + keyBreak);
        }

        /*
            * Resets the Visibility status of every MCU to true to "reset" the list.
            * Reset the page title and also clear the key/type used to break from the page
            * */
        protected void resetVisibility(object sender, EventArgs e)
        {
            Response.Redirect("~/Controls.aspx");
        }

        /*
            * Evaluate the type to ensure that one exists.  
            * Key is not currently checked.  It is technically possible, because of this, to edit
            * all MCUs that are not part of a group.
            * 
            * 
            * If there is no type set, nothing happens.
            * We can also assume at this point that no MCU has been selected, so the list should not contain any
            * hidden MCUs.
            * */
        protected void editRemaining(object sender, EventArgs e)
        {
            switch (typeBreak)
            {
                case "MCUId":
                    Response.Redirect("~/editpage.aspx?type=" + typeBreak + "&key=" + keyBreak);
                    break;
                case "GroupName":
                    Response.Redirect("~/Grouping/SingleGroup.aspx?GroupName=" + typeBreak + "&Msg=None");
                    break;
                case "Room":
                    Response.Redirect("~/editpage.aspx?type=" + typeBreak + "&key=" + keyBreak);
                    break;
                default:
                    break;
            }
        }

        /*
         * Connect to the database and pull all unique Facility names.
         * Build them into a dropdown list
         * */
        protected void fillFacilitiesDDL()
        {
            String connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connect = new SqlConnection(connString);
            SqlCommand com = new SqlCommand("SELECT DISTINCT Facility FROM [dbo].[MCU] WHERE UserName = " + "'" + User.Identity.Name + "'", connect);
            DataTable facilities = new DataTable();

            try
            {
                //Generate the connection and query string to pull the contents for the DDL
                connect.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(facilities);

                ddlFacility.DataSource = facilities;
                ddlFacility.DataTextField = "Facility";
                ddlFacility.DataValueField = "Facility";
                ddlFacility.DataBind();
            }
            //Catch for issuing connection string
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
            finally
            {
                //Set the initial item to label the DDL
                ddlFacility.Items.Insert(0, new ListItem("Facilities", null));
                connect.Close();
            }
        }

        /*
        * Connect to the database and pull all unique Room names.
        * Build them into a dropdown list
        * */
        protected void fillRoomDDL()
        {
            String connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connect = new SqlConnection(connString);
            SqlCommand com = new SqlCommand("SELECT DISTINCT Room FROM [dbo].[MCU] WHERE UserName = " + "'" + User.Identity.Name + "'", connect);
            DataTable room = new DataTable();

            try
            {
                //Generate the connection and query string to pull the contents for the DDL
                connect.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(room);

                ddlRoom.DataSource = room;
                ddlRoom.DataTextField = "Room";
                ddlRoom.DataValueField = "Room";
                ddlRoom.DataBind();
            }
            //Catch for issuing connection string
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
            finally
            {
                //Set the initial item to label the DDL
                ddlRoom.Items.Insert(0, new ListItem("Rooms", null));
                connect.Close();
            }
        }

        /*
        * Connect to the database and pull all unique Group names.
        * Build them into a dropdown list
        * */
        protected void fillGroupDDL()
        {
            String connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connect = new SqlConnection(connString);
            SqlCommand com = new SqlCommand("SELECT DISTINCT " + '"' + "GroupName" + '"' + " FROM [dbo].[MCU] WHERE UserName = " + "'" + User.Identity.Name + "'", connect);
            DataTable group = new DataTable();

            try
            {
                //Generate the connection and query string to pull the contents for the DDL
                connect.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(group);

                ddlGroup.DataSource = group;
                ddlGroup.DataTextField = "GroupName";
                ddlGroup.DataValueField = "GroupName";
                ddlGroup.DataBind();
            }
            //Catch for issuing connection string
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
            finally
            {
                //Set the initial item to label the DDL
                ddlGroup.Items.Insert(0, new ListItem("Group", null));
                connect.Close();
            }
        }

        /**
         * When an item is selected within the drop down list, reload the table to display the relevant rows.
         * Modifies by selection of a "Room"
         * */   
        protected void ddlRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            keyBreak = this.ddlRoom.SelectedValue;
            typeBreak = "Room";
            Response.Redirect("~/Controls.aspx?type=" + typeBreak + "&key=" + keyBreak);
        }

        /**
         * When an item is selected within the drop down list, reload the table to display the relevant rows.
         * Modifies by selection of a "Facility"
         * */
        protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            keyBreak = this.ddlFacility.SelectedValue;
            typeBreak = "Facility";
            Response.Redirect("~/Controls.aspx?type=" + typeBreak + "&key=" + keyBreak);
        }

        /**
         * When an item is selected within the drop down list, reload the table to display the relevant rows.
         * Modifies by selection of a "Group"
         * */
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            keyBreak = this.ddlGroup.SelectedValue;
            typeBreak = "GroupName";
            Response.Redirect("~/Controls.aspx?type=" + typeBreak + "&key=" + keyBreak);
        }
    }
}
