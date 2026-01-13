<%@ Page Title="Book Details | BookSmart" Language="C#" MasterPageFile="~/Pages/Site1.Master" AutoEventWireup="true" CodeBehind="BookDetail.aspx.cs" Inherits="Assignment_Web_Application.Pages.BookDetails" %>

<asp:Content ID="headC" ContentPlaceHolderID="head" runat="server">
    <style>
        .review-item { border-bottom: 1px solid #eee; padding: 12px 0; }
        .book-actions .btn { min-width: 120px; }
    </style>
</asp:Content>

<asp:Content ID="mainC" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="pnlNotFound" runat="server" Visible="false" CssClass="alert alert-warning">
        Book not found.
    </asp:Panel>

    <asp:Panel ID="pnlDetails" runat="server" Visible="false">
        <div class="row g-4">
            <!-- Book Cover -->
            <div class="col-md-4">
                <asp:Image ID="imgCover" runat="server" CssClass="img-fluid rounded shadow-sm" />
            </div>

            <!-- Book Info -->
            <div class="col-md-8">
                <h2 class="mb-1"><asp:Literal ID="litTitle" runat="server" /></h2>
                <p class="text-muted mb-2">
                    <asp:Literal ID="litAuthor" runat="server" />
                    &nbsp;•&nbsp; <asp:Literal ID="litGenre" runat="server" />
                    &nbsp;•&nbsp; ISBN: <asp:Literal ID="litISBN" runat="server" />
                </p>
                <h4 class="text-primary">$<asp:Literal ID="litPrice" runat="server" /></h4>
                <p class="mt-3"><asp:Literal ID="litDescription" runat="server" /></p>

                <!-- Add to Cart / Wishlist -->
                <div class="d-flex align-items-center gap-2 mt-3 book-actions">
                    <asp:TextBox ID="txtQty" runat="server" TextMode="Number" Text="1" CssClass="form-control" style="width:90px;" />

                    <asp:LinkButton ID="btnAddToCart" runat="server" CssClass="btn btn-primary" OnClick="btnAddToCart_Click">
                        <i class="bi bi-cart"></i> Add to Cart
                    </asp:LinkButton>

                    <asp:LinkButton ID="btnAddToWishlist" runat="server" CssClass="btn btn-outline-warning" OnClick="btnAddToWishlist_Click" CommandArgument='<%# Eval("BookID") %>'>
                        <i class="bi bi-heart"></i> Wishlist
                    </asp:LinkButton>

                    <asp:Label ID="lblAddMsg" runat="server" CssClass="ms-2"></asp:Label>
                    <asp:Label ID="lblWishlistMsg" runat="server" CssClass="ms-2"></asp:Label>
                </div>
            </div>
        </div>

        <hr class="my-4" />

        <!-- Reviews Section -->
        <div class="row">
            <!-- Existing Reviews -->
            <div class="col-md-6">
                <h5 class="mb-3">Customer Reviews</h5>
                <asp:Repeater ID="rptReviews" runat="server">
                    <ItemTemplate>
                        <div class="review-item">
                            <div class="mb-1">
                                <span class="badge bg-warning text-dark">Rating: <%# Eval("Rating") %>/5</span>
                                <span class="text-muted ms-2"><%# String.Format("{0:yyyy-MM-dd}", Eval("DateAddedTimeStamp")) %></span>
                            </div>
                            <div><%# Eval("Comment") %></div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Label ID="lblNoReviews" runat="server" CssClass="text-muted" Visible="false">No reviews yet.</asp:Label>
            </div>

            <!-- Submit Review -->
            <div class="col-md-6">
                <h5 class="mb-3">Write a Review</h5>
                <asp:Panel ID="pnlReviewForm" runat="server">
                    <label class="form-label">Rating</label>
                    <asp:DropDownList ID="ddlRating" runat="server" CssClass="form-select mb-2">
                        <asp:ListItem Text="1" Value="1" />
                        <asp:ListItem Text="2" Value="2" />
                        <asp:ListItem Text="3" Value="3" />
                        <asp:ListItem Text="4" Value="4" />
                        <asp:ListItem Text="5" Value="5" Selected="True" />
                    </asp:DropDownList>

                    <label class="form-label">Comment</label>
                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control mb-2" />

                    <asp:LinkButton ID="btnSubmitReview" runat="server" CssClass="btn btn-outline-primary" OnClick="btnSubmitReview_Click">Submit Review</asp:LinkButton>
                    <asp:Label ID="lblReviewMsg" runat="server" CssClass="ms-2"></asp:Label>
                </asp:Panel>

                <asp:Label ID="lblLoginToReview" runat="server" CssClass="text-muted" Visible="false">
                    Please <a href="/Pages/Login.aspx">log in</a> to submit a review.
                </asp:Label>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
