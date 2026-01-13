using System;
using System.Data.SqlClient;

public class Author
{
    public int AuthorID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Bio { get; set; }
    public DateTime DateAddedTimeStamp { get; set; }
    public DateTime LastModified { get; set; }
    public string Status { get; set; }

    public Author() { }

    public Author(SqlDataReader reader)
    {
        AuthorID = reader.GetInt32(reader.GetOrdinal("AuthorID"));
        FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
        LastName = reader.GetString(reader.GetOrdinal("LastName"));
        Bio = reader.IsDBNull(reader.GetOrdinal("Bio")) ? null : reader.GetString(reader.GetOrdinal("Bio"));
        DateAddedTimeStamp = reader.GetDateTime(reader.GetOrdinal("DateAddedTimeStamp"));
        LastModified = reader.GetDateTime(reader.GetOrdinal("LastModified"));
        Status = reader.GetString(reader.GetOrdinal("Status"));
    }
}
