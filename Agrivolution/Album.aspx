<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Album.aspx.cs" Inherits="Agrivolution.Album" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    Your Albums<br />
    <asp:HiddenField ID="uName" runat="server" />
    <br />
    <br />
    Select the MCU
    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="mcuid" DataTextField="MCUId" DataValueField="MCUId">
    </asp:DropDownList>
    <asp:SqlDataSource ID="mcuid" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT DISTINCT [MCUId] FROM [MCU] WHERE ([UserName] = @UserName)">
        <SelectParameters>
            <asp:ControlParameter ControlID="uName" Name="UserName" PropertyName="Value" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <br />
</asp:Content>
