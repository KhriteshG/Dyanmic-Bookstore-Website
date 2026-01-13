using System;
using System.Data.SqlClient;

public class Genre
{
    public int GenreID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateAddedTimeStamp { get; set; }
    public DateTime LastModified { get; set; }
    public string Status { get; set; }

    public Genre() { }

    public Genre(SqlDataReader reader)
    {
        GenreID = reader.GetInt32(reader.GetOrdinal("GenreID"));
        Name = reader.GetString(reader.GetOrdinal("Name"));
        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description"));
        DateAddedTimeStamp = reader.GetDateTime(reader.GetOrdinal("DateAddedTimeStamp"));
        LastModified = reader.GetDateTime(reader.GetOrdinal("LastModified"));
        Status = reader.GetString(reader.GetOrdinal("Status"));
    }
}
