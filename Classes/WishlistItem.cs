using System;
using System.Data.SqlClient;

public class WishlistItem
{
    public int WishlistItemID { get; set; }
    public int WishlistID { get; set; }
    public int BookID { get; set; }
    public DateTime DateAddedTimeStamp { get; set; }
    public DateTime LastModified { get; set; }
    public string Status { get; set; }

    public WishlistItem() { }

    public WishlistItem(SqlDataReader reader)
    {
        WishlistItemID = reader.GetInt32(reader.GetOrdinal("WishlistItemID"));
        WishlistID = reader.GetInt32(reader.GetOrdinal("WishlistID"));
        BookID = reader.GetInt32(reader.GetOrdinal("BookID"));
        DateAddedTimeStamp = reader.GetDateTime(reader.GetOrdinal("DateAddedTimeStamp"));
        LastModified = reader.GetDateTime(reader.GetOrdinal("LastModified"));
        Status = reader.GetString(reader.GetOrdinal("Status"));
    }
}

