<%@ Page Title="Your Cart | BookSmart" Language="C#" 
    MasterPageFile="~/Pages/Site1.Master" AutoEventWireup="true" 
    CodeBehind="Cart.aspx.cs" Inherits="Assignment_Web_Application.Pages.Cart" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <!-- Empty Cart Message -->
    <asp:Panel ID="pnlEmpty" runat="server" Visible="false" CssClass="alert alert-info text-center mt-5">
        Your cart is empty.
    </asp:Panel>

    <!-- Cart Items -->
    <asp:Repeater ID="rptCart" runat="server" OnItemCommand="rptCart_ItemCommand">
        <HeaderTemplate>
            <div class="table-responsive mt-4">
                <table class="table align-middle table-hover">
                    <thead class="table-light">
                        <tr>
                            <th>Book</th>
                            <th style="width:120px;">Quantity</th>
                            <th>Price</th>
                            <th>Subtotal</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <div class="d-flex align-items-center">
                        <img src='<%# Eval("CoverImage") %>' alt="Cover" class="me-3" style="width:64px;height:64px;object-fit:cover;" />
                        <div>
                            <div class="fw-semibold"><%# Eval("Title") %></div>
                            <small class="text-muted">ISBN: <%# Eval("ISBN") %></small>
                        </div>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="txtQty" runat="server" Text='<%# Eval("Quantity") %>' 
                        CssClass="form-control form-control-sm" TextMode="Number" />
                </td>
                <td>$<%# Eval("Price", "{0:F2}") %></td>
                <td>$<%# (Convert.ToDecimal(Eval("Price")) * Convert.ToInt32(Eval("Quantity"))).ToString("F2") %></td>
                <td class="text-end">
                    <asp:LinkButton runat="server" CommandName="update" CommandArgument='<%# Eval("BookID") %>' 
                        CssClass="btn btn-sm btn-outline-primary me-2">Update</asp:LinkButton>
                    <asp:LinkButton runat="server" CommandName="remove" CommandArgument='<%# Eval("BookID") %>' 
                        CssClass="btn btn-sm btn-outline-danger">Remove</asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
                    </tbody>
                </table>
            </div>
        </FooterTemplate>
    </asp:Repeater>

    <!-- Cart Summary -->
    <asp:Panel ID="pnlSummary" runat="server" CssClass="d-flex justify-content-end mt-4" Visible="false">
        <div class="card p-3 shadow-sm" style="min-width:300px;">
            <div class="d-flex justify-content-between mb-3">
                <span class="fw-semibold">Total</span>
                <span class="fw-bold text-primary">$<asp:Literal ID="litTotal" runat="server" /></span>
            </div>
            <div class="d-grid gap-2">
                <a href="/Pages/Books.aspx" class="btn btn-outline-secondary">Continue Shopping</a>
                <asp:Button ID="btnCheckout" runat="server" Text="Proceed to Checkout" CssClass="btn btn-primary" OnClick="btnCheckout_Click" />
            </div>
        </div>
    </asp:Panel>

</asp:Content>

