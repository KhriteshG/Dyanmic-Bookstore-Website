<%@ Page Title="BookSmart | Home" Language="C#" MasterPageFile="~/Pages/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Assignment_Web_Application.Pages.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content="BookSmart – Your one-stop bookstore for bestsellers, rare editions, and exclusive collections.">
    <style>
        .book-card {
            height: 100%;
            display: flex;
            flex-direction: column;
        }
        .book-title {
            min-height: 48px;
            overflow-wrap: break-word;
            display: -webkit-box;
            -webkit-line-clamp: 2; 
            -webkit-box-orient: vertical;
            overflow: hidden;
        }
        .card-body {
            flex-grow: 1;
            display: flex;
            flex-direction: column;
        }
        .staff-title {
            min-height: 48px;
            overflow-wrap: break-word;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Hero Section -->
    <section class="position-relative text-center text-white" 
             style="background: url('/Images/BackGround.jpg') center/cover no-repeat; height: 60vh;" 
             role="banner">
        <div class="d-flex flex-column justify-content-center align-items-center h-100 bg-dark bg-opacity-50">
            <h1 class="display-4 fw-bold">Discover Your Next Favorite Book</h1>
            <p class="lead mb-4">Exclusive collectible editions & bestsellers</p>
            <a href="/Pages/Books.aspx" class="btn btn-lg btn-primary px-5 shadow">Browse Books</a>
        </div>
    </section>

    <!-- Featured Books -->
    <section class="py-5" aria-labelledby="featured-books">
        <div class="container">
            <h2 id="featured-books" class="mb-4 text-center fw-bold">Featured Books</h2>
            <div class="row g-4">
                <asp:Repeater ID="rptFeaturedBooks" runat="server">
                    <ItemTemplate>
                        <div class="col-6 col-md-4 col-lg-3">
                            <div class="card border-0 shadow-sm book-card">
                                <img src='<%# Eval("CoverImage") %>' 
                                     alt='<%# Eval("Title") %>' 
                                     loading="lazy"
                                     class="card-img-top" 
                                     style="height: 250px; object-fit: cover;">
                                <div class="card-body">
                                    <h5 class="card-title book-title"><%# Eval("Title") %></h5>
                                    <div class="mt-auto">
                                        <span class="fw-bold text-primary">$<%# Eval("Price", "{0:F2}") %></span>
                                        <a href='/Pages/BookDetail.aspx?id=<%# Eval("BookID") %>' 
                                           class="btn btn-sm btn-outline-primary mt-2 w-100">View Details</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>

    <!-- Staff Picks: 4 Random High-Rated Books -->
    <section class="py-5" aria-labelledby="staff-picks">
        <div class="container">
            <h2 id="staff-picks" class="mb-4 text-center fw-bold">Staff Picks</h2>
            <div class="row g-4" id="staffPicksRow">
                <asp:Repeater ID="rptStaffPicks" runat="server">
                    <ItemTemplate>
                        <div class="col-6 col-md-3">
                            <div class="card border-0 shadow-sm h-100">
                                <img src='<%# Eval("CoverImage") %>' 
                                     alt='<%# Eval("Title") %>' 
                                     class="card-img-top" 
                                     style="height:250px; object-fit:cover;" />
                                <div class="card-body d-flex flex-column">
                                    <h6 class="card-title staff-title"><%# Eval("Title") %></h6>
                                    <span class="text-primary fw-bold mb-2">$<%# Eval("Price", "{0:F2}") %></span>
                                    <a class="btn btn-sm btn-outline-primary mt-auto w-100" 
                                       href='<%# "/Pages/BookDetail.aspx?id=" + Eval("BookID") %>'>View Details</a>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>

</asp:Content>


