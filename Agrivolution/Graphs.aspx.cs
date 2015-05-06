using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Data.SqlClient;

namespace Agrivolution
{
    public partial class Graphs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            username.Value = User.Identity.Name;
        }

        protected void displayGraph_Click(object sender, EventArgs e)
        {
            //change the y value to the correct data input
            graphData.Series.FindByName("Series1").YValueMembers = dataValue.Text;
            string query = "SELECT [" + dataValue.Text + "], [Time] FROM [MCUData] WHERE [MCUId] = " + id.Text;

            //display the graph if it can be displayed
            try {
                mcudata2.SelectCommand = query;
                errorDisplay.Visible = false;
                graphData.Visible = true;
            }
            catch (Exception display)
            {
                errorDisplay.Visible = true;
            }
        }
    }
}