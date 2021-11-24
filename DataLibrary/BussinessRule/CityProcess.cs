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
    public static class CityProcess
    {
        public static List<CityModel> GetListCity()
        {
            string sql = "SELECT * FROM City";
            List<CityModel> lstCity = SqlDataAccess.LoadData<CityModel>(sql);
            if (lstCity != null && lstCity.Count > 0)
            {
                return lstCity;
            }
            return null;
        }
        public static CityModel GetCityByID(int id)
        {
            string sql = @"Select * from City where [id] = '" + id + "'";
            List<CityModel> lstRule = SqlDataAccess.LoadData<CityModel>(sql);
            if (lstRule != null && lstRule.Count > 0)
            {
                return lstRule[0];
            }
            return null;
        }
    }
}