<%@ Page Title="Forgot Password | BookSmart" Language="C#" MasterPageFile="~/Pages/Site1.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Assignment_Web_Application.Pages.ForgotPassword" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-5">
            <div class="card shadow-sm p-4">
                <h3 class="mb-3">Forgot Password</h3>
                <asp:Label ID="lblMsg" runat="server" CssClass="text-danger"></asp:Label>

                <div class="mb-3">
                    <label class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                </div>

                <asp:LinkButton ID="btnSendReset" runat="server" CssClass="btn btn-primary w-100" OnClick="btnSendReset_Click">
                    Send Reset Link
                </asp:LinkButton>

                <p class="mt-3 text-muted">
                    Remembered your password? <a href="/Pages/Login.aspx">Login</a>
                </p>
            </div>
        </div>
    </div>
</asp:Content>

