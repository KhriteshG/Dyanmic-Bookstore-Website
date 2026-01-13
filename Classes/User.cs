using System;
using System.Data.SqlClient;

namespace Assignment_Web_Application.Objects
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public int LoyaltyCardPoints { get; set; }
        public DateTime DateAddedTimeStamp { get; set; }
        public DateTime LastModified { get; set; }
        public string Status { get; set; }

        public User() { }

        public User(SqlDataReader reader)
        {
            UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
            Username = reader.GetString(reader.GetOrdinal("Username"));
            PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"));
            Email = reader.GetString(reader.GetOrdinal("Email"));
            FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? null : reader.GetString(reader.GetOrdinal("FirstName"));
            LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? null : reader.GetString(reader.GetOrdinal("LastName"));
            Role = reader.GetString(reader.GetOrdinal("Role"));
            LoyaltyCardPoints = reader.GetInt32(reader.GetOrdinal("LoyaltyCardPoints"));
            DateAddedTimeStamp = reader.GetDateTime(reader.GetOrdinal("DateAddedTimeStamp"));
            LastModified = reader.GetDateTime(reader.GetOrdinal("LastModified"));
            Status = reader.GetString(reader.GetOrdinal("Status"));
        }
    }
}

    
