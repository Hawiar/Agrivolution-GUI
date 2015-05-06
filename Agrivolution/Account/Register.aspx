<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Agrivolution.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2> Register </h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Create a new account</h4>
        <h5>All fields denoted with an asterisk (*) are required</h5>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />

        <%--EMAIL - required
            The user enters an email that also acts as their username. The TextMode="Email" checks for proper email format
            This ensures uniqueness of users. --%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email*</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                    CssClass="text-danger" ErrorMessage="The email field is required." />
            </div>
        </div>

        <%--PASSWORD - required
            Password Requirements = Minimum of 6 characters, one uppercase letter, one lowercase letter,
                                    One Digit, One Special Character
             --%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password*</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    CssClass="text-danger" ErrorMessage="The password field is required." />
            </div>
        </div>

        <%--CONFIRM PASSWORD - required
             --%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Confirm password*</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
            </div>
        </div>

        <%--FIRST NAME - required
             --%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="FirstName" CssClass="col-md-2 control-label">First Name*</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="FirstName" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="FirstName"
                    CssClass="text-danger" ErrorMessage="The First Name field is required." />
            </div>
        </div>

        <%--LAST NAME - required
             --%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="LastName" CssClass="col-md-2 control-label">Last Name*</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="LastName" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="LastName"
                    CssClass="text-danger" ErrorMessage="The Last Name field is required." />
            </div>
        </div>

        <%--ADDRESS LINE 1 - required
             User's street address --%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Address1" CssClass="col-md-2 control-label">Address Line 1*</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Address1" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Address1"
                    CssClass="text-danger" ErrorMessage="The Address Line 1 field is required." />
            </div>
        </div>

        <%--ADDRESS LINE 2 - optional
             Additional space for longer addresses --%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Address2" CssClass="col-md-2 control-label">Address Line 2</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Address2" CssClass="form-control" />
            </div>
        </div>

        <%--CITY - required
             User enters their city, there are no checks to verify if their city exists.--%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="City" CssClass="col-md-2 control-label">City*</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="City" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="City"
                    CssClass="text-danger" ErrorMessage="The City field is required." />
            </div>
        </div>

        <%--STATE - required
            User selects their state from the alphabetically organized drop down list. --%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="State" CssClass="col-md-2 control-label">State*</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="State" runat="server">
                <asp:ListItem Value="NA">Select A State</asp:ListItem>
	            <asp:ListItem Value="AL">Alabama</asp:ListItem>
	            <asp:ListItem Value="AK">Alaska</asp:ListItem>
	            <asp:ListItem Value="AZ">Arizona</asp:ListItem>
	            <asp:ListItem Value="AR">Arkansas</asp:ListItem>
	            <asp:ListItem Value="CA">California</asp:ListItem>
	            <asp:ListItem Value="CO">Colorado</asp:ListItem>
	            <asp:ListItem Value="CT">Connecticut</asp:ListItem>
	            <asp:ListItem Value="DC">District of Columbia</asp:ListItem>
	            <asp:ListItem Value="DE">Delaware</asp:ListItem>
	            <asp:ListItem Value="FL">Florida</asp:ListItem>
	            <asp:ListItem Value="GA">Georgia</asp:ListItem>
	            <asp:ListItem Value="HI">Hawaii</asp:ListItem>
	            <asp:ListItem Value="ID">Idaho</asp:ListItem>
	            <asp:ListItem Value="IL">Illinois</asp:ListItem>
	            <asp:ListItem Value="IN">Indiana</asp:ListItem>
	            <asp:ListItem Value="IA">Iowa</asp:ListItem>
	            <asp:ListItem Value="KS">Kansas</asp:ListItem>
	            <asp:ListItem Value="KY">Kentucky</asp:ListItem>
	            <asp:ListItem Value="LA">Louisiana</asp:ListItem>
	            <asp:ListItem Value="ME">Maine</asp:ListItem>
	            <asp:ListItem Value="MD">Maryland</asp:ListItem>
	            <asp:ListItem Value="MA">Massachusetts</asp:ListItem>
                <asp:ListItem Value="MI">Michigan</asp:ListItem>
	            <asp:ListItem Value="MN">Minnesota</asp:ListItem>
	            <asp:ListItem Value="MS">Mississippi</asp:ListItem>
	            <asp:ListItem Value="MO">Missouri</asp:ListItem>
            	<asp:ListItem Value="MT">Montana</asp:ListItem>
            	<asp:ListItem Value="NE">Nebraska</asp:ListItem>
            	<asp:ListItem Value="NV">Nevada</asp:ListItem>
            	<asp:ListItem Value="NH">New Hampshire</asp:ListItem>
            	<asp:ListItem Value="NJ">New Jersey</asp:ListItem>
            	<asp:ListItem Value="NM">New Mexico</asp:ListItem>
            	<asp:ListItem Value="NY">New York</asp:ListItem>
            	<asp:ListItem Value="NC">North Carolina</asp:ListItem>
            	<asp:ListItem Value="ND">North Dakota</asp:ListItem>
            	<asp:ListItem Value="OH">Ohio</asp:ListItem>
            	<asp:ListItem Value="OK">Oklahoma</asp:ListItem>
            	<asp:ListItem Value="OR">Oregon</asp:ListItem>
            	<asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
            	<asp:ListItem Value="RI">Rhode Island</asp:ListItem>
            	<asp:ListItem Value="SC">South Carolina</asp:ListItem>
	            <asp:ListItem Value="SD">South Dakota</asp:ListItem>
	            <asp:ListItem Value="TN">Tennessee</asp:ListItem>
            	<asp:ListItem Value="TX">Texas</asp:ListItem>
            	<asp:ListItem Value="UT">Utah</asp:ListItem>
            	<asp:ListItem Value="VT">Vermont</asp:ListItem>
	            <asp:ListItem Value="VA">Virginia</asp:ListItem>
            	<asp:ListItem Value="WA">Washington</asp:ListItem>
            	<asp:ListItem Value="WV">West Virginia</asp:ListItem>
            	<asp:ListItem Value="WI">Wisconsin</asp:ListItem>
            	<asp:ListItem Value="WY">Wyoming</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="State"
             CssClass="text-danger" ErrorMessage="The State field is required." />
          </div>
        </div>

       <%--TIME ZONE - required
           User selects their time zone from a dropdown list of available United States Time Zones.  --%>
       <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="TimeZone" CssClass="col-md-2 control-label">Time Zone*</asp:Label>
              <div class="col-md-10">
                <asp:DropDownList ID="TimeZone" runat="server">
                  <asp:ListItem Value="NA">Select Your Time Zone</asp:ListItem>
	              <asp:ListItem Value="PST">Pacific Standard Time (PST) </asp:ListItem>
	              <asp:ListItem Value="MST">Mountain Standard Time (MST) </asp:ListItem>
	              <asp:ListItem Value="CST">Central Standard Time (CST) </asp:ListItem>
	              <asp:ListItem Value="EST">Eastern Standard Time (EST) </asp:ListItem>
	              <asp:ListItem Value="AKST">Alaskan Standard Time (AKST) </asp:ListItem>
                  <asp:ListItem Value="HST">Hawaii-Aleutian Standard Time (HAST) </asp:ListItem>
                </asp:DropDownList>
             <asp:RequiredFieldValidator runat="server" ControlToValidate="TimeZone"
             CssClass="text-danger" ErrorMessage="The Time Zone field is required." />
          </div>
        </div>

        <%--ZIP CODE - required
            Validates that a user's zip code is 5 digits long, as is standard in the United States. --%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Zip" CssClass="col-md-2 control-label">Postal/Zip Code*</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Zip" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Zip"
                    CssClass="text-danger" ErrorMessage="The Postal/Zip Code field is required." />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ErrorMessage="Invalid zip code." ControlToValidate="Zip"
                    ValidationExpression="[0-9]{5}"
                 />
            </div>
        </div>

        <%--PHONE NUMBER - required
            User is required to enter 10-digit number representing their phone number.--%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Phone" CssClass="col-md-2 control-label">Phone Number*</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Phone" CssClass="form-control"  />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Phone"
                    CssClass="text-danger" ErrorMessage="The Phone Number field is required." />
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorPhone" runat="server" 
                    ErrorMessage="Invalid phone number." ControlToValidate="Phone" ValidationExpression="[0-9]{10}"
                    />
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>
