using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace Agrivolution
{
    public partial class MCUSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Generates a dynamic page title
            this.Title = "Placeholder";
            string selectQuery = "SELECT * FROM ";

            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[4] 
            {
                new DataColumn("singleName", typeof(string)),
                new DataColumn("facilityName", typeof(string)),
                new DataColumn("roomName", typeof(string)),
                new DataColumn("groupName", typeof(string))
            });

            searchResultsGrid.DataSource = table;
            searchResultsGrid.DataBind();

            collectMCUTable(selectQuery, table);


        }
        //Current plan:  Upon page load from URL, initiate query. 
        //URL will have search term.  Clicking a link in master.aspx  (ie, Facilities, Groups)
        //will return a search result based on that.  Clicking on a Facility or Room within the page
        //triggers a new search to execute to display further information.  
        //Need to also include a way to break from the search page and return a list
        //That list will contain all of the MCU information.
        //The page that is broken to will perform edits on that specific information.  May include a flag that 
        //Mentions if the list contains a single MCU, a group, a Facility, or a Room.


        //Pulls in a query request (either group/facility/room name)
        //Returns a corresponding DataTable.
        //
        //DataTable will be used in two ways: Passed to the GridView,
        //And can be passed if the user submits that they wish to perform changes
        //on that specific collection of MCUs.
        //This current implementation may need to be examined later on.
        //SqlDataReader keeps the connection to the database open.
        //As such, the data may need to be pushed to a separate container first,
        //with the DataTable filled after the connections are closed.
        protected DataTable collectMCUTable(string selectQuery, DataTable table)
        {
            SqlConnection serverConnection = new SqlConnection("Data Source=SERVERNAME;Initial Catalog=POSSIBLE_SEPARATE_DATABASE_NAME;Persist Security Info=True;user id=USERIDFORDATABASE;pwd=PASSWORD");
            SqlDataReader reader = null;

            try
            {
                serverConnection.Open();
                SqlCommand executedCommand = new SqlCommand(selectQuery, serverConnection);
                reader = executedCommand.ExecuteReader();
            }
            catch (Exception) 
            {
                Console.Write(" (╯°□°）╯︵ ┻━┻) What happened?");
            }
            finally
            {
                if (reader != null)
                {
                    //Use the reader to build the DataTable.
                    while (reader.Read())
                    {
                        table.Load(reader);
                    }
                    reader.Close();
                }
                //Disconnect from the server
                if (serverConnection != null)
                {
                    serverConnection.Close();
                }
            }

            return table;
        }

        protected void searchResultsGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}