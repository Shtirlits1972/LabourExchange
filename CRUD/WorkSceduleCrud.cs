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
    public class WorkSceduleCrud
    {
        public static readonly string strConn = Ut.strConn;
        public static List<WorkScedule> GetAll()
        {
            List<WorkScedule> list = new List<WorkScedule>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<WorkScedule>("SELECT Id, Name, duration FROM WorkScedule").ToList();
            }

            return list;
        }
        public static WorkScedule GetOne(int Id)
        {
            WorkScedule model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<WorkScedule>("SELECT Id, Name, duration FROM WorkScedule WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM WorkScedule WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(WorkScedule model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE WorkScedule SET Name = @Name, duration = @duration WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static WorkScedule Add(WorkScedule model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO WorkScedule (Name, duration) VALUES(@Name, @duration); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
