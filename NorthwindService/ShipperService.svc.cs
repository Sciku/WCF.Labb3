using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace NorthwindService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ShipperService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ShipperService.svc or ShipperService.svc.cs at the Solution Explorer and start debugging.
    public class ShipperService : IShipperService
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["theDB"].ToString();
        public Shipper GetShipperByID(int ShipperId)
        {
            string query = "SELECT [ShipperID], [CompanyName], [Phone]FROM [NORTHWND].[dbo].[Shippers]" +
                "WHERE [ShipperID] = " + ShipperId;
            var shipper = new Shipper();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    shipper.ShipperID = Convert.ToInt32(reader["ShipperID"].ToString());
                    shipper.CompanyName = reader["CompanyName"].ToString();
                    shipper.Phone = reader["Phone"].ToString();
                }
            }
            return shipper;

        }

        public Shipper SaveShipper(int ShipperID, string CompanyName, string Phone)
        {
            throw new NotImplementedException();
        }
    }
}
