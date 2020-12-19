<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisplayReaders.aspx.cs" Inherits="FYPMSWebsite.Coordinator.DisplayReaders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #00008B" class="h4"><strong>FYP Readers</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:Panel ID="pnlAssignedReaders" runat="server" Visible="False">
            <hr />
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvAssignedReaders" runat="server" CssClass="table-condensed" BorderStyle="Solid" CellPadding="0"
                        Font-Names="Arial" Font-Size="Small" OnRowDataBound="GvAssignedReaders_RowDataBound">
                        <HeaderStyle Font-Names="Arial" Font-Size="Small" />
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
        <br />
        <div class="form-group">
            <div class="col-xs-3">
                <asp:HyperLink ID="hlAssignReaders" runat="server" NavigateUrl="~/Coordinator/AssignReaders.aspx" Font-Names="Arial" Font-Size="Small">FYPs Without Readers</asp:HyperLink>
            </div>
        </div>
    </div>
</asp:Content>
