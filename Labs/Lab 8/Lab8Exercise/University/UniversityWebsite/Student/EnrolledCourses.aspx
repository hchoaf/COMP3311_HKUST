<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EnrolledCourses.aspx.cs" Inherits="UniversityWebsite.Enrollment.EnrolledCourses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #990000">Enrolled Courses</span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label"></asp:Label>
        <asp:Panel ID="pnlEnrolledCourses" runat="server" Visible="False">
            <hr />
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvEnrolledCourses" runat="server" BorderWidth="2px" CssClass="table-condensed" BorderStyle="Solid"
                        Caption="&lt;b style='color:Black;'&gt;Courses You Are Enrolled In&lt;/b&gt;" CaptionAlign="Top" CellPadding="3"
                        OnRowDataBound="GvEnrolledCourses_RowDataBound">
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>