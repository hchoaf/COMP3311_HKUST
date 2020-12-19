<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisplayFYPs.aspx.cs" Inherits="FYPMSWebsite.Faculty.DisplayFYPs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #00008B" class="h4"><strong>My FYPs</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:Panel ID="pnlFYPInfo" runat="server">
            <hr />
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvFYPs" runat="server" CssClass="table-condensed" OnRowDataBound="GvProjects_RowDataBound"
                        Font-Names="Arial" Font-Size="Small">
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="FYPID" DataNavigateUrlFormatString="EditFYP.aspx?fypId={0}"
                                NavigateUrl="~/Faculty/EditFYP.aspx" Text="Edit" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <br />
        </asp:Panel>
    </div>
</asp:Content>
