<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Controls.aspx.cs" Inherits="Agrivolution.MCUSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
        input[type="button"], input[type="button"]:focus, input[type="button"]:active,
        button, button:focus, button:active {
            margin: 0;
            padding: 0;
            background: none;
            border: none;
            display: inline;
            outline: none;
            outline-offset: 0;
            color: blue;
            cursor: pointer;
            text-decoration: underline;
        }
    </style>

    <h2><%: "Controls: " + Page.Title%></h2>
    <asp:Button ID="resetButton" runat="server" Text="Reset List" OnClick="resetVisibility" UseSubmitBehavior="false" />
    <asp:Button ID="goToEditButton" runat="server" Text="Edit MCUs" OnClick="editRemaining" UseSubmitBehavior="false" />

    <asp:GridView runat="server" AutoGenerateColumns="False" ID="resultsGrid" AllowPaging="True" AllowSorting="True" PageSize="20" DataKeyNames="MCUID" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:TemplateField HeaderText="MCUID" SortExpression="MCUID">
                <ItemTemplate>
                    <asp:Button DataField="MCUID" ID="MCUID" runat="server" Text='<%# Eval("MCUID") %>' OnClick="mcuidClick" UseSubmitBehavior="false" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Room" SortExpression="Room">
                <ItemTemplate>
                    <asp:Button DataField="Room" ID="Room" runat="server" Text='<%#Eval("Room") %>' OnClick="roomClick" UseSubmitBehavior="false" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Facility" SortExpression="Facility">
                <ItemTemplate>
                    <asp:Button DataField="Facility" ID="Facility" runat="server" Text='<%#Eval("Facility") %>' OnClick="facilityClick" UseSubmitBehavior="false" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Status" SortExpression="Status">
                <ItemTemplate>
                    <asp:Label ID="Status" runat="server" Text='<%#Eval("Status") %>' />
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Group" SortExpression="Group">
                <ItemTemplate>
                    <asp:Button DataField="Group" ID="Group" runat="server" Text='<%#Eval("Group") %>' OnClick="groupClick" UseSubmitBehavior="false" />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [MCUID], [Room], [Facility], [Group], [Status] FROM [MCUList]"></asp:SqlDataSource>


</asp:Content>
