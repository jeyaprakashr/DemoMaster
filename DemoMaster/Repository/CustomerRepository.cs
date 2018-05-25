using DemoMaster.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DemoMaster.Repository
{
    public class CustomerRepository
    {
        private SqlConnection con;
        private void connection()
        {
            string lstrConString = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            con = new SqlConnection(lstrConString);
        }
        public bool AddCustomer(CustomerMaster cm)
        {
            string constr = ConfigurationManager.ConnectionStrings["FMSARABICEntities"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);

            SqlCommand sCommand = new SqlCommand("stpMAS_SaveCustomer", con);
            sCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sCommand.Parameters.AddWithValue("@CUSTOMERCODE", cm.CustomerCode);
            sCommand.Parameters.AddWithValue("@COMPANYCODE", cm.CompanyCode);
            sCommand.Parameters.AddWithValue("@COMPANYNAME", cm.CompanyName);
            sCommand.Parameters.AddWithValue("@ADDRESS1", cm.Address1);
            sCommand.Parameters.AddWithValue("@ADDRESS2", cm.Address2);
            sCommand.Parameters.AddWithValue("@CITY", cm.City);
            sCommand.Parameters.AddWithValue("@STATECODE", cm.StateCode);
            sCommand.Parameters.AddWithValue("@COUNTRYCODE", cm.CountryCode);
            sCommand.Parameters.AddWithValue("@ZIPCODE", cm.ZipCode);
            sCommand.Parameters.AddWithValue("@PHONE", cm.Phone);
            sCommand.Parameters.AddWithValue("@FAX", cm.Fax);
            sCommand.Parameters.AddWithValue("@EMAIL", cm.Email);
            sCommand.Parameters.AddWithValue("@INACTIVE", cm.InActive);
            sCommand.Parameters.AddWithValue("@CREATEDDATE", cm.CreatedDate);

            con.Open();
            int i = sCommand.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}