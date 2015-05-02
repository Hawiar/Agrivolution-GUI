<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditMCU.aspx.cs" Inherits="Agrivolution.EditMCU" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <h2>MCU Id:&nbsp;
            <asp:Label ID="MCULabel" runat="server"></asp:Label>
        </h2>
    <p>
        <asp:GridView ID="Attributes" runat="server" AutoGenerateColumns="False" DataSourceID="MCUAttributeSource">
            <Columns>
                <asp:BoundField DataField="Facility" HeaderText="Facility" SortExpression="Facility" />

                <asp:TemplateField HeaderText="Room">
                    <ItemTemplate>
                        <asp:Button DataField="Room" ID="Room" runat="server" Text='<%# Eval("Room") %>' OnClick="redirectToRoom" UseSubmitBehavior="false" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Group Name">
                    <ItemTemplate>
                        <asp:Button DataField="GroupName" ID="GroupName" runat="server" Text='<%# Eval("GroupName") %>' OnClick="redirectToGroup" UseSubmitBehavior="false" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:CheckBoxField DataField="PumpStatus" HeaderText="PumpStatus" SortExpression="PumpStatus"></asp:CheckBoxField>
            </Columns>
        </asp:GridView>
    </p>
    <p>
        <asp:SqlDataSource ID="MCUAttributeSource" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [Room], [Facility], [GroupName], [PumpStatus] FROM [MCU] WHERE ([MCUId] = @MCUId)">
            <SelectParameters>
                <asp:QueryStringParameter Name="MCUId" QueryStringField="MCUId" Type="Int32" DefaultValue="" />
            </SelectParameters>
        </asp:SqlDataSource>
    </p>
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
        ight Cycle (hours)&nbsp;&nbsp;
        <asp:TextBox ID="lightTimerValue" runat="server"></asp:TextBox>
    </p>
    <p>
        Crop Type:<asp:TextBox ID="cropType" runat="server"></asp:TextBox>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Button ID="setMCUButon" runat="server" Text="apply changes" OnClick="setMCUValues"/>
    </p>
        <p>
            &nbsp;</p>
        <p>
        <asp:Button ID="returnButton" runat="server" OnClick="returnClick" Text="Return" />
    </p>
</asp:Content>
