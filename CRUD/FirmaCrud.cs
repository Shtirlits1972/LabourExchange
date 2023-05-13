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
    public class FirmaCrud
    {
        public static readonly string strConn = Ut.strConn;
        public static List<Firma> GetAll()
        {
            List<Firma> list = new List<Firma>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Firma>("SELECT Id, Name FROM Firma").ToList();
            }

            return list;
        }
        public static Firma GetOne(int Id)
        {
            Firma model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<Firma>("SELECT Id, Name FROM Firma WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM Firma WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(Firma model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE Firma SET Name = @Name WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static Firma Add(Firma model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO Firma (Name) VALUES(@Name); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
