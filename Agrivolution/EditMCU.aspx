<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditMCU.aspx.cs" Inherits="Agrivolution.EditMCU" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        Fan&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;  
        <asp:DropDownList ID="fanSwitch" runat="server">
                <asp:ListItem Text="On" Value="1" />
                <asp:ListItem Text="Off" Value="0" />
            </asp:DropDownList>
    </p>
    <p>
        Pumps:&nbsp;&nbsp;
        <asp:DropDownList ID="pumpSwitch" runat="server">
                <asp:ListItem Text="On" Value="1" />
                <asp:ListItem Text="Off" Value="0" />
            </asp:DropDownList>
    </p>
    <p>
        light cycle (hours)&nbsp;&nbsp;
        <asp:TextBox ID="lightTimerValue" runat="server"></asp:TextBox>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Button ID="setMCU" runat="server" Text="apply changes" OnClick="setMCUValues"/>
    </p>
</asp:Content>
