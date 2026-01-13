using System;
using System.Data.SqlClient;

public class Order
{
    public int OrderID { get; set; }
    public int UserID { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string PaymentStatus { get; set; }
    public string ShippingAddress { get; set; }
    public DateTime DateAddedTimeStamp { get; set; }
    public DateTime LastModified { get; set; }
    public string Status { get; set; }

    public Order() { }

    public Order(SqlDataReader reader)
    {
        OrderID = reader.GetInt32(reader.GetOrdinal("OrderID"));
        UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
        OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate"));
        TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount"));
        PaymentStatus = reader.GetString(reader.GetOrdinal("PaymentStatus"));
        ShippingAddress = reader.IsDBNull(reader.GetOrdinal("ShippingAddress")) ? null : reader.GetString(reader.GetOrdinal("ShippingAddress"));
        DateAddedTimeStamp = reader.GetDateTime(reader.GetOrdinal("DateAddedTimeStamp"));
        LastModified = reader.GetDateTime(reader.GetOrdinal("LastModified"));
        Status = reader.GetString(reader.GetOrdinal("Status"));
    }
}