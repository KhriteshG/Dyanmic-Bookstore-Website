<%@ Page Title="Register" Language="C#" MasterPageFile="~/Pages/Site1.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Assignment_Web_Application.Pages.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.7/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .register-form { max-width: 500px; margin: auto; padding: 2rem; border: 1px solid #ddd; border-radius: 8px; background: #fff; }
        .register-form h2 { margin-bottom: 1.5rem; text-align: center; }
        .form-control { margin-bottom: 1rem; }
        .btn-register { width: 100%; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="register-form">
        <h2>Create Account</h2>

        <!-- Message Panel -->
        <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="mb-3">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </asp:Panel>

        <asp:Panel ID="pnlForm" runat="server">
            <!-- Username -->
            <div class="mb-3">
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Placeholder="Username" />
            </div>

            <!-- Email -->
            <div class="mb-3">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Email" TextMode="Email" />
            </div>

            <!-- Password -->
            <div class="mb-3">
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" Placeholder="Password" TextMode="Password" />
            </div>

            <!-- Confirm Password -->
            <div class="mb-3">
                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" Placeholder="Confirm Password" TextMode="Password" />
            </div>

            <!-- Register Button -->
            <div class="d-grid gap-2">
                <asp:Button ID="btnRegister" runat="server" CssClass="btn btn-primary btn-register" Text="Register" OnClick="btnRegister_Click" />
            </div>

            <p class="mt-3 text-center">
                Already have an account? <a href="Login.aspx">Login here</a>
            </p>
        </asp:Panel>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.7/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
