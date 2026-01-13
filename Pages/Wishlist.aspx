<%@ Page Title="My Wishlist | BookSmart" Language="C#" 
    MasterPageFile="~/Pages/Site1.Master" AutoEventWireup="true" 
    CodeBehind="Wishlist.aspx.cs" Inherits="Assignment_Web_Application.Pages.Wishlist" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Empty Wishlist Message -->
    <asp:Panel ID="pnlEmpty" runat="server" Visible="false" CssClass="alert alert-info text-center mt-5">
        Your wishlist is empty.
    </asp:Panel>

    <!-- Wishlist Items -->
    <asp:Repeater ID="rptWishlist" runat="server" OnItemCommand="rptWishlist_ItemCommand">
        <HeaderTemplate>
            <div class="table-responsive mt-4">
                <table class="table align-middle table-hover">
                    <thead class="table-light">
                        <tr>
                            <th>Book</th>
                            <th>Price</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <div class="d-flex align-items-center">
                        <img src='<%# Eval("CoverImageBase64") %>' 
                             alt="Cover" class="me-3" style="width:64px;height:64px;object-fit:cover;" />
                        <div>
                            <div class="fw-semibold"><%# Eval("Title") %></div>
                        </div>
                    </div>
                </td>
                <td>$<%# Eval("Price", "{0:F2}") %></td>
                <td class="text-end">
                    <a href='<%# ResolveUrl("/Pages/BookDetail.aspx?bookId=" + Eval("BookID")) %>' 
                       class="btn btn-sm btn-outline-primary me-2">View</a>
                    <asp:LinkButton runat="server" CommandName="remove" 
                        CommandArgument='<%# Eval("WishlistItemID") %>' 
                        CssClass="btn btn-sm btn-outline-danger"
                        OnClientClick="return confirm('Remove this item from your wishlist?');">
                        Remove
                    </asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
                    </tbody>
                </table>
            </div>
        </FooterTemplate>
    </asp:Repeater>

    <!-- Continue Shopping -->
    <asp:Panel ID="pnlActions" runat="server" CssClass="d-flex justify-content-end mt-4" Visible="true">
        <a href="/Pages/Books.aspx" class="btn btn-outline-secondary">Continue Shopping</a>
    </asp:Panel>

</asp:Content>


