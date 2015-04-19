using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;

namespace Agrivolution
{
    public partial class MCUSearch : System.Web.UI.Page
    {

        public static string keyBreak;
        public static string typeBreak;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //Default Page Title
                Page.Title = "All";
                keyBreak = null;
                typeBreak = null;
            }
            else
            {
                //Post-back Specific Stuff
            }
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

        protected void mcuidClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            foreach (GridViewRow rw in resultsGrid.Rows)
            {
                if (((Button)rw.FindControl("MCUID")).Text == btn.Text) 
                {
                    //Keep or mark matching items as visible
                    rw.Visible = true;
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
        }

        protected void roomClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            foreach (GridViewRow rw in resultsGrid.Rows)
            {
                if (((Button)rw.FindControl("Room")).Text == btn.Text)
                {
                    //Keep or mark matching items as visible
                    rw.Visible = true;
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
        }

        protected void facilityClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            foreach (GridViewRow rw in resultsGrid.Rows)
            {
                if (((Button)rw.FindControl("Facility")).Text == btn.Text)
                {
                    //Keep or mark matching items as visible
                    rw.Visible = true;
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
        }

        protected void groupClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            foreach (GridViewRow rw in resultsGrid.Rows)
            {
                if (((Button)rw.FindControl("Group")).Text == btn.Text)
                {
                    //Keep or mark matching items as visible
                    rw.Visible = true;
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
        }

        protected void resetVisibility(object sender, EventArgs e)
        {
            foreach (GridViewRow rw in resultsGrid.Rows)
            {
                rw.Visible = true; 
            }
            Page.Title = "All";
            keyBreak = null;
            typeBreak = null;
        }

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