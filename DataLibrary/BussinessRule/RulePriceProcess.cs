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
    public static class RulePriceProcess
    {
        public static List<RulePriceModel> GetListRulePrices()
        {
            string sql = @"SELECT * FROM RulePrice";
            List<RulePriceModel> lstRule = SqlDataAccess.LoadData<RulePriceModel>(sql);
            if (lstRule != null && lstRule.Count > 0)
            {
                return lstRule;
            }
            return null;
        }
        public static RulePriceModel GetRulePricesByID(int id)
        {
            string sql = @"SELECT * FROM RulePrice WHERE [ID] = '"+id+"'";
            List<RulePriceModel> lstRule = SqlDataAccess.LoadData<RulePriceModel>(sql);
            if (lstRule != null && lstRule.Count == 1)
            {
                return lstRule[0];
            }
            return null;
        }
    }
}