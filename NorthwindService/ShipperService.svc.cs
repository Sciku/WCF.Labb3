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
        Shipper shipper = new Shipper();
        public Shipper GetShipperByID(int ShipperId)
        {
            string getQuery = "SELECT [ShipperID], [CompanyName], [Phone]FROM [NORTHWND].[dbo].[Shippers]" +
                "WHERE [ShipperID] = " + ShipperId;
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(getQuery, connection);
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

        public int SaveShipper(int shipperID, string companyName, string phone)
        {
            string updateQuery = "UPDATE [NORTHWND].[dbo].[Shippers] SET" + 
                " companyName = @CompanyName,"+
                " phone = @Phone" +
                " WHERE shipperID = @ShipperID"; 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                SqlCommand cmd = new SqlCommand(updateQuery, connection);

                SqlParameter paramShipperID = new SqlParameter("@ShipperID", shipperID);
                cmd.Parameters.Add(paramShipperID);

                SqlParameter paramCompanyName = new SqlParameter("@CompanyName", companyName);
                cmd.Parameters.Add(paramCompanyName);

                SqlParameter paramPhone = new SqlParameter("@Phone", phone);
                cmd.Parameters.Add(paramPhone);
       
                connection.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
