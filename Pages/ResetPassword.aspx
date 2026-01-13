<%@ Page Title="Reset Password | BookSmart" Language="C#" MasterPageFile="~/Pages/Site1.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="Assignment_Web_Application.Pages.ResetPassword" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-5">
            <div class="card shadow-sm p-4">
                <h3 class="mb-3">Reset Password</h3>
                <asp:Label ID="lblMsg" runat="server" CssClass="text-danger"></asp:Label>

                <div class="mb-3">
                    <label class="form-label">New Password</label>
                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="form-control" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Confirm Password</label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" />
                </div>

                <asp:LinkButton ID="btnReset" runat="server" CssClass="btn btn-primary w-100" OnClick="btnReset_Click">
                    Reset Password
                </asp:LinkButton>

                <p class="mt-3 text-muted">
                    Remembered your password? <a href="/Pages/Login.aspx">Login</a>
                </p>
            </div>
        </div>
    </div>
</asp:Content>

