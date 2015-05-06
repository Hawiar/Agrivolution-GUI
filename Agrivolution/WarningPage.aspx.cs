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
using System.Windows;
using System.Drawing;
using System.Net.Mail;

namespace Agrivolution
{
    public partial class WarningPage : System.Web.UI.Page
    {

        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs a)
        {
            if (IsPostBack == true)
            {

            }
            else
            {
                clearWarningsTable();
                populateWarningsTable();
                emailWarningMessage();

                int numberOfWarnings = getNumberOfWarnings();
                if (numberOfWarnings > 0)
                {
                    btnRemove.Visible = true;
                }
                if (numberOfWarnings == 0)
                {
                    LiteralControl noWarningsMessage = new LiteralControl("There are currently no warnings!");
                    PlaceHolder1.Controls.Add(noWarningsMessage);
                }
            }
        }

        // Finds MCUs that are experiencing inclimate temperature, humidity levels, or
        // carbon dioxide levels, as well as any faulty lights, then logs the MCU's ID to
        // the Warnings table with an Error ID later used to generate more descriptive
        // error messages.
        // The restriction values will need to be updated.
        protected void populateWarningsTable() 
        {
            try
            {
                SqlConnection connect = new SqlConnection(connString);
                SqlConnection connect2 = new SqlConnection(connString);
                {
                    connect.Open();
                    connect2.Open();
                    SqlCommand readCommand = new SqlCommand("Select * from MCUData", connect2);
                    SqlDataReader read = readCommand.ExecuteReader();
                    
                    while (read.Read())
                    {
                        int MCUId = (int)read["MCUId"];
                        int errorId;
                        double temperature = Convert.ToDouble(read["Temperature"]);
                        double CO2Level = Convert.ToDouble(read["CO2"]);
                        double humidityLevel = Convert.ToDouble(read["Humidity"]);
                        SqlCommand command;
                        if (temperature > 75.0)
                        {
                            errorId = 0;
                            command = new SqlCommand("Insert into Warnings(MCUId, ErrorId, Message, TimeStamp) values (@MCUId, @ErrorId, @Message, @TimeStamp)", connect);
                            command.Parameters.AddWithValue("@MCUId", MCUId);
                            command.Parameters.AddWithValue("@ErrorId", errorId);
                            command.Parameters.AddWithValue("@Message", createWarningMessage(MCUId, errorId));
                            command.Parameters.AddWithValue("@TimeStamp", DateTime.Now.ToString("hh:mm"));
                            command.ExecuteNonQuery();
                        }
                        if (temperature < 25.0)
                        {
                            errorId = 1;
                            command = new SqlCommand("Insert into Warnings(MCUId, ErrorId, Message, TimeStamp) values (@MCUId, @ErrorId, @Message, @TimeStamp)", connect); 
                            command.Parameters.AddWithValue("@MCUId", MCUId);
                            command.Parameters.AddWithValue("@ErrorId", errorId);
                            command.Parameters.AddWithValue("@Message", createWarningMessage(MCUId, errorId));
                            command.Parameters.AddWithValue("@TimeStamp", DateTime.Now.ToString("hh:mm"));
                            command.ExecuteNonQuery();
                        }
                        if (CO2Level > 75.0) 
                        {
                            errorId = 2;
                            command = new SqlCommand("Insert into Warnings(MCUId, ErrorId, Message, TimeStamp) values (@MCUId, @ErrorId, @Message, @TimeStamp)", connect); 
                            command.Parameters.AddWithValue("@MCUId", MCUId);
                            command.Parameters.AddWithValue("@ErrorId", errorId);
                            command.Parameters.AddWithValue("@Message", createWarningMessage(MCUId, errorId));
                            command.Parameters.AddWithValue("@TimeStamp", DateTime.Now.ToString("hh:mm"));
                            command.ExecuteNonQuery();
                        }
                        if (CO2Level < 25.0)
                        {
                            errorId = 3;
                            command = new SqlCommand("Insert into Warnings(MCUId, ErrorId, Message, TimeStamp) values (@MCUId, @ErrorId, @Message, @TimeStamp)", connect); 
                            command.Parameters.AddWithValue("@MCUId", MCUId);
                            command.Parameters.AddWithValue("@ErrorId", errorId);
                            command.Parameters.AddWithValue("@Message", createWarningMessage(MCUId, errorId));
                            command.Parameters.AddWithValue("@TimeStamp", DateTime.Now.ToString("hh:mm"));
                            command.ExecuteNonQuery();
                        }
                        if (humidityLevel > 75.0)
                        {
                            errorId = 4;
                            command = new SqlCommand("Insert into Warnings(MCUId, ErrorId, Message, TimeStamp) values (@MCUId, @ErrorId, @Message, @TimeStamp)", connect); 
                            command.Parameters.AddWithValue("@MCUId", MCUId);
                            command.Parameters.AddWithValue("@ErrorId", errorId);
                            command.Parameters.AddWithValue("@Message", createWarningMessage(MCUId, errorId));
                            command.Parameters.AddWithValue("@TimeStamp", DateTime.Now.ToString("hh:mm"));
                            command.ExecuteNonQuery();
                        }
                        if (humidityLevel < 25.0)
                        {
                            errorId = 5;
                            command = new SqlCommand("Insert into Warnings(MCUId, ErrorId, Message, TimeStamp) values (@MCUId, @ErrorId, @Message, @TimeStamp)", connect); 
                            command.Parameters.AddWithValue("@MCUId", MCUId);
                            command.Parameters.AddWithValue("@ErrorId", errorId);
                            command.Parameters.AddWithValue("@Message", createWarningMessage(MCUId, errorId));
                            command.Parameters.AddWithValue("@TimeStamp", DateTime.Now.ToString("hh:mm"));
                            command.ExecuteNonQuery();
                        }
                    }
                    read.Close();
                    connect.Close();
                }
            }
            catch (SqlException e)
            {

                Console.Write(e.ToString());
                throw e;
            }
        }

        // Generates an warning message based on the MCU experiencing the problem and the
        // type of warning.
        protected String createWarningMessage(int MCUId, int ErrorId)
        {
            String warningMessage = "MCU " + MCUId + " ";
            if (ErrorId == 0)
                return warningMessage += "detects a temperature that is too high.";
            if (ErrorId == 1)
                return warningMessage += "detects a temperature that is too low.";
            if (ErrorId == 2)
                return warningMessage += "detects a carbon dioxide level that is too high.";
            if (ErrorId == 3)
                return warningMessage += "detects a carbon dioxide level that is too low.";
            if (ErrorId == 4)
                return warningMessage += "detects a humidity level that is too high.";
            if (ErrorId == 5)
                return warningMessage += "detects a humidity level that is too low.";
            return "";
        }

        // Sends an error message directly to the user's email address.
        // The From field for the email needs to be filled in and the actual send command
        // needs to be uncommented after a web service is set up.
        protected void emailWarningMessage()
        {
            try
            {
            SqlConnection connect = new SqlConnection(connString);
            ArrayList allErrors = new ArrayList();
            String userName = User.Identity.Name;
            
                connect.Open();
                SqlCommand command = new SqlCommand("Select * from Warnings", connect);
                SqlDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    int MCUId = (int) read["MCUId"];
                    int ErrorId = (int) read["ErrorId"];
                    String errorMessage = createWarningMessage(MCUId, ErrorId);
                    allErrors.Add(errorMessage);
                }
                read.Close();
                MailAddress toAddress = new MailAddress(userName);
                MailMessage mail = new MailMessage();
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Host = "smtp.gmail.com";
                mail.To.Add(toAddress);
                //mail.From = new MailAddress("");
                mail.Subject = "Agrivolution Warnings";
                mail.Body = allErrors.ToString();
                //client.Send(mail);
                connect.Close();
            }
            catch (SqlException e)
            {
                Console.Write(e.ToString());
                throw e;
            }
        }

        

        // removes all values from the Warnings database table
        protected void clearWarningsTable()
        {
            try
            {
                SqlConnection connect = new SqlConnection(connString);
                connect.Open();
                SqlCommand command = new SqlCommand("truncate table Warnings", connect);
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.Write(e.ToString());
                throw e;
            }
        }

        // Obtains and returns the total number of warnings currently in the Warnings Table
        protected int getNumberOfWarnings()
        {
            int numberOfWarnings;
            try
            {
                SqlConnection connect = new SqlConnection(connString);
                connect.Open();
                SqlCommand command = new SqlCommand("select count (*) from Warnings", connect);
                numberOfWarnings = (int)command.ExecuteScalar();
                return numberOfWarnings;
            }
            catch (SqlException e)
            {
                Console.Write(e.ToString());
                throw e;
            }
        }

        protected void btnRemove_Click(object sender, EventArgs a)
        {
            //Loops through each row in the grid to verify if check box has been selected.
            for (int i = 0; i < WarningsGrid.Rows.Count; i++)
            {
                //Remove checked off rows.
                if (((CheckBox)WarningsGrid.Rows[i].FindControl("CheckRemove")).Checked)
                {
                    try
                    {
                        SqlConnection connect = new SqlConnection(connString);
                        {
                            String message = WarningsGrid.Rows[i].Cells[2].Text.ToString();
                            String timeStamp = WarningsGrid.Rows[i].Cells[3].Text.ToString();
                            connect.Open();
                            SqlCommand command = new SqlCommand("delete from Warnings where Message=@Message and TimeStamp=@TimeStamp", connect);
                            command.Parameters.AddWithValue("@Message", message);
                            command.Parameters.AddWithValue("@TimeStamp", timeStamp);
                            command.ExecuteNonQuery();
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
            WarningsGrid.DataBind();
        }
    }
}