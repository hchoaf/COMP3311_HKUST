<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisplayAllFYPs.aspx.cs" Inherits="FYPMSWebsite.DisplayAllFYPs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #00008B" class="h4"><strong>FYP Information</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:Panel ID="pnlProjectInfo" runat="server">
            <hr />
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvProjects" runat="server" CssClass="table-condensed" OnRowDataBound="GvProjects_RowDataBound" Font-Names="Arial" Font-Size="Small" AllowPaging="True" OnPageIndexChanging="gvProjects_PageIndexChanging" PageSize="15">
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="FYPID" NavigateUrl="DisplayFYPDetails.aspx" Text="Details"
                                DataNavigateUrlFormatString="DisplayFYPDetails?fypId={0}&returnUrl=DisplayAllFYPs.aspx" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <br />
        </asp:Panel>
    </div>
</asp:Content>
