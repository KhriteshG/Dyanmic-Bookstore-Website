<%@ Page Title="Books | BookSmart" Language="C#" MasterPageFile="~/Pages/Site1.Master" AutoEventWireup="true" CodeBehind="Books.aspx.cs" Inherits="Assignment_Web_Application.Pages.Books" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        
        .book-title {
            min-height: 48px;
            display: -webkit-box;
            -webkit-line-clamp: 2;
            -webkit-box-orient: vertical;
            overflow: hidden;
            font-weight: 600;
        }

        
        .book-card {
            transition: transform 0.2s ease, box-shadow 0.2s ease;
        }
        .book-card:hover {
            transform: translateY(-4px);
            box-shadow: 0 6px 16px rgba(0, 0, 0, 0.15);
        }

        
        .book-card img {
            height: 250px;
            object-fit: cover;
            border-top-left-radius: 0.5rem;
            border-top-right-radius: 0.5rem;
        }

        
        .book-price {
            font-size: 1rem;
            color: #d63384;
            font-weight: bold;
        }

        
        .filters label {
            font-weight: 500;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <div class="filters row g-3 align-items-end mb-4">
        <div class="col-md-4">
            <label class="form-label">Search</label>
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Title, Author, ISBN..." />
        </div>
        <div class="col-md-3">
            <label class="form-label">Genre</label>
            <asp:DropDownList ID="ddlGenre" runat="server" CssClass="form-select" />
        </div>
        <div class="col-md-3">
            <label class="form-label">Sort by</label>
            <asp:DropDownList ID="ddlSort" runat="server" CssClass="form-select">
                <asp:ListItem Text="Newest" Value="newest" Selected="True" />
                <asp:ListItem Text="Price: Low to High" Value="plh" />
                <asp:ListItem Text="Price: High to Low" Value="phl" />
                <asp:ListItem Text="Title (A–Z)" Value="title" />
            </asp:DropDownList>
        </div>
        <div class="col-md-2 d-grid">
            <asp:LinkButton ID="btnApply" runat="server" CssClass="btn btn-primary" OnClick="btnApply_Click">
                <i class="bi bi-funnel"></i> Apply
            </asp:LinkButton>
        </div>
    </div>

    <!-- Books grid -->
    <div class="row g-4">
        <asp:Repeater ID="rptBooks" runat="server">
            <ItemTemplate>
                <div class="col-6 col-md-4 col-lg-3">
                    <div class="card book-card border-0 shadow-sm h-100">
                        <img src='<%# Eval("CoverImage") %>' alt='<%# Eval("Title") %>' class="card-img-top">
                        <div class="card-body d-flex flex-column">
                            <h6 class="book-title"><%# Eval("Title") %></h6>
                            <span class="book-price mb-2">$<%# Eval("Price", "{0:F2}") %></span>
                            <a class="btn btn-sm btn-outline-primary mt-auto w-100" 
                               href='<%# "/Pages/BookDetail.aspx?id=" + Eval("BookID") %>'>
                                View Details
                            </a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <!-- Pager -->
    <div class="d-flex justify-content-between align-items-center mt-4">
        <asp:Label ID="lblResults" runat="server" CssClass="text-muted"></asp:Label>
        <div>
            <asp:LinkButton ID="btnPrev" runat="server" CssClass="btn btn-outline-secondary me-2" OnClick="btnPrev_Click">
                <i class="bi bi-chevron-left"></i> Previous
            </asp:LinkButton>
            <asp:LinkButton ID="btnNext" runat="server" CssClass="btn btn-outline-secondary" OnClick="btnNext_Click">
                Next <i class="bi bi-chevron-right"></i>
            </asp:LinkButton>
        </div>
    </div>

</asp:Content>
