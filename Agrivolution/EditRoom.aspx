<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditRoom.aspx.cs" Inherits="Agrivolution.EditRoom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<h2>Room Name:&nbsp;&nbsp; 
            <asp:Label ID="roomLabel" runat="server"></asp:Label>
    </h2>
    <p>
        <!--displays all MCUs asssociated with the room and their relevent data--> 
        <asp:GridView ID="MCUs" runat="server" AutoGenerateColumns="False" DataKeyNames="MCUId" DataSourceID="MCUsINRoom">
            <Columns>
                <asp:TemplateField HeaderText="MCUId">
                    <ItemTemplate>
                        <asp:Button DataField="MCUId" ID="MCUId" runat="server" Text='<%# Eval("MCUId") %>' OnClick="redirectToMCU" UseSubmitBehavior="false" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Group Name">
                    <ItemTemplate>
                        <asp:Button DataField="GroupName" ID="GroupName" runat="server" Text='<%# Eval("GroupName") %>' OnClick="redirectToGroup" UseSubmitBehavior="false" />
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:CheckBoxField DataField="FanStatus" HeaderText="FanStatus" SortExpression="FanStatus"></asp:CheckBoxField>
                <asp:CheckBoxField DataField="LightStatus" HeaderText="LightStatus" SortExpression="LightStatus"></asp:CheckBoxField>
                <asp:CheckBoxField DataField="PumpStatus" HeaderText="PumpStatus" SortExpression="PumpStatus"></asp:CheckBoxField>
                <asp:BoundField DataField="CropType" HeaderText="CropType" SortExpression="CropType"></asp:BoundField>
                
            </Columns>
        </asp:GridView>
    </p>
    <p>
        <!--data souuce for the gridview abouve (MCUs)-->
        <asp:SqlDataSource ID="MCUsINRoom" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [MCUId], [GroupName], [FanStatus], [LightStatus], [PumpStatus], [CropType] FROM [MCU] WHERE ([Room] = @Room)">
            <SelectParameters>
                <asp:QueryStringParameter Name="Room" QueryStringField="Room" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <!--user imputs temperature-->
        Temperature:<asp:TextBox ID="temperature" runat="server" ToolTip="Adjusts the temperatur of the room"></asp:TextBox>
    </p>
        <p>
            <!--user imputs CO2-->
            CO2:<asp:TextBox ID="co2" runat="server" ToolTip="Adjusts the CO2 of the room">></asp:TextBox>
    </p>
        <p>
            <!--user imputs Humidity-->
            Humidity:<asp:TextBox ID="humidity" runat="server" ToolTip="Adjusts the humidity of the room">></asp:TextBox>
    </p>
        <p>
        Pumps:&nbsp;&nbsp;
            <!--user turns the pumps on or off-->
        <asp:DropDownList ID="pumpSwitch" runat="server" ToolTip="Turns the pumps on or off for this room">
                <asp:ListItem Text="On" Value="1" />
                <asp:ListItem Text="Off" Value="0" />
            </asp:DropDownList>
    </p>
    <p>
        &nbsp;</p>
        <p>
            <!--user saves changes to the database-->
        <asp:Button ID="setRoom" runat="server" Text="apply changes" OnClick="setRoomValues" ToolTip="Applys and saves any changes you made"/>
    </p>
        <p>
            &nbsp;</p>
    <p>
        <!--returns the user to the controls page-->
        <asp:Button ID="returnButton" runat="server" OnClick="returnClick" Text="Return" ToolTip="This button will take you back to the main controls page"/>
    </p>

    <!--displays Rooms that have no MCUs associated with them and allows the user to delete them-->
</asp:Content>
