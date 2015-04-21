<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SingleGroup.aspx.cs" Inherits="Agrivolution.Grouping.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="MCUID" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="493px">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MCUID" SortExpression="MCUID">
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("MCUID") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblMcuId" runat="server" Text='<%# Bind("MCUID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Room" HeaderText="Room" SortExpression="Room" />
            <asp:BoundField DataField="Facility" HeaderText="Facility" SortExpression="Facility" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [MCUID], [Room], [Facility], [Group] FROM [MCUList] WHERE ([Group] IS NULL)"></asp:SqlDataSource>
    <div>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
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
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        <br />
        <br />
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="MCUID" DataSourceID="SqlDataSource2" Width="494px">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="ChkRemove" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MCUID" SortExpression="MCUID">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("MCUID") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblMCUId2" runat="server" Text='<%# Bind("MCUID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Group" HeaderText="Group" SortExpression="Group" />
                <asp:BoundField DataField="Room" HeaderText="Room" SortExpression="Room" />
                <asp:BoundField DataField="Facility" HeaderText="Facility" SortExpression="Facility" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [Group], [Room], [Facility], [MCUID] FROM [MCUList] WHERE ([Group] = @Group)">
            <SelectParameters>
                <asp:QueryStringParameter Name="Group" QueryStringField="GroupName" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <asp:Button ID="btnRemove" runat="server" OnClick="btnRemove_Click" Text="Remove MCUs" />
        <br />
    </div>

</asp:Content>
