<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GroupSearch.aspx.cs" Inherits="Agrivolution.WebForm1" %>

<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Groups</h2>
    <!--Dynamic grid view to display the list of all groups created in the system.-->
    <asp:GridView ID="GridViewCurrentGroups" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSourceGroupTable" Width="349px">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="ChkRemove" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="View" InsertVisible="False" SortExpression="GroupName">
                <EditItemTemplate>
                    <asp:Label ID="lblGroupName" runat="server" Text='<%# Eval("GroupName") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <!--Link to direct users to an individual group page. Also passes a query string.-->
                    <a href='SingleGroup.aspx?GroupName=<%# Eval("GroupName") %>&Msg=None'>View</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="GroupName">
                <ItemTemplate>
                    <asp:Label ID="lblGroupName" runat="server" Text='<%# Bind("GroupName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <!--Datasource to link with the datagrid to help populate the data table from the databgase-->
    <asp:HiddenField runat="server" ID="UseName"/>
    <asp:SqlDataSource ID="SqlDataSourceGroupTable" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [GroupsMasterList] WHERE UserName = @UserName">
        <SelectParameters>
            <asp:ControlParameter ControlID="UseName" Name="UserName" Type="String" DefaultValue="Anonymous" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br>
    <asp:Button ID="btnRemove" runat="server" OnClick="btnRemove_Click" Text="Remove Group" Width="131px" />
    <asp:Button ID="btnCreateGroup" runat="server" Text="Create Group" OnClick="btnCreateGroup_Click" />
</asp:Content>
