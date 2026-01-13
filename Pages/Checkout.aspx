<%@ Page Title="Checkout | BookSmart" Language="C#" 
    MasterPageFile="~/Pages/Site1.Master" AutoEventWireup="true" 
    CodeBehind="Checkout.aspx.cs" Inherits="Assignment_Web_Application.Pages.Checkout" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <h2 class="mb-4 text-center">Checkout</h2>

        <!-- Billing / Shipping Form -->
        <div class="row">
            <div class="col-md-7">
                <div class="card p-4 shadow-sm mb-4">
                    <h5 class="mb-3">Billing & Shipping Information</h5>
                    <div class="mb-3">
                        <label for="txtFullName" class="form-label">Full Name</label>
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="txtEmail" class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                    </div>
                    <div class="mb-3">
                        <label for="txtAddress" class="form-label">Address</label>
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                    </div>
                    <div class="mb-3">
                        <label for="txtCity" class="form-label">City</label>
                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="txtZip" class="form-label">ZIP / Postal Code</label>
                        <asp:TextBox ID="txtZip" runat="server" CssClass="form-control" />
                    </div>
                </div>

                <div class="card p-4 shadow-sm">
                    <h5 class="mb-3">Payment Information</h5>
                    <div class="mb-3">
                        <label for="txtCardNumber" class="form-label">Card Number</label>
                        <asp:TextBox ID="txtCardNumber" runat="server" CssClass="form-control" />
                    </div>
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label for="txtExpiry" class="form-label">Expiry (MM/YY)</label>
                            <asp:TextBox ID="txtExpiry" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6 mb-3">
                            <label for="txtCVV" class="form-label">CVV</label>
                            <asp:TextBox ID="txtCVV" runat="server" CssClass="form-control" TextMode="Password" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- Order Summary -->
            <div class="col-md-5">
                <div class="card p-4 shadow-sm">
                    <h5 class="mb-3">Order Summary</h5>
                    <asp:Repeater ID="rptSummary" runat="server">
                        <ItemTemplate>
                            <div class="d-flex justify-content-between mb-2">
                                <span><%# Eval("Title") %> (x<%# Eval("Quantity") %>)</span>
                                <span>$<%# (Convert.ToDecimal(Eval("Price")) * Convert.ToInt32(Eval("Quantity"))).ToString("F2") %></span>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <hr />
                    <div class="d-flex justify-content-between fw-bold">
                        <span>Total</span>
                        <span>$<asp:Literal ID="litOrderTotal" runat="server" /></span>
                    </div>
                    <div class="d-grid mt-4">
                        <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" CssClass="btn btn-primary" OnClick="btnPlaceOrder_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
