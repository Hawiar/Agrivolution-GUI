<%@ Page Title=""        Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Controls.aspx.cs" Inherits="Agrivolution.MCUSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%: Title %></h2>

    <asp:GridView ID="searchResultsGrid" runat="server" AutoGenerateColumns="false" DataSourceID="DATABASENAME" AllowPaging="true">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:BoundField ID="singleName" HeaderText="MCU" runat="server" Enabled="true" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:BoundField ID="facilityName" HeaderText="Facility" runat="server" Enabled="true" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:BoundField ID="roomName" runat="server" Enabled="true" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:BoundField ID="groupName" runat="server" Enabled="true" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
