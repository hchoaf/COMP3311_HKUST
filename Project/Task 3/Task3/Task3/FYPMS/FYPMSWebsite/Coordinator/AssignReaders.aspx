<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssignReaders.aspx.cs" Inherits="FYPMSWebsite.Coordinator.AssignReaders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #00008B" class="h4"><strong>Assign Readers to FYPs</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:Panel ID="pnlDisplayFYPsWithoutReaders" runat="server" Visible="False">
            <hr />
            <h5><span style="text-decoration: underline; color: #00008B" class="h5"><strong>FYPs Without Readers</strong></span></h5>
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvFYPsWithoutReaders" runat="server" CssClass="table-condensed" BorderStyle="Solid" CellPadding="0"
                        AutoGenerateSelectButton="True" Font-Names="Arial" Font-Size="Small"
                        OnSelectedIndexChanged="GvFYPsWithoutReaders_SelectedIndexChanged" OnRowDataBound="GvFYPsWithoutReaders_RowDataBound">
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlAssignReader" runat="server" Visible="False">
            <hr />
            <h5><span style="text-decoration: underline; color: #00008B" class="h5"><strong>Readers Available For Assignment To Selected FYP</strong></span></h5>
            <div class="form-group">
                <!-- Group code textbox -->
                <asp:Label runat="server" Text="Selected FYP:" CssClass="control-label col-xs-2" AssociatedControlID="txtGroupCode" Font-Names="Arial"
                    Font-Size="Small"></asp:Label>
                <div class="col-xs-1">
                    <asp:TextBox ID="txtGroupCode" runat="server" CssClass="form-control-static" Font-Names="Arial" Font-Size="Small"
                        ReadOnly="True" Wrap="False" BorderColor="White" BorderStyle="None" BorderWidth="0px" Width="100%"></asp:TextBox>
                </div>
                <!-- Title textbox -->
                <asp:Label runat="server" Text="Title:" CssClass="control-label col-xs-1" AssociatedControlID="txtTitle" Font-Names="Arial"
                    Font-Size="Small"></asp:Label>
                <div class="col-xs-8">
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control-static" Font-Names="Arial" Font-Size="Small"
                        ReadOnly="True" Wrap="False" BorderColor="White" BorderStyle="None" BorderWidth="0px" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvAvailableReaders" runat="server" AutoGenerateSelectButton="True" BorderStyle="Solid"
                        CssClass="table-condensed" Font-Names="Arial" Font-Size="Small"
                        OnSelectedIndexChanged="GvAvailableReaders_SelectedIndexChanged" OnRowDataBound="GvAvailableReaders_RowDataBound">
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
        <br />
        <div class="form-group">
            <div class="col-xs-3">
                <asp:HyperLink ID="hlDisplayReaders" runat="server" NavigateUrl="~/Coordinator/DisplayReaders.aspx" Font-Names="Arial"
                    Font-Size="Small">FYPs With Readers</asp:HyperLink>
            </div>
        </div>
    </div>
</asp:Content>
