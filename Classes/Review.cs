using System;
using System.Data.SqlClient;

public class Review
{
    public int ReviewID { get; set; }
    public int BookID { get; set; }
    public int UserID { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime DateAddedTimeStamp { get; set; }
    public DateTime LastModified { get; set; }
    public string Status { get; set; }

    public Review() { }

    public Review(SqlDataReader reader)
    {
        ReviewID = reader.GetInt32(reader.GetOrdinal("ReviewID"));
        BookID = reader.GetInt32(reader.GetOrdinal("BookID"));
        UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
        Rating = reader.GetInt32(reader.GetOrdinal("Rating"));
        Comment = reader.IsDBNull(reader.GetOrdinal("Comment")) ? null : reader.GetString(reader.GetOrdinal("Comment"));
        DateAddedTimeStamp = reader.GetDateTime(reader.GetOrdinal("DateAddedTimeStamp"));
        LastModified = reader.GetDateTime(reader.GetOrdinal("LastModified"));
        Status = reader.GetString(reader.GetOrdinal("Status"));
    }
}