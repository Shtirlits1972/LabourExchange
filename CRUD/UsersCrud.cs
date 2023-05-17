using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using LabourExchange.Model;

namespace LabourExchange.CRUD
{
    public class UsersCrud
    {
        public static readonly string strConn = Ut.strConn;
        public static List<Users> GetAll()
        {
            List<Users> list = new List<Users>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Users>("SELECT Id, [Login], [Password],  [Role] FROM Users").ToList();
            }

            return list;
        }

        public static List<Users> GetUsersWithoutAnkets()
        {
            List<Users> list = new List<Users>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Users>(" SELECT Id, [Login], [Password],  [Role] FROM Users WHERE Id NOT IN (SELECT A.UserId FROM Anketa A) AND [Role] = 'user' ; ").ToList();
            }

            return list;
        }

        public static Users Login(string strLogin, string strPassword)
        {
            Users model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<Users>("SELECT TOP 1 Id, [Login], [Password],  [Role] FROM Users WHERE [Login] = @Login AND [Password] = @Password;", new { Login = strLogin, Password  = strPassword }).FirstOrDefault();
            }
            return model;
        }

        public static Users GetOne(int Id)
        {
            Users model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<Users>("SELECT Id, [Login], [Password], [Role] FROM Users WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM Users WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(Users model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE Users SET [Login] = @Login, [Password] = @Password,  [Role] = @Role WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static Users Add(Users model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO Users ([Login], [Password],  [Role]) VALUES(@Login, @Password,  @Role); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
