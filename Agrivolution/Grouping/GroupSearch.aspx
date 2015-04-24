﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GroupSearch.aspx.cs" Inherits="Agrivolution.WebForm1" %>

<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Groups</h2>
    <!--Dynamic grid view to display the list of all groups created in the system.-->
    <asp:GridView ID="GridViewCurrentGroups" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSourceGroupTable" Width="349px">
        <Columns>
            <asp:TemplateField HeaderText="GroupName" InsertVisible="False" SortExpression="GroupName">
                <EditItemTemplate>
                    <asp:Label ID="lblGroupName" runat="server" Text='<%# Eval("GroupName") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <!--Link to direct users to an individual group page. Also passes a query string.-->
                    <a href='SingleGroup.aspx?GroupName=<%# Eval("GroupName") %>'>View</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="GroupName" HeaderText="GroupName" SortExpression="GroupName" />
        </Columns>
    </asp:GridView>
    <!--Datasource to link with the datagrid to help populate the data table from the databgase-->
    <asp:SqlDataSource ID="SqlDataSourceGroupTable" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [GroupsMasterList]"></asp:SqlDataSource>
    <br>
    <asp:Button ID="btnCreateGroup" runat="server" Text="Create Group" OnClick="btnCreateGroup_Click" />
</asp:Content>
