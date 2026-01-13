using System;
using System.Data.SqlClient;

namespace Assignment_Web_Application.Objects
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int OrderID { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public string TransactionID { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateAddedTimeStamp { get; set; }
        public DateTime LastModified { get; set; }
        public string Status { get; set; }

        public Payment() { }

        public Payment(SqlDataReader reader)
        {
            PaymentID = reader.GetInt32(reader.GetOrdinal("PaymentID"));
            OrderID = reader.GetInt32(reader.GetOrdinal("OrderID"));
            PaymentMethod = reader.GetString(reader.GetOrdinal("PaymentMethod"));
            PaymentStatus = reader.GetString(reader.GetOrdinal("PaymentStatus"));
            PaymentDate = reader.GetDateTime(reader.GetOrdinal("PaymentDate"));
            TransactionID = reader.IsDBNull(reader.GetOrdinal("TransactionID")) ? null : reader.GetString(reader.GetOrdinal("TransactionID"));
            Amount = reader.GetDecimal(reader.GetOrdinal("Amount"));
            DateAddedTimeStamp = reader.GetDateTime(reader.GetOrdinal("DateAddedTimeStamp"));
            LastModified = reader.GetDateTime(reader.GetOrdinal("LastModified"));
            Status = reader.GetString(reader.GetOrdinal("Status"));
        }
    }
}
