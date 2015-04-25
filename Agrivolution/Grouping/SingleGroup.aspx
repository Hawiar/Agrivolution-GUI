<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SingleGroup.aspx.cs" Inherits="Agrivolution.Grouping.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!--Dynamic grid view to display the list of all MCUs that do not have a group associated with them.-->
    <asp:GridView ID="GridAddMcu" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="MCUID" DataSourceID="SqlDataGridAddMcu" Width="493px" OnSelectedIndexChanged="GridAddMcu_SelectedIndexChanged">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="ChkAdd" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MCUID" SortExpression="MCUID">
                <EditItemTemplate>
                    <asp:Label ID="LblMCUID" runat="server" Text='<%# Eval("MCUID") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblMcuId" runat="server" Text='<%# Bind("MCUID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Room" HeaderText="Room" SortExpression="Room" />
            <asp:BoundField DataField="Facility" HeaderText="Facility" SortExpression="Facility" />
        </Columns>
    </asp:GridView>
    <asp:HiddenField runat="server" ID="UseName"/>
    <asp:SqlDataSource ID="SqlDataGridAddMcu" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [MCUId], [Room], [Facility] FROM [MCU] WHERE (([GroupName] IS NULL) AND ([UserName] = @UserName))">
        <SelectParameters>
            <asp:ControlParameter ControlID="UseName" Name="UserName" Type="String" DefaultValue="Anonymous" />
        </SelectParameters>
    </asp:SqlDataSource>
    <!--Sets up html controls for Group control.-->
    <div>
        <p>
            <asp:Button ID="BtnAddMcu" runat="server" OnClick="BtnAddMcu_Click" Text="Add To Group" />
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
        <!--Dynamic grid view to display the list of all MCUs that belong to the current group-->
        <asp:GridView ID="GridRemoveMcu" runat="server" AutoGenerateColumns="False" DataKeyNames="MCUID" DataSourceID="SqlDataGridRemoveMcu" Width="494px">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="ChkRemove" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MCUID" SortExpression="MCUID">
                    <EditItemTemplate>
                        <asp:Label ID="LblMCUID" runat="server" Text='<%# Eval("MCUID") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblMCUId2" runat="server" Text='<%# Bind("MCUID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="GroupName" HeaderText="Group" SortExpression="GroupName" />
                <asp:BoundField DataField="Room" HeaderText="Room" SortExpression="Room" />
                <asp:BoundField DataField="Facility" HeaderText="Facility" SortExpression="Facility" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataGridRemoveMcu" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [MCUId], [GroupName], [Room], [Facility] FROM [MCU] WHERE ([GroupName] = @GroupName)">
            <SelectParameters>
                <asp:QueryStringParameter Name="GroupName" QueryStringField="GroupName" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <asp:Button ID="btnRemove" runat="server" OnClick="btnRemove_Click" Text="Remove MCUs" />
        <br />
    </div>

</asp:Content>
