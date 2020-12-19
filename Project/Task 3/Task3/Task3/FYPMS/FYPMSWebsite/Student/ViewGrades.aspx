<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewGrades.aspx.cs" Inherits="FYPMSWebsite.Student.ViewGrades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #00008B" class="h4"><strong>Your Grades</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:Panel ID="pnlGrades" runat="server" Visible="False">
            <hr />
            <div class="form-group">
                <asp:Label runat="server" Text="Title:" CssClass="control-label col-xs-2" AssociatedControlID="txtTitle" Font-Names="Arial"
                    Font-Size="Small"></asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control-static" Font-Names="Arial" Font-Size="Small"
                        ReadOnly="True" BackColor="White" BorderStyle="None" BorderWidth="0px" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Supervisor(s):" CssClass="control-label col-xs-2" AssociatedControlID="txtSupervisor"
                    Font-Names="Arial" Font-Size="Small"></asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txtSupervisor" runat="server" CssClass="form-control-static" Font-Names="Arial" Font-Size="Small"
                        ReadOnly="True" BackColor="White" BorderStyle="None" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div class="col-xs-offset-2 col-xs-10">
                    <asp:GridView ID="gvGrades" runat="server" CssClass="table-condensed" BorderStyle="Solid" CellPadding="0"
                        Font-Names="Arial" Font-Size="Small" OnRowDataBound="GvGrades_RowDataBound">
                        <HeaderStyle Font-Names="Arial" Font-Size="Small" Wrap="False" />
                        <RowStyle Font-Names="Arial" Font-Size="Small" />
                    </asp:GridView>
                </div>
            </div>
            <br />
        </asp:Panel>
    </div>
</asp:Content>