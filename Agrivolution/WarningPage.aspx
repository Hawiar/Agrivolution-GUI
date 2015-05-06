<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WarningPage.aspx.cs" Inherits="Agrivolution.WarningPage" %>

<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" runat="server">
    <p></p>
     <asp:GridView ID="WarningsGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="MCUId" DataSourceID="SqlDataWarningsGrid" Width="493px">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckRemove" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="MCUId" HeaderText="MCUId" ReadOnly="True" SortExpression="MCUId" />
                <asp:BoundField DataField="Message" HeaderText="Message" ReadOnly="True" SortExpression="Message"/>
                <asp:BoundField DataField="TimeStamp" HeaderText="Time Stamp" ReadOnly="True" SortExpression="TimeStamp"/>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataWarningsGrid" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [MCUId], [Message], [TimeStamp] FROM [Warnings]"></asp:SqlDataSource>
        <asp:HiddenField runat="server" ID="UseName"/>
     <asp:Button ID="btnRemove" runat="server" OnClick="btnRemove_Click" Text="Remove Warning" Visible="False" />
     <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
    <div>
        
    </div>
</asp:Content>
