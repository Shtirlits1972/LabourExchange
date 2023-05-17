using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using LabourExchange.Model;
using System.Xml.Linq;

namespace LabourExchange.CRUD
{
    public class BenefitCrud
    {
        public static readonly string strConn = Ut.strConn;
        public static List<Benefit> GetAll()
        {
            List<Benefit> list = new List<Benefit>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Benefit>("SELECT Id, AnketaId, AnketaName, Val, Data_Vyplaty, Data_Postanovki, Deskr  FROM BenefitView ").ToList();
            }

            return list;
        }

        public static Benefit GetOne(int Id)
        {
            Benefit model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<Benefit>("SELECT Id, AnketaId, AnketaName, Val, Data_Vyplaty, Data_Postanovki, Deskr FROM BenefitView WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM Benefit WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(Benefit model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE Benefit SET AnketaId = @AnketaId, Val = @Val, Data_Vyplaty = @Data_Vyplaty, Data_Postanovki = @Data_Postanovki, Deskr = @Deskr WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static Benefit Add(Benefit model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO Benefit (AnketaId, Val, Data_Vyplaty, Data_Postanovki, Deskr) VALUES(@AnketaId, @Val, @Data_Vyplaty, @Data_Postanovki, @Deskr); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
