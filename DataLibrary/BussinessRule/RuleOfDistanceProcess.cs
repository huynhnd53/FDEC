using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Resources;
using System.Text;
using DataLibrary.DataAccess;
using DataLibrary.Models;

namespace DataLibrary.BusinessRule
{
    public static class RuleOfDistanceProcess
    {
        public static RuleOfDistanceModel GetRuleOfDistance(int addressFrom, int addressOder)
        {
            RuleOfDistanceModel data = new RuleOfDistanceModel
            {
                AddressFrom = addressFrom,
                AddressOrder = addressOder
            };
            string sql = @"SELECT * FROM RuleOfDistance
  WHERE AddressFrom = @AddressFrom 
  AND AddressOrder = @AddressOrder";
            RuleOfDistanceModel result = SqlDataAccess.FindData<RuleOfDistanceModel>(sql, data);
            return result;
        }

       

    }
}