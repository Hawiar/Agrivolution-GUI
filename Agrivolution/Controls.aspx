<%@ Page Title=""        Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Controls.aspx.cs" Inherits="Agrivolution.MCUSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%: Title %></h2>

    <asp:GridView runat="server" AutoGenerateColumns="false" ID="searchResultsGrid" AllowPaging="true" PageSize="30" OnPageIndexChanging="searchResultsGrid_PageIndexChanging">
        <Columns>
      
                    <asp:BoundField DataField="singleName" runat="server" HeaderStyle-Width="150px"  HeaderText="MCU" />
                    <asp:BoundField HeaderStyle-Width="150px" HeaderText="Facility" DataField="facilityName"  runat="server" />
                    <asp:BoundField DataField="roomName" runat="server" HeaderStyle-Width="150px" HeaderText="Room" />
                    <asp:BoundField DataField="groupName" runat="server" HeaderStyle-Width="150px" HeaderText="Group" />
        </Columns>
    </asp:GridView>
</asp:Content>
