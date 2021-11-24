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
    public static class OrdersProcess
    {
        public static int CreateOrders(OrdersModel data)
        {
            string sql = @"INSERT INTO dbo.Orders VALUES(@CreaterID, @ShipperID, @CreateDate, @ExpiredDate, @SuccessDate, @AddressFrom, @AddressOrder, @StatusOrder,@ShopID, @NumberPhoneOrder, @Cash, @CategoryID, @Size, @WeightID, @IsActive);";
            return SqlDataAccess.InsertData(sql, data);
        }

        public static int UpdateOrder(OrdersModel data)
        {
            string sql =
                @"UPDATE dbo.Orders SET 
[CreaterID] = @CreaterID, [ShipperID] = @ShipperID, [CreateDate] = @CreateDate, [ExprieDate] = @ExprieDate, [AddressFrom] = @AddressFrom, [AddressOrder] = @AddressOrder, [StatusOrder] = @StatusOrder, [ShopID] = @ShopID, [NumberPhoneOrder] = @NumberPhoneOrder, [Cash] = @Cash,[CategoryID] = @CategoryID, [Size] = @Size, [WeightID] = @WeightID 
WHERE [ID] = @ID";
            return SqlDataAccess.InsertData(sql, data);
        }

        public static OrdersModel GetOrderByID(int id)
        {
            string sql =
                @"SELECT * FROM  dbo.Orders 
 WHERE [ID] = @id; ";
            List<OrdersModel> order = SqlDataAccess.LoadData<OrdersModel>(sql);
            if (order != null && order.Count == 1)
                return order[0];
            return null;
        }

        public static List<OrdersModel> GetListOrderByUserID(int id)
        {
            string sql =
                @"SELECT * FROM  dbo.Orders 
 WHERE [CreaterID] = '"+id+"';";
            List<OrdersModel> order = SqlDataAccess.LoadData<OrdersModel>(sql);
            if (order != null && order.Count >= 1)
                return order;
            return null;
        }

        public static int DeleteOrder(OrdersModel data)
        {
            string sql = @"UPDATE dbo.Orders SET [IsActive] = 0 WHERE [ID] = @ID;";
            return SqlDataAccess.InsertData(sql, data);
        }

        

    }
}