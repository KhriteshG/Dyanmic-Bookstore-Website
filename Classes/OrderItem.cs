using System;
using System.Data.SqlClient;

public class OrderItem
{
    public int OrderItemID { get; set; }
    public int OrderID { get; set; }
    public int BookID { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAtPurchase { get; set; }
    public DateTime DateAddedTimeStamp { get; set; }
    public DateTime LastModified { get; set; }
    public string Status { get; set; }

    public OrderItem() { }

    public OrderItem(SqlDataReader reader)
    {
        OrderItemID = reader.GetInt32(reader.GetOrdinal("OrderItemID"));
        OrderID = reader.GetInt32(reader.GetOrdinal("OrderID"));
        BookID = reader.GetInt32(reader.GetOrdinal("BookID"));
        Quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
        PriceAtPurchase = reader.GetDecimal(reader.GetOrdinal("PriceAtPurchase"));
        DateAddedTimeStamp = reader.GetDateTime(reader.GetOrdinal("DateAddedTimeStamp"));
        LastModified = reader.GetDateTime(reader.GetOrdinal("LastModified"));
        Status = reader.GetString(reader.GetOrdinal("Status"));
    }
}