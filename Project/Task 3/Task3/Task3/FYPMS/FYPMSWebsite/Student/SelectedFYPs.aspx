<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SelectedFYPs.aspx.cs" Inherits="FYPMSWebsite.Student.SelectedFYPs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #00008B" class="h4"><strong>FYPs For Which Your Group Has Indicated An Interest</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:Panel ID="pnlSelectedProjects" runat="server" Visible="False">
            <hr />
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvSelectedProjects" runat="server" CssClass="table-condensed" BorderStyle="Solid" CellPadding="0"
                        Font-Names="Arial" Font-Size="Small" OnRowDataBound="GvSelectedProjects_RowDataBound">
                        <Columns>
                            <asp:HyperLinkField Text="Details" DataNavigateUrlFields="FYPID" NavigateUrl="../DisplayFYPDetails.aspx"
                                DataNavigateUrlFormatString="../DisplayFYPDetails.aspx?fypId={0}&returnUrl=~/Student/SelectedFYPs.aspx" />
                        </Columns>
                        <EditRowStyle Font-Names="Arial" Font-Size="Small" />
                        <HeaderStyle Font-Names="Arial" Font-Size="Small" Wrap="False" />
                        <RowStyle Font-Names="Arial" Font-Size="Small" />
                    </asp:GridView>
                    <br />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlSelectProjects" runat="server" Visible="False">
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:HyperLink ID="hlSelectProjects" runat="server" NavigateUrl="~/Student/AvailableFYPs.aspx" Font-Names="Arial" Font-Size="Small">Show Available FYPs</asp:HyperLink>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlFormProjectGroup" runat="server" Visible="False">
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:HyperLink ID="hlCreateProjectGroup" runat="server" NavigateUrl="~/Student/ManageProjectGroup.aspx" Font-Names="Arial" Font-Size="Small">Create Project Group</asp:HyperLink>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
