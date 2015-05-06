<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditRoom.aspx.cs" Inherits="Agrivolution.EditRoomaspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <p>
        &nbsp;</p>
    <p>
    <p>
        temperature (degres F)&nbsp;&nbsp;    <!-- look up askii value of degree symble -->
        <asp:TextBox ID="temperatureValue" runat="server"></asp:TextBox>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Button ID="setRoom" runat="server" Text="apply changes" OnClick="setRoomValues"/>
    </p>
</asp:Content>
    