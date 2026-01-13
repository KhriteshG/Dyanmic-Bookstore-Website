using System;
using System.Data.SqlClient;

public class Book
{
    public int BookID { get; set; }
    public string Title { get; set; }
    public int GenreID { get; set; }
    public int AuthorID { get; set; }
    public int PublisherID { get; set; }
    public string ISBN { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public byte[] CoverImage { get; set; }
    public DateTime DateAddedTimeStamp { get; set; }
    public DateTime LastModified { get; set; }
    public string Status { get; set; }

    public Book() { }

    public Book(SqlDataReader reader)
    {
        BookID = reader.GetInt32(reader.GetOrdinal("BookID"));
        Title = reader.GetString(reader.GetOrdinal("Title"));
        GenreID = reader.GetInt32(reader.GetOrdinal("GenreID"));
        AuthorID = reader.GetInt32(reader.GetOrdinal("AuthorID"));
        PublisherID = reader.GetInt32(reader.GetOrdinal("PublisherID"));
        ISBN = reader.IsDBNull(reader.GetOrdinal("ISBN")) ? null : reader.GetString(reader.GetOrdinal("ISBN"));
        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description"));
        Price = reader.GetDecimal(reader.GetOrdinal("Price"));
        StockQuantity = reader.GetInt32(reader.GetOrdinal("StockQuantity"));
        CoverImage = reader.IsDBNull(reader.GetOrdinal("CoverImage")) ? null : (byte[])reader["CoverImage"];
        DateAddedTimeStamp = reader.GetDateTime(reader.GetOrdinal("DateAddedTimeStamp"));
        LastModified = reader.GetDateTime(reader.GetOrdinal("LastModified"));
        Status = reader.GetString(reader.GetOrdinal("Status"));
    }
}
