<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EnrollInCourses.aspx.cs" Inherits="UniversityWebsite.Enrollment.EnrollInCourses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #990000">Enroll in Courses</span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label"></asp:Label>
        <asp:Panel ID="pnlAvailableCourses" runat="server" Visible="False">
            <hr />
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvAvailableCourses" runat="server" BorderWidth="2px" CssClass="table-condensed" BorderStyle="Solid"
                        Caption="&lt;b style='color:Black;'&gt;Courses You Can Enroll In&lt;/b&gt;" CaptionAlign="Top" CellPadding="3" 
                        HorizontalAlign="Justify" OnRowDataBound="GvAvailableCourses_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="SELECT">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRow" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="form-group">
                <div class="col-xs-offset-1 col-xs-10">
                    <asp:Button ID="btnSubmit" runat="server" OnClick="BtnSubmit_Click" Text="Submit" CssClass="btn-sm" />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>