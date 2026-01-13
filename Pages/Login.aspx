<%@ Page Title="Login | BookSmart" Language="C#" MasterPageFile="~/Pages/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Assignment_Web_Application.Pages.Login" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-5">
            <div class="card shadow-sm mt-5">
                <div class="card-body p-4">
                    <h3 class="mb-3">Login</h3>

                    <asp:Label ID="lblMsg" runat="server" CssClass="text-danger mb-3 d-block"></asp:Label>

                    <div class="mb-3">
                        <label for="txtUser" class="form-label">Email or Username</label>
                        <asp:TextBox ID="txtUser" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvUser" runat="server" ControlToValidate="txtUser"
                            ErrorMessage="Please enter your username or email." CssClass="text-danger" Display="Dynamic" />
                    </div>

                    <div class="mb-3 position-relative">
                        <label for="txtPass" class="form-label">Password</label>
                        <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="form-control" />
                        <span id="togglePass" style="position:absolute; top:38px; right:10px; cursor:pointer;">
                            <i class="bi bi-eye"></i>
                        </span>
                        <asp:RequiredFieldValidator ID="rfvPass" runat="server" ControlToValidate="txtPass"
                            ErrorMessage="Please enter your password." CssClass="text-danger" Display="Dynamic" />
                    </div>

                    <div class="form-check mb-3">
                        <asp:CheckBox ID="chkRemember" runat="server" CssClass="form-check-input" />
                        <label class="form-check-label" for="chkRemember">Remember Me</label>
                        <a href="/Pages/ForgotPassword.aspx" class="float-end text-decoration-none">Forgot Password?</a>
                    </div>

                    <asp:LinkButton ID="btnLogin" runat="server" CssClass="btn btn-primary w-100" OnClick="btnLogin_Click">
                        Login
                    </asp:LinkButton>

                    <p class="mt-3 text-muted text-center">No account? <a href="/Pages/Register.aspx">Register</a></p>
                </div>
            </div>
        </div>
    </div>

    <!-- jQuery and ScriptManager -->
    <script src="https://code.jquery.com/jquery-3.7.1.min.js" crossorigin="anonymous"></script>
    <asp:ScriptManager runat="server" EnablePartialRendering="true" />

    <!-- Show/Hide Password -->
    <script>
        $(document).ready(function () {
            $('#togglePass').click(function () {
                var txt = $('#<%= txtPass.ClientID %>');
                var icon = $(this).find('i');
                if (txt.attr('type') === 'password') {
                    txt.attr('type', 'text');
                    icon.removeClass('bi-eye').addClass('bi-eye-slash');
                } else {
                    txt.attr('type', 'password');
                    icon.removeClass('bi-eye-slash').addClass('bi-eye');
                }
            });
        });
    </script>
</asp:Content>
