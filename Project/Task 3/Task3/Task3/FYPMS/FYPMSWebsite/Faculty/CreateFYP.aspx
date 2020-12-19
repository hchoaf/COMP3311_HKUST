<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateFYP.aspx.cs" Inherits="FYPMSWebsite.Faculty.CreateFYP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfSQLError" runat="server" Visible="False" Value="false" />
    <div class="form-horizontal" role="form">
        <h4><span style="text-decoration: underline; color: #00008B" class="h4"><strong>Create FYP</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
        <asp:Panel ID="pnlProjectInfo" runat="server">
            <hr />
            <div class="form-group" role="row">
                <!-- Title input control -->
                <asp:Label runat="server" Text="Title:" CssClass="control-label col-xs-2" AssociatedControlID="txtTitle" Font-Names="Arial"
                    Font-Size="Small"></asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control input-sm" MaxLength="100" Font-Names="Arial"
                        Font-Size="Small"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="The title field is required." ControlToValidate="txtTitle"
                        CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ValidationGroup="ProjectValidation"
                        Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group" role="row">
                <!-- Description input control -->
                <asp:Label runat="server" Text="Description:" CssClass="control-label col-xs-2" AssociatedControlID="txtDescription"
                    Font-Names="Arial" Font-Size="Small"></asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control input-sm" MaxLength="1200" TextMode="MultiLine"
                        Height="100" Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="The description field is required." ControlToValidate="txtDescription"
                        CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ValidationGroup="ProjectValidation" Font-Names="Arial"
                        Font-Size="Small"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group" role="row">
                <!-- Co-supervisor input control -->
                <asp:Label runat="server" Text="Co-supervisor:" CssClass="control-label col-xs-2" AssociatedControlID="ddlCosupervisor"
                    Font-Names="Arial" Font-Size="Small"></asp:Label>
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddlCosupervisor" runat="server" Font-Names="Arial" Font-Size="Small">
                    </asp:DropDownList>
                </div>
                <!-- Category input control -->
                <asp:Label runat="server" Text="Category:" CssClass="control-label col-xs-1" AssociatedControlID="ddlCategory"
                    Font-Names="Arial" Font-Size="Small"></asp:Label>
                <div class="col-xs-3">
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="dropdown" Font-Names="Arial" Font-Size="Small">
                    </asp:DropDownList>
                </div>
                <!-- Project type input control -->
                <asp:Label runat="server" Text="Type:" CssClass="control-label col-xs-1" AssociatedControlID="rblType"
                    Font-Names="Arial" Font-Size="Small"></asp:Label>
                <div class="col-xs-3">
                    <asp:RadioButtonList ID="rblType" runat="server" CssClass="" RepeatDirection="Horizontal" RepeatLayout="Flow"
                        AutoPostBack="True" OnSelectedIndexChanged="RblType_SelectedIndexChanged" Font-Names="Arial" Font-Size="Small">
                        <asp:ListItem class="radio-inline" Value="project" Selected="True">Project</asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="thesis">Thesis</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="form-group" role="row">
                <!-- Requirement input control -->
                <asp:Label runat="server" Text="Requirements:" CssClass="control-label col-xs-2" AssociatedControlID="txtOtherRequirements"
                    Font-Names="Arial" Font-Size="Small"></asp:Label>
                <div class="col-xs-10">
                    <asp:TextBox ID="txtOtherRequirements" runat="server" CssClass="form-control input-sm" MaxLength="200" Wrap="False"
                        Font-Names="Arial" Font-Size="Small"></asp:TextBox>
                </div>
            </div>
            <div class="form-group" role="row">
                <!-- Minimum number of students input control -->
                <asp:Label runat="server" Text="Minimum number of students in this project (1 if individual):" CssClass="control-label col-xs-7"
                    AssociatedControlID="rblMinStudents" Font-Names="Arial" Font-Size="Small"></asp:Label>
                <div class="col-xs-5">
                    <asp:RadioButtonList ID="rblMinStudents" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                        Font-Names="Arial" Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="RblMinStudents_SelectedIndexChanged">
                        <asp:ListItem class="radio-inline" Selected="True">1</asp:ListItem>
                        <asp:ListItem class="radio-inline">2</asp:ListItem>
                        <asp:ListItem class="radio-inline">3</asp:ListItem>
                        <asp:ListItem class="radio-inline">4</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="form-group" role="row">
                <!-- Maximum number of students input control -->
                <asp:Label runat="server" Text="Maximum number of students in this project (1 if individual):" CssClass="control-label col-xs-7"
                    AssociatedControlID="rblMaxStudents" Font-Names="Arial" Font-Size="Small"></asp:Label>
                <div class="col-xs-5">
                    <asp:RadioButtonList ID="rblMaxStudents" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                        Font-Names="Arial" Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="RblMaxStudents_SelectedIndexChanged">
                        <asp:ListItem class="radio-inline" Selected="True">1</asp:ListItem>
                        <asp:ListItem class="radio-inline">2</asp:ListItem>
                        <asp:ListItem class="radio-inline">3</asp:ListItem>
                        <asp:ListItem class="radio-inline">4</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="form-group" role="row">
                <div class="col-xs-offset-2">
                    <asp:CompareValidator runat="server" ErrorMessage="The minimum number of students cannot be greater than the maximum number of students."
                        ControlToCompare="rblMinStudents" ControlToValidate="rblMaxStudents" ValidationGroup="ProjectValidation"
                        Operator="GreaterThanEqual" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" Font-Names="Arial"
                        Font-Size="Small" ID="CvMinMaxStudents"></asp:CompareValidator>
                </div>
            </div>
            <div class="form-group" role="row">
                <!-- Close project input control -->
                <asp:Label runat="server" Text="Close this project:" CssClass="control-label col-xs-2" AssociatedControlID="rblIsAvailable"
                    Font-Names="Arial" Font-Size="Small"></asp:Label>
                <div class="col-xs-4">
                    <asp:RadioButtonList ID="rblIsAvailable" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Font-Names="Arial"
                        Font-Size="Small">
                        <asp:ListItem class="radio-inline" Value="Y" Selected="True">No</asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="N">Yes</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="form-group" role="row">
                <div class="col-xs-offset-2 col-xs-2">
                    <asp:Button ID="btnCreate" runat="server" Text="Create FYP" CssClass="btn-sm" OnClick="BtnCreate_Click"
                        ValidationGroup="ProjectValidation" Font-Names="Arial" Font-Size="Small" />
                </div>
                <div class="col-xs-offset-2 col-xs-2">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn-sm"
                        Font-Names="Arial" Font-Size="Small" OnClick="BtnCancel_Click" />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
