using System;
using System.Data.SqlClient;

public class Cart
{
    public int CartID { get; set; }
    public int UserID { get; set; }
    public DateTime DateAddedTimeStamp { get; set; }
    public DateTime LastModified { get; set; }
    public string Status { get; set; }

    public Cart() { }

    public Cart(SqlDataReader reader)
    {
        CartID = reader.GetInt32(reader.GetOrdinal("CartID"));
        UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
        DateAddedTimeStamp = reader.GetDateTime(reader.GetOrdinal("DateAddedTimeStamp"));
        LastModified = reader.GetDateTime(reader.GetOrdinal("LastModified"));
        Status = reader.GetString(reader.GetOrdinal("Status"));
    }
}

public class CartItem
{
    public int CartItemID { get; set; }
    public int CartID { get; set; }
    public int BookID { get; set; }
    public int Quantity { get; set; }
    public DateTime DateAddedTimeStamp { get; set; }
    public DateTime LastModified { get; set; }
    public string Status { get; set; }

    public CartItem() { }

    public CartItem(SqlDataReader reader)
    {
        CartItemID = reader.GetInt32(reader.GetOrdinal("CartItemID"));
        CartID = reader.GetInt32(reader.GetOrdinal("CartID"));
        BookID = reader.GetInt32(reader.GetOrdinal("BookID"));
        Quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
        DateAddedTimeStamp = reader.GetDateTime(reader.GetOrdinal("DateAddedTimeStamp"));
        LastModified = reader.GetDateTime(reader.GetOrdinal("LastModified"));
        Status = reader.GetString(reader.GetOrdinal("Status"));
    }
}
