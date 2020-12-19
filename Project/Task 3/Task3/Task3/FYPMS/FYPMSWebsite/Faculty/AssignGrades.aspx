<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssignGrades.aspx.cs" Inherits="FYPMSWebsite.Faculty.AssignGrades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #00008B" class="h4"><strong>Assign Grades To Students</strong></span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="false"></asp:Label>
        <hr />
        <div class="form-group">
            <asp:Label runat="server" Text="Grade as:" CssClass="control-label col-xs-2" AssociatedControlID="rblGraderRole"
                Font-Names="Arial" Font-Size="Small"></asp:Label>
            <div class="col-xs-3">
                <asp:RadioButtonList ID="rblGraderRole" runat="server" CssClass="" RepeatDirection="Horizontal" RepeatLayout="Flow"
                    AutoPostBack="True" OnSelectedIndexChanged="RblGraderRole_SelectedIndexChanged" Font-Names="Arial" Font-Size="Small">
                    <asp:ListItem class="radio-inline" Value="supervisor" Selected="True">Supervisor</asp:ListItem>
                    <asp:ListItem class="radio-inline" Value="reader">Reader</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <asp:Panel ID="pnlSelectGroup" runat="server">
                <asp:Label runat="server" Text="Group:" CssClass="control-label col-xs-1" AssociatedControlID="ddlGroups" Font-Names="Arial"></asp:Label>
                <div class="col-xs-7">
                    <asp:DropDownList ID="ddlGroups" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="Small" OnSelectedIndexChanged="DdlGroups_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </asp:Panel>
        </div>
        <asp:Panel ID="pnlGroupMembers" runat="server" Visible="False">
            <hr />
            <h5><span style="text-decoration: underline; color: #00008B" class="h5"><strong>Students in the Selected Group:</strong></span></h5>
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvStudents" runat="server" CssClass="table-condensed" BorderStyle="Solid" CellPadding="0"
                        Font-Names="Arial" Font-Size="Small" OnRowCancelingEdit="GvStudents_RowCancelingEdit"
                        OnRowEditing="GvStudents_RowEditing" OnRowUpdating="GvStudents_RowUpdating" OnRowDataBound="GvStudents_RowDataBound"
                        AutoGenerateEditButton="True" DataKeyNames="NAME">
                        <EditRowStyle Font-Names="Arial" Font-Size="Small" />
                        <HeaderStyle Font-Names="Arial" Font-Size="Small" Wrap="False" />
                        <RowStyle Font-Names="Arial" Font-Size="Small" />
                    </asp:GridView>
                </div>
            </div>
            <div class="form-group">
                <br />
                <asp:Label ID="lblGradeErrorMessage" runat="server" Font-Bold="True" CssClass="label col-xs-offset-1" Font-Names="Arial Narrow" Font-Size="Small"></asp:Label>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
