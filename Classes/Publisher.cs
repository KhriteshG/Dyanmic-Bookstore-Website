using System;
using System.Data.SqlClient;

public class Publisher
{
    public int PublisherID { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Website { get; set; }
    public DateTime DateAddedTimeStamp { get; set; }
    public DateTime LastModified { get; set; }
    public string Status { get; set; }

    public Publisher() { }

    public Publisher(SqlDataReader reader)
    {
        PublisherID = reader.GetInt32(reader.GetOrdinal("PublisherID"));
        Name = reader.GetString(reader.GetOrdinal("Name"));
        Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address"));
        Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone"));
        Website = reader.IsDBNull(reader.GetOrdinal("Website")) ? null : reader.GetString(reader.GetOrdinal("Website"));
        DateAddedTimeStamp = reader.GetDateTime(reader.GetOrdinal("DateAddedTimeStamp"));
        LastModified = reader.GetDateTime(reader.GetOrdinal("LastModified"));
        Status = reader.GetString(reader.GetOrdinal("Status"));
    }
}
