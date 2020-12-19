<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisplayFYPDetails.aspx.cs" Inherits="FYPMSWebsite.DisplayFYPDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #00008B" class="h4"><strong>FYP Details</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:Panel ID="pnlProjectInfo" runat="server">
            <hr />
            <div class="form-group" role="row">
                <!-- Title control -->
                <asp:Label runat="server" Text="Title:" CssClass="control-label col-xs-2" AssociatedControlID="txtTitle" Font-Names="Arial"
                    Font-Size="Small"></asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control-static" Font-Names="Arial" Font-Size="Small"
                        ReadOnly="True" BackColor="White" BorderStyle="None" BorderWidth="0px" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="form-group" role="row">
                <!-- Description control -->
                <asp:Label runat="server" Text="Description:" CssClass="control-label col-xs-2" AssociatedControlID="txtDescription"
                    Font-Names="Arial" Font-Size="Small"></asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control-static" Font-Names="Arial" Font-Size="Small"
                        ReadOnly="True" BackColor="White" BorderStyle="None" Height="150px" TextMode="MultiLine" BorderWidth="0px" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="form-group" role="row">
                <!-- Supervisor control -->
                <asp:Label runat="server" Text="Supervisor(s):" CssClass="control-label col-xs-2" AssociatedControlID="txtSupervisor"
                    Font-Names="Arial" Font-Size="Small"></asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtSupervisor" runat="server" CssClass="form-control-static" Font-Names="Arial" Font-Size="Small"
                        ReadOnly="True" BackColor="White" BorderStyle="None" Width="100%"></asp:TextBox>
                </div>
                <!-- Category control -->
                <asp:Label runat="server" Text="Category:" CssClass="control-label col-xs-1" AssociatedControlID="txtCategory"
                    Font-Names="Arial" Font-Size="Small"></asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control-static" Font-Names="Arial" Font-Size="Small"
                        ReadOnly="True" BackColor="White" BorderStyle="None" Width="100%"></asp:TextBox>
                </div>
                <!-- Project type control -->
                <asp:Label runat="server" Text="Type:" CssClass="control-label col-xs-1" AssociatedControlID="txtType"
                    Font-Names="Arial" Font-Size="Small"></asp:Label>
                <div class="col-xs-2">
                    <asp:TextBox ID="txtType" runat="server" CssClass="form-control-static" Font-Names="Arial" Font-Size="Small"
                        ReadOnly="True" BackColor="White" BorderStyle="None" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="form-group" role="row">
                <!-- Requirement control -->
                <asp:Label runat="server" Text="Requirements:" CssClass="control-label col-xs-2" AssociatedControlID="txtRequirements"
                    Font-Names="Arial" Font-Size="Small"></asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txtRequirements" runat="server" CssClass="form-control-static" Font-Names="Arial" Font-Size="Small"
                        ReadOnly="True" BackColor="White" BorderStyle="None" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="form-group" role="row">
                <!-- Minimum number of students control -->
                <asp:Label runat="server" Text="Minimum students:" CssClass="control-label col-xs-offset-2 col-xs-2"
                    AssociatedControlID="txtMinStudents" Font-Names="Arial" Font-Size="Small"></asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtMinStudents" runat="server" CssClass="form-control-static" Font-Names="Arial" Font-Size="Small"
                        ReadOnly="True" BackColor="White" BorderStyle="None"></asp:TextBox>
                </div>
                <!-- Maximum number of students control -->
                <asp:Label runat="server" Text="Maximum students:" CssClass="control-label col-xs-2"
                    AssociatedControlID="txtMaxStudents" Font-Names="Arial" Font-Size="Small"></asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtMaxStudents" runat="server" CssClass="form-control-static" Font-Names="Arial" Font-Size="Small"
                        ReadOnly="True" BackColor="White" BorderStyle="None"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div class="col-xs-offset-1 col-xs-11">
                    <asp:LinkButton ID="btnReturn" runat="server" OnClick="BtnReturn_Click" Font-Names="Arial" Font-Size="Small"><<< Go Back</asp:LinkButton>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
