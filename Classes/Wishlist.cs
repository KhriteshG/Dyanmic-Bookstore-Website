using System;
using System.Data.SqlClient;

public class Wishlist
{
    public int WishlistID { get; set; }
    public int UserID { get; set; }
    public DateTime DateAddedTimeStamp { get; set; }
    public DateTime LastModified { get; set; }
    public string Status { get; set; }

    public Wishlist() { }

    public Wishlist(SqlDataReader reader)
    {
        WishlistID = reader.GetInt32(reader.GetOrdinal("WishlistID"));
        UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
        DateAddedTimeStamp = reader.GetDateTime(reader.GetOrdinal("DateAddedTimeStamp"));
        LastModified = reader.GetDateTime(reader.GetOrdinal("LastModified"));
        Status = reader.GetString(reader.GetOrdinal("Status"));
    }
}

