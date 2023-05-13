using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabourExchange.Model;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace LabourExchange.CRUD
{
    public class FamilyStatusCrud
    {
        public static readonly string strConn = Ut.strConn;
        public static List<FamilyStatus> GetAll()
        {
            List<FamilyStatus> list = new List<FamilyStatus>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<FamilyStatus>("SELECT Id, Name FROM FamilyStatus").ToList();
            }

            return list;
        }
        public static FamilyStatus GetOne(int Id)
        {
            FamilyStatus model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<FamilyStatus>("SELECT Id, Name FROM FamilyStatus WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM FamilyStatus WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(FamilyStatus model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE FamilyStatus SET Name = @Name WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static FamilyStatus Add(FamilyStatus model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO FamilyStatus (Name) VALUES(@Name); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
