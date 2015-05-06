<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddMCU.aspx.cs" Inherits="Agrivolution.AddMCU" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p style="font-size: large">
        <strong>Add a Micro Control Unit</strong></p>
    <p>
        <asp:HiddenField ID="UserID" runat="server" />
    </p>
    <p style="font-family: Arial; font-size: medium">
        <asp:Label ID="mcuIdLabel" runat="server" Text="MCU ID Number"></asp:Label>
        <asp:RequiredFieldValidator ID="IdRequire" runat="server" ControlToValidate="ID_text" Display="Dynamic" ErrorMessage="MCU Id is required" Font-Bold="True" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
        &nbsp;&nbsp;&nbsp;<asp:CustomValidator ID="IdFormatCheck" runat="server" ControlToValidate="ID_text" Display="Dynamic" ErrorMessage="MCU Id must be a number" OnServerValidate="IdFormatCheck_ServerValidate" SetFocusOnError="True" ForeColor="Red">Invalid number.</asp:CustomValidator>
&nbsp;<asp:TextBox ID="ID_text" runat="server" Height="24px" Width="193px"></asp:TextBox>
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;<asp:Label ID="facilityLabel" runat="server" Text="Facility"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="facilityList" runat="server" DataSourceID="Facility" DataTextField="Facility" DataValueField="Facility" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="facilityList_SelectedIndexChanged">
        <asp:ListItem Text="New Facility" Value="New Facility"></asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="newFacility" runat="server" AutoPostBack="True" Visible="False"></asp:TextBox>
    </p>
    <p>
        <asp:SqlDataSource ID="Facility" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT DISTINCT [Facility] FROM [MCU] WHERE ([UserName] = @UserName)">
            <SelectParameters>
                <asp:ControlParameter ControlID="UserID" Name="UserName" PropertyName="Value" Type="String" />
            </SelectParameters>
            
        </asp:SqlDataSource>
&nbsp;&nbsp;&nbsp;&nbsp;
    </p>
    <p>
        &nbsp;
        <asp:Label ID="roomLabel" runat="server" Text="Room"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="roomList" runat="server" DataSourceID="Room" DataTextField="Room" DataValueField="Room" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="roomList_SelectedIndexChanged">
        <asp:ListItem Text="New Room" Value="New Room"></asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="newRoom" runat="server" AutoPostBack="True" Visible="False"></asp:TextBox>
        <asp:SqlDataSource ID="Room" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT DISTINCT [Room] FROM [MCU] WHERE ([UserName] = @UserName)">
            <SelectParameters>
                <asp:ControlParameter ControlID="UserID" Name="UserName" PropertyName="Value" Type="String" />
            </SelectParameters>

        </asp:SqlDataSource>
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;
        <asp:Label ID="groupLabel" runat="server" Text="Group"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="groupList" runat="server" DataSourceID="Group" DataTextField="GroupName" DataValueField="GroupName" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="groupList_SelectedIndexChanged">
        <asp:ListItem Text="New Group" Value="New Group"></asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="newGroup" runat="server" AutoPostBack="True" Visible="False"></asp:TextBox>
        <asp:SqlDataSource ID="Group" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT DISTINCT [GroupName] FROM [MCU] WHERE ([UserName] = @UserName)">
            <SelectParameters>
                <asp:ControlParameter ControlID="UserID" Name="UserName" PropertyName="Value" Type="String" />
            </SelectParameters>
            
        </asp:SqlDataSource>
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;
        <asp:Label ID="CropTypeLabel" runat="server" Text="Crop Type"></asp:Label>
        <asp:RequiredFieldValidator ID="cropRequired" runat="server" ControlToValidate="CropType" Display="Dynamic" ErrorMessage="Crop type is required." Font-Bold="True" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="CropType" runat="server" Height="17px" Width="141px"></asp:TextBox>
    </p>
    <p>
        &nbsp;</p>
    <p>
&nbsp;&nbsp;
        <asp:Label ID="LOnLabel" runat="server" Text="Lights on at:"></asp:Label>
        <asp:RequiredFieldValidator ID="lightOnRequired" runat="server" ControlToValidate="LOn" Display="Dynamic" ErrorMessage="This field is required." Font-Bold="True" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="LOn" runat="server">HH:MM:SS</asp:TextBox>
        <asp:CheckBox ID="timeOfDayLightOn" runat="server" Text="PM" />
        <asp:RegularExpressionValidator ID="timeOnFormatCheck" runat="server" ControlToValidate="LOn" Display="Dynamic" ErrorMessage="Time format must be in hh:mm:ss form." ForeColor="Red" SetFocusOnError="True" ValidationExpression="([0-1][0-9]|[2][0-3]):[0-5][0-9]:[0-5][0-9]"></asp:RegularExpressionValidator>
    </p>
    <p>
        &nbsp;&nbsp;&nbsp;
        <asp:Label ID="LOffLabel" runat="server" Text="Lights off at:"></asp:Label>
        <asp:RequiredFieldValidator ID="lightOffRequired" runat="server" ControlToValidate="LOff" Display="Dynamic" ErrorMessage="This field is required." Font-Bold="True" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="LOff" runat="server">HH:MM:SS</asp:TextBox>
        <asp:CheckBox ID="timeOfDayLightOff" runat="server" Text="PM" />
        <asp:RegularExpressionValidator ID="timeOffFormatCheck0" runat="server" ControlToValidate="LOff" Display="Dynamic" ErrorMessage="Time format must be in hh:mm:ss form." ForeColor="Red" SetFocusOnError="True" ValidationExpression="([0-1][0-9]|[2][0-3]):[0-5][0-9]:[0-5][0-9]"></asp:RegularExpressionValidator>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Label ID="messageLabel" runat="server"></asp:Label>
    </p>
    <p>
        <asp:Button ID="addButton" runat="server" Text="Add MCU" OnClick="CreateMCU" />
    </p>
</asp:Content>
