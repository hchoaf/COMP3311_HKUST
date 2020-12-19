<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UniversityWebsite._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <div class="jumbotron">
        <h1 style="text-align: center">University Website</h1>
        <br />
        <div style="text-align: center">
            <asp:Image runat="server" ImageUrl="~/Images/HKUST.jpg" ImageAlign="Middle" />
        </div>
    </div>
</asp:Content>