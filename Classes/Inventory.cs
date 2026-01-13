using System;
using System.Data.SqlClient;

namespace Assignment_Web_Application.Objects
{
    public class Inventory
    {
        public int InventoryID { get; set; }
        public int BookID { get; set; }
        public int QuantityInStock { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Status { get; set; }

        public Inventory() { }

        public Inventory(SqlDataReader reader)
        {
            InventoryID = reader.GetInt32(reader.GetOrdinal("InventoryID"));
            BookID = reader.GetInt32(reader.GetOrdinal("BookID"));
            QuantityInStock = reader.GetInt32(reader.GetOrdinal("QuantityInStock"));
            LastUpdated = reader.GetDateTime(reader.GetOrdinal("LastUpdated"));
            Status = reader.GetString(reader.GetOrdinal("Status"));
        }
    }
}
