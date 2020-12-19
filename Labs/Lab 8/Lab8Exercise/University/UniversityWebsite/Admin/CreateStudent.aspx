<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateStudent.aspx.cs" Inherits="UniversityWebsite.Admin.CreateStudentRecord" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #990000">Create Student Record</span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial"></asp:Label>
        <asp:Panel ID="pnlStudentInfo" runat="server">
            <hr />
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtStudentId" CssClass="col-xs-2 control-label" Font-Names="Arial">Student Id</asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox runat="server" ID="txtStudentId" CssClass="form-control input-sm" MaxLength="8" ToolTip="Student id" Font-Names="Arial" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtStudentId" CssClass="text-danger" ErrorMessage="A student id is required."
                        Display="Dynamic" EnableClientScript="False" Font-Names="Arial" Font-Size="Small" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtStudentId" CssClass="text-danger" ErrorMessage="Please enter exactly 8 digits."
                        Display="Dynamic" ValidationExpression="^\d{8}$" EnableClientScript="False" Font-Names="Arial" Font-Size="Small"></asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="CvStudentId" runat="server" ControlToValidate="txtStudentId" CssClass="text-danger"
                        Display="Dynamic" EnableClientScript="False" OnServerValidate="CvStudentId_ServerValidate" Visible="True" ErrorMessage="The student id already exists."
                        Font-Names="Arial" Font-Size="Small"></asp:CustomValidator>
                </div>
                <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="col-xs-2 control-label" Font-Names="Arial">Email</asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control input-sm" MaxLength="15" ToolTip="Email address" Font-Names="Arial" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" CssClass="text-danger" ErrorMessage="An email is required."
                        Display="Dynamic" EnableClientScript="False" Font-Names="Arial" Font-Size="Small" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"
                        ErrorMessage="Please enter a valid email." Font-Names="Arial" Font-Size="Small" ValidationExpression="^[a-zA-Z0-9,.'-]+$"></asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="CvUserEmail" runat="server" ControlToValidate="txtEmail" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"
                        ErrorMessage="The email already exists." OnServerValidate="CvUserEmail_ServerValidate" Font-Names="Arial" Font-Size="Small"></asp:CustomValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtFirstName" CssClass="col-xs-2 control-label" Font-Names="Arial">First Name</asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control input-sm" MaxLength="20" ToolTip="First nName" Font-Names="Arial" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFirstName" CssClass="text-danger" ErrorMessage="A first name is required."
                        Display="Dynamic" EnableClientScript="False" Font-Names="Arial" Font-Size="Small" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFirstName" CssClass="text-danger"
                        Display="Dynamic" EnableClientScript="False" ErrorMessage="Please enter a valid first name." Font-Names="Arial" Font-Size="Small"
                        ValidationExpression="^[A-Z][A-Za-z]+((\s)?((\'|\-|\.)?([A-Za-z])+))*$"></asp:RegularExpressionValidator>
                </div>
                <asp:Label runat="server" AssociatedControlID="txtLastName" CssClass="col-xs-2 control-label" Font-Names="Arial">Last Name</asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control input-sm" MaxLength="25" ToolTip="Last name" Font-Names="Arial" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLastName"
                        CssClass="text-danger" ErrorMessage="A last name is required." Display="Dynamic" EnableClientScript="False" Font-Names="Arial" Font-Size="Small" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtLastName" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"
                        ErrorMessage="Please enter a valid last name." Font-Names="Arial" Font-Size="Small" ValidationExpression="^[A-Za-z]+((\s)?((\'|\-|\.)?([A-Za-z])+))*$"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtPhoneNo" CssClass="col-xs-2 control-label" Font-Names="Arial">Phone Number</asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox runat="server" ID="txtPhoneNo" CssClass="form-control input-sm" MaxLength="8" ToolTip="Phone number" Font-Names="Arial" Width="80px" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPhoneNo" CssClass="text-danger" ErrorMessage="A phone number is required."
                        Display="Dynamic" EnableClientScript="False" Font-Names="Arial" Font-Size="Small" />
                    <asp:RegularExpressionValidator ID="revPhoneNo" runat="server" ControlToValidate="txtPhoneNo" CssClass="text-danger"
                        ErrorMessage="Please enter exactly 8 digits." ValidationExpression="\d{8}" Display="Dynamic" EnableClientScript="False"
                        Font-Names="Arial" Font-Size="Small"></asp:RegularExpressionValidator>
                </div>
                <asp:Label runat="server" AssociatedControlID="ddlDepartments" CssClass="col-xs-2 control-label" Font-Names="Arial">Department</asp:Label>
                <div class="col-xs-3">
                    <asp:DropDownList ID="ddlDepartments" runat="server" CssClass="form-control dropdown input-sm" ForeColor="Black" Font-Names="Arial">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ErrorMessage="Please select a department." InitialValue="none selected"
                        Display="Dynamic" EnableClientScript="False" ControlToValidate="ddlDepartments" Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtAdmissionYear" CssClass="col-xs-2 control-label" Font-Names="Arial">Admission Year</asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox runat="server" ID="txtAdmissionYear" CssClass="form-control input-sm" MaxLength="4" ToolTip="Admission year" Font-Names="Arial" Width="60px" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAdmissionYear" CssClass="text-danger" ErrorMessage="An admission year is required."
                        Display="Dynamic" EnableClientScript="False" Font-Names="Arial" Font-Size="Small" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtAdmissionYear" CssClass="text-danger" ErrorMessage="Please enter a valid year."
                        ValidationExpression="\d{4}" Display="Dynamic" EnableClientScript="False" Font-Names="Arial" Font-Size="Small"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="form-group">
                <div class="col-xs-offset-2 col-xs-10">
                    <asp:Button runat="server" OnClick="CreateStudent_Click" Text="Create" CssClass="btn-sm" Font-Names="Arial" />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>