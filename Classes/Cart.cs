using System;
using System.Data.SqlClient;

namespace Assignment_Web_Application.Objects
{
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
}
