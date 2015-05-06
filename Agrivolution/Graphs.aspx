<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Graphs.aspx.cs" Inherits="Agrivolution.Graphs" %><%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        MCU Data</p>
    <p>
        <asp:HiddenField ID="username" runat="server" />
</p>
    <p>
        Select the MCU
        <asp:DropDownList ID="id" runat="server" DataSourceID="mcuid" DataTextField="MCUId" DataValueField="MCUId">
        </asp:DropDownList>
        <asp:SqlDataSource ID="mcuid" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT DISTINCT [MCUId] FROM [MCU] WHERE ([UserName] = @UserName)">
            <SelectParameters>
                <asp:ControlParameter ControlID="username" Name="UserName" PropertyName="Value" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
</p>
    <p>
        Select what data to display
        <asp:DropDownList ID="dataValue" runat="server" DataTextField="Temperature">
            <asp:ListItem>Temperature</asp:ListItem>
            <asp:ListItem>CO2</asp:ListItem>
            <asp:ListItem>Humidity</asp:ListItem>
        </asp:DropDownList>
</p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Button ID="displayGraph" runat="server" OnClick="displayGraph_Click" Text="Display Graph" />
</p>
    <p>
        <asp:Label ID="errorDisplay" runat="server" Text="No data to display!" Visible="False"></asp:Label>
</p>
    <p>
        <asp:Chart ID="graphData" runat="server" DataSourceID="mcudata2" Visible="False" Width="700px">
            <series>
                <asp:Series ChartType="Line" Name="Series1" XValueMember="Time">
                </asp:Series>
            </series>
            <chartareas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </chartareas>
        </asp:Chart>
</p>
    <p>
    <asp:SqlDataSource ID="mcudata2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [Temperature], [CO2], [Humidity], [Time] FROM [MCUData]"></asp:SqlDataSource>
</p>
</asp:Content>
