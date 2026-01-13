<%@ Page Title="Contact Us" Language="C#" MasterPageFile="~/Pages/Site1.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Assignment_Web_Application.Pages.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.7/dist/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5" style="max-width:600px;">
        <h2 class="mb-4 text-center">Contact Us</h2>

        <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="mb-3">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </asp:Panel>

        <asp:Panel ID="pnlForm" runat="server">
            <!-- Name -->
            <div class="mb-3">
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Placeholder="Your Name" />
            </div>

            <!-- Email -->
            <div class="mb-3">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Your Email" TextMode="Email" />
            </div>

            <!-- Subject -->
            <div class="mb-3">
                <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" Placeholder="Subject" />
            </div>

            <!-- Message -->
            <div class="mb-3">
                <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Placeholder="Your Message"></asp:TextBox>
            </div>

            <div class="d-grid gap-2">
                <asp:Button ID="btnSend" runat="server" CssClass="btn btn-primary" Text="Send Message" OnClick="btnSend_Click" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>



