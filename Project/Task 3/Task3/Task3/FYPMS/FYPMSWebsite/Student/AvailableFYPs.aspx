<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AvailableFYPs.aspx.cs" Inherits="FYPMSWebsite.Student.AvailableFYPs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #00008B" class="h4"><strong>FYPs For Which Your Group Can Indicate An Interest</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:Panel ID="pnlFYPsAvailableForSelection" runat="server" Visible="False">
            <hr />
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvAvailableForSelection" runat="server" CssClass="table-condensed" BorderStyle="Solid" CellPadding="0"
                        Font-Names="Arial" Font-Size="Small" OnRowDataBound="GvAvailableForSelection_RowDataBound">
                        <Columns>
                            <asp:HyperLinkField Text="Details" DataNavigateUrlFields="FYPID" NavigateUrl="../DisplayFYPDetails.aspx"
                                DataNavigateUrlFormatString="../DisplayFYPDetails.aspx?fypId={0}&returnUrl=~/Student/AvailableFYPs.aspx" />
                            <asp:TemplateField HeaderText="PRIORITY">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlPriority" runat="server">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle Font-Names="Arial" Font-Size="Small" />
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlBtnSelectFYPs" runat="server" Visible="False">
            <br />
            <div class="form-group">
                <div class="col-xs-2">
                    <asp:Button ID="btnUpdateFYPInterest" runat="server" Text="Update FYP Interest" CssClass="btn-sm"
                        Font-Size="Small" Font-Names="Arial" OnClick="BtnUpdateFYPInterest_Click" />
                </div>
                <div class="col-xs-10">
                    <asp:HyperLink ID="hlSelectedProjects" runat="server" NavigateUrl="~/Student/SelectedFYPs.aspx" Font-Names="Arial" Font-Size="Small">Show Selected FYPs</asp:HyperLink>
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
        <br />
    </div>
</asp:Content>
