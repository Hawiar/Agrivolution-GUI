<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditRoom.aspx.cs" Inherits="Agrivolution.EditRoom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<h2>Room Name:&nbsp;&nbsp; 
            <asp:Label ID="roomLabel" runat="server"></asp:Label>
    </h2>
    <p>
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
        <asp:SqlDataSource ID="MCUsINRoom" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [MCUId], [GroupName], [FanStatus], [LightStatus], [PumpStatus], [CropType] FROM [MCU] WHERE ([Room] = @Room)">
            <SelectParameters>
                <asp:QueryStringParameter Name="Room" QueryStringField="Room" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </p>
    <p>
        &nbsp;</p>
    <p>
        Temperature:<asp:TextBox ID="temperature" runat="server"></asp:TextBox>
    </p>
        <p>
            CO2:<asp:TextBox ID="co2" runat="server"></asp:TextBox>
    </p>
        <p>
            Humidity:<asp:TextBox ID="humidity" runat="server"></asp:TextBox>
    </p>
        <p>
        Pumps:&nbsp;&nbsp;
        <asp:DropDownList ID="pumpSwitch" runat="server">
                <asp:ListItem Text="On" Value="1" />
                <asp:ListItem Text="Off" Value="0" />
            </asp:DropDownList>
    </p>
    <p>
        &nbsp;</p>
        <p>
        <asp:Button ID="setRoom" runat="server" Text="apply changes" OnClick="setRoomValues"/>
    </p>
        <p>
            &nbsp;</p>
    <p>
        <asp:Button ID="returnButton" runat="server" OnClick="returnClick" Text="Return" />
    </p>
</asp:Content>
