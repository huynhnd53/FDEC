using DataLibrary.DataAccess;
using DataLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLibrary.BusinessRule
{
    public static class UserProcessor
    {
        public static int CreateAccount(string username, string password)
        {
            UserModel data = new UserModel
            {
                Username = username,
                Password = password
            };
            string sql = @"INSERT INTO dbo.Users VALUES(@Username, @Password)";
            return SqlDataAccess.InsertData(sql, data);
        }

        public static bool IsValidLogin(string username, string password)
        {
            UserModel data = new UserModel
            {
                Username = username,
                Password = password
            };
            string sql = @"SELECT * FROM dbo.Users WHERE Username = @Username and Password = @Password";
            UserModel result = SqlDataAccess.FindData<UserModel>(sql, data);
            return result != null;
        }

        public static bool IsExistedUsername(string username)
        {
            UserModel data = new UserModel { Username = username };
            string sql = @"SELECT * FROM dbo.Users WHERE Username = @Username";
            UserModel result = SqlDataAccess.FindData<UserModel>(sql, data);
            return result != null;
        }

        public static UserModel GetAccountIdByUsername(string username)
        {
            UserModel data = new UserModel
            {
                Username = username,
            };
            string sql = @"SELECT * FROM dbo.Users WHERE Username = @Username";
            UserModel result = SqlDataAccess.FindData<UserModel>(sql, data);
            return SqlDataAccess.FindData<UserModel>(sql, data);
        }

        public static int UpdateAccount(string username, string password)
        {
            UserModel data = new UserModel
            {
                Username = username,
                Password = password
            };
            string sql = @"UPDATE dbo.Users SET Password = @Password WHERE Username = @Username";
            return SqlDataAccess.InsertData(sql, data);
        }
        public static bool IsEmailOfUsername(string username, string email)
        {
            ForgotPassModel data = new ForgotPassModel { Username = username, Email = email };
            string sql = @"
SELECT * FROM Users s
  WHERE s.Email = @Email
  AND s.Username = @Username";
            ForgotPassModel result = SqlDataAccess.FindData<ForgotPassModel>(sql, data);

            return result != null;
        }
        public static int UpdatePassword(string username, string password)
        {
            ForgotPassModel data = new ForgotPassModel { Username = username, Password = password };
            string sql = @"UPDATE Users
					SET Password =@Password
					WHERE Username like @Username";
            return SqlDataAccess.InsertData(sql, data);
        }
    }
}