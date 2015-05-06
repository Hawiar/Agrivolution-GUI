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

    <asp:DropDownList ID="ddlFacility" runat="server" OnSelectedIndexChanged="ddlFacility_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
    <asp:DropDownList ID="ddlRoom" runat="server" OnSelectedIndexChanged="ddlRoom_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
    <asp:DropDownList ID="ddlGroup" runat="server" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>

    <br />
    <asp:Button ID="resetButton" runat="server" Text="Reset List" OnClick="resetVisibility" UseSubmitBehavior="false" />
    <asp:Button ID="goToEditButton" runat="server" Text="Edit MCUs" OnClick="editRemaining" UseSubmitBehavior="false" />

    <asp:GridView runat="server" AutoGenerateColumns="False" ID="resultsGrid" AllowPaging="True" OnPageIndexChanging="resultsGrid_PageIndexChanging" AutoPostBack="True" AllowSorting="True" OnSorting="resultsGrid_Sorting" PageSize="20">
        <Columns>
            <asp:TemplateField HeaderText="MCUId" SortExpression="MCUId">
                <ItemTemplate>
                    <asp:Button ID="MCUId" runat="server" Text='<%# Eval("MCUId") %>' OnClick="mcuidClick" UseSubmitBehavior="false" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Room" SortExpression="Room">
                <ItemTemplate>
                    <asp:Button ID="Room" runat="server" Text='<%#Eval("Room") %>' OnClick="roomClick" UseSubmitBehavior="false" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Facility" SortExpression="Facility">
                <ItemTemplate>
                    <asp:Button ID="Facility" runat="server" Text='<%#Eval("Facility") %>' OnClick="facilityClick" UseSubmitBehavior="false" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Group" SortExpression="GroupName">
                <ItemTemplate>
                    <asp:Button ID="GroupName" runat="server" Text='<%#Eval("GroupName") %>' OnClick="groupClick" UseSubmitBehavior="false" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Crop Type" SortExpression="CropType">
                <ItemTemplate>
                    <asp:Label ID="CropType" runat="server" Text='<%#Eval("CropType") %>' />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>
    <br />
    <asp:GridView ID="StatsBlock" runat="server" AutoGenerateColumns="False" AllowPaging="False" AllowSorting="False">
        <Columns>
            <asp:BoundField DataField="Humidity" runat="server" HeaderText="Humidity" Visible="true" />
            <asp:BoundField DataField="Temperature" runat="server" HeaderText="Temperature" Visible="true" />
            <asp:BoundField DataField="CO2" runat="server" HeaderText="CO2 Level" Visible="true" />
            <asp:BoundField DataField="LightStatus" runat="server" HeaderText="Light Status" Visible="true" />
            <asp:BoundField DataField="FanStatus" runat="server" HeaderText="Fan Status" Visible="true" />
            <asp:BoundField DataField="LightOn" runat="server" HeaderText="Light On Time" Visible="true" />
            <asp:BoundField DataField="LightOff" runat="server" HeaderText="Light Off Time" Visible="true" />
        </Columns>
    </asp:GridView>
</asp:Content>
