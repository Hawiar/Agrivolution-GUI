<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditMCU.aspx.cs" Inherits="Agrivolution.EditMCU" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <h2>MCU Id:&nbsp;
            <asp:Label ID="MCULabel" runat="server"></asp:Label>
        </h2>
    <p>
        <!--displays relivent information about the MCU including:  ID#, Room, group, and pump status-->
        <asp:GridView ID="Attributes" runat="server" AutoGenerateColumns="False" DataSourceID="MCUAttributeSource">
            <Columns>
                <asp:BoundField DataField="Facility" HeaderText="Facility" SortExpression="Facility" />
                <asp:TemplateField HeaderText="Room">
                    <ItemTemplate>
                        <asp:Button DataField="Room" ID="Room" runat="server" Text='<%# Eval("Room") %>' OnClick="redirectToRoom" UseSubmitBehavior="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Group Name">
                    <ItemTemplate>
                        <asp:Button DataField="GroupName" ID="GroupName" runat="server" Text='<%# Eval("GroupName") %>' OnClick="redirectToGroup" UseSubmitBehavior="false" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:CheckBoxField DataField="PumpStatus" HeaderText="PumpStatus" SortExpression="PumpStatus"></asp:CheckBoxField>
            </Columns>
        </asp:GridView>
    </p>
    <p>
        <!--data sourse for the abouve gidview (Attributes)-->
        <asp:SqlDataSource ID="MCUAttributeSource" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [Room], [Facility], [GroupName], [PumpStatus] FROM [MCU] WHERE ([MCUId] = @MCUId)">
            <SelectParameters>
                <asp:QueryStringParameter Name="MCUId" QueryStringField="MCUId" Type="Int32" DefaultValue="" />
            </SelectParameters>
        </asp:SqlDataSource>
    </p>
    <p>
        <!--the remainder of these lables and text boxes will be set to invisible if there is a group assoiciated with the MCU-->
        <!--This is to prevent the user to change the functionality of an MCU if it is already specified by a group-->

        <!--user turns the fans on and off-->
        <asp:Label ID="fanLabel" runat="server" Text="Fan:  "></asp:Label>
        <asp:DropDownList ID="fanSwitch" runat="server" ToolTip="Turns the fan on or off for this MCU">
                <asp:ListItem Text="On" Value="1" />
                <asp:ListItem Text="Off" Value="0" />
            </asp:DropDownList>
        </p>
    <p>
        <asp:Label ID="lightCycleLable" runat="server" Text="Light Cycle:  "></asp:Label>
        </p>
        <p>
            <!--displays when the lights go on-->
            <asp:Label ID="startLabel" runat="server" Text="Start Time:  "></asp:Label>
            &nbsp;
        <asp:TextBox ID="startTime" runat="server" ToolTip="When the lights are set to turn ON" ReadOnly="true"></asp:TextBox>
    &nbsp;
            <!--displays when the lights go off-->
            <asp:Label ID="endLabel" runat="server" Text="End Time:  "></asp:Label>
            &nbsp;
            <asp:TextBox ID="endTime" runat="server" ToolTip="when the lights are set to turn OFF" ReadOnly="true"></asp:TextBox>
            </p>
        <p>
            <!--user selects the start time of the light timers, changing when they are turned on-->
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="startSelect" runat="server" ToolTip="choose when lights are turned ON">
                <asp:ListItem Text="12:00 am" Value="00:00:00" />
                <asp:ListItem Text="1:00 am" Value="01:00:00" />
                <asp:ListItem Text="2:00 am" Value="02:00:00" />
                <asp:ListItem Text="3:00 am" Value="03:00:00" />
                <asp:ListItem Text="4:00 am" Value="04:00:00" />
                <asp:ListItem Text="5:00 am" Value="05:00:00" />
                <asp:ListItem Text="6:00 am" Value="06:00:00" />
                <asp:ListItem Text="7:00 am" Value="07:00:00" />
                <asp:ListItem Text="8:00 am" Value="08:00:00" />
                <asp:ListItem Text="9:00 am" Value="09:00:00" />
                <asp:ListItem Text="10:00 am" Value="10:00:00" />
                <asp:ListItem Text="11:00 am" Value="11:00:00" />
                <asp:ListItem Text="12:00 pm" Value="12:00:00" />
                <asp:ListItem Text="1:00 pm" Value="13:00:00" />
                <asp:ListItem Text="2:00 pm" Value="14:00:00" />
                <asp:ListItem Text="3:00 pm" Value="15:00:00" />
                <asp:ListItem Text="4:00 pm" Value="16:00:00" />
                <asp:ListItem Text="5:00 pm" Value="17:00:00" />
                <asp:ListItem Text="6:00 pm" Value="18:00:00" />
                <asp:ListItem Text="7:00 pm" Value="19:00:00" />
                <asp:ListItem Text="8:00 pm" Value="20:00:00" />
                <asp:ListItem Text="9:00 pm" Value="21:00:00" />
                <asp:ListItem Text="10:00 pm" Value="22:00:00" />
                <asp:ListItem Text="11:00 pm" Value="23:00:00" />
            </asp:DropDownList>

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

            <!--user selects the end time of the light timers, changing when they are turned off-->
            <asp:DropDownList ID="endSelect" runat="server" ToolTip="choose when lights are turned Off">
                <asp:ListItem Text="12:00 am" Value="00:00:00" />
                <asp:ListItem Text="1:00 am" Value="01:00:00" />
                <asp:ListItem Text="2:00 am" Value="02:00:00" />
                <asp:ListItem Text="3:00 am" Value="03:00:00" />
                <asp:ListItem Text="4:00 am" Value="04:00:00" />
                <asp:ListItem Text="5:00 am" Value="05:00:00" />
                <asp:ListItem Text="6:00 am" Value="06:00:00" />
                <asp:ListItem Text="7:00 am" Value="07:00:00" />
                <asp:ListItem Text="8:00 am" Value="08:00:00" />
                <asp:ListItem Text="9:00 am" Value="09:00:00" />
                <asp:ListItem Text="10:00 am" Value="10:00:00" />
                <asp:ListItem Text="11:00 am" Value="11:00:00" />
                <asp:ListItem Text="12:00 pm" Value="12:00:00" />
                <asp:ListItem Text="1:00 pm" Value="13:00:00" />
                <asp:ListItem Text="2:00 pm" Value="14:00:00" />
                <asp:ListItem Text="3:00 pm" Value="15:00:00" />
                <asp:ListItem Text="4:00 pm" Value="16:00:00" />
                <asp:ListItem Text="5:00 pm" Value="17:00:00" />
                <asp:ListItem Text="6:00 pm" Value="18:00:00" />
                <asp:ListItem Text="7:00 pm" Value="19:00:00" />
                <asp:ListItem Text="8:00 pm" Value="20:00:00" />
                <asp:ListItem Text="9:00 pm" Value="21:00:00" />
                <asp:ListItem Text="10:00 pm" Value="22:00:00" />
                <asp:ListItem Text="11:00 pm" Value="23:00:00" />
            </asp:DropDownList>
            </p>
    <p>
        <!--user specifies the type of crop that is growing for this MCU-->
        <asp:Label ID="cropLabel" runat="server" Text="Crop Type:  "></asp:Label>
&nbsp;<asp:TextBox ID="cropType" runat="server" ToolTip="Specifies what type of crop is being grom hear"></asp:TextBox>
        </p>
    <p>
        &nbsp;</p>
    <p>
        <!--user saves changes to the database.  This is also invisible if a group is associated with this MCU-->
        <asp:Button ID="setMCUButon" runat="server" Text="apply changes" OnClick="setMCUValues" ToolTip="Applys and saves any changes you made"/>
        <asp:Label ID="functionalityUnavailable" runat="server" Text="This MCU belongs to a group.  It's functionality can only be changed on it's group page"></asp:Label>
    </p>
        <p>
            &nbsp;</p>
        <p>
            <!--redirects the user back to the controls page-->
        <asp:Button ID="returnButton" runat="server" OnClick="returnClick" Text="Return" ToolTip="This button will take you back to the main controls page"/>
    </p>
</asp:Content>
