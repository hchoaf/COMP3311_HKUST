<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchForStudent.aspx.cs" Inherits="UniversityWebsite.Student.SearchForStudent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .form-group {
            margin-bottom: 0px;
        }
    </style>
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #990000">Search For Student Record</span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial"></asp:Label>
        <hr />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtStudentId" CssClass="col-xs-2 control-label" Font-Names="Arial">Student Id:</asp:Label>
            <div class="col-xs-2" id="8">
                <asp:TextBox ID="txtStudentId" runat="server" CssClass="form-control input-sm" MaxLength="8" ToolTip="Student Id" Font-Names="Arial"></asp:TextBox>
            </div>
            <div class="col-xs-2">
                <asp:Button ID="btnSearch" runat="server" OnClick="BtnFindStudent_Click" Text="Search" CssClass="btn-sm" Font-Names="Arial" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-xs-offset-2 col-xs-10">
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Please enter a valid student id." ControlToValidate="txtStudentId" 
                    CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator runat="server" ErrorMessage="Please enter exactly 8 digits."
                    ControlToValidate="txtStudentId" CssClass="text-danger" Display="Dynamic" ValidationExpression="^\d{8}$"></asp:RegularExpressionValidator>
            </div>
        </div>
        <asp:Panel ID="pnlStudentRecord" runat="server" Visible="False">
            <hr />
            <div class="form-group">
                <div class="col-xs-offset-1 col-xs-11">
                    <asp:GridView ID="gvStudentRecord" runat="server" BorderWidth="2px" CssClass="table-condensed" BorderStyle="Solid" 
                        Caption="&lt;b style='color:Black;'&gt;Search Result&lt;/b&gt;" CaptionAlign="Top" CellPadding="3" HorizontalAlign="Justify" Font-Names="Arial" Font-Size="Small"></asp:GridView>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>