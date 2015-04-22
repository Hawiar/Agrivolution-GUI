<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewGroup.aspx.cs" Inherits="Agrivolution.Grouping.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <p>
        </p>
        <p>
            <asp:Label ID="lblGroupName" runat="server" Text="Group Name: "></asp:Label>
            <asp:TextBox ID="txtGroupName" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="lblFan" runat="server" Text="Fan On/Off: "></asp:Label>
            <asp:DropDownList ID="ddlFan" runat="server">
                <asp:ListItem Text="On" Value="1" />
                <asp:ListItem Text="Off" Value="0" />
            </asp:DropDownList>
        </p>
        <p>
            <asp:Label ID="lblLightTimer" runat="server" Text="Lighting Cycle(Hours): "></asp:Label>
            <asp:TextBox ID="txtLightTimer" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="btnSaveGroup" runat="server" Text="Save Grouping" OnClick="btnSaveGroup_Click" />
        </p>
    </div>
</asp:Content>
