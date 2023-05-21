using LabourExchange.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Xml.Linq;


namespace LabourExchange.CRUD
{
    public class AnketaCrud
    {
        public static readonly string strConn = Ut.strConn;

        public static List<Anketa> GetAll_ForNew_Bezwork()
        {
            List<Anketa> list = new List<Anketa>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Anketa>("SELECT Id, Fam, Name, Otch, Pasport, KolYear, Email, Telephone, UserId, Birthday FROM Anketa   WHERE Id NOT IN ( SELECT DISTINCT B.AnketaId FROM BezWork B ) ; ").ToList();
            }

            return list;
        }
        public static List<Anketa> GetAll()
        {
            List<Anketa> list = new List<Anketa>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Anketa>("SELECT Id, Fam, Name, Otch, Pasport, KolYear, Email, Telephone, UserId, Birthday FROM Anketa").ToList();
            }
                  
            return list;
        }
        public static Anketa GetByUserId(int UserId)
        {
            Anketa model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<Anketa>("SELECT TOP 1 Id,  Fam, Name, Otch, Pasport, KolYear, Email, Telephone, UserId, Birthday FROM Anketa WHERE UserId = @UserId; ", new { UserId }).FirstOrDefault();
            }
            return model;
        }

        public static Anketa GetOne(int Id)
        {
            Anketa model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<Anketa>("SELECT Id,  Fam, Name, Otch, Pasport, KolYear, Email, Telephone, UserId, Birthday FROM Anketa WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM Anketa WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(Anketa model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE Anketa SET  Fam = @Fam , Name = @Name, Otch = @Otch, Pasport = @Pasport, KolYear = @KolYear, Email = @Email, Telephone = @Telephone, UserId = @UserId, Birthday = @Birthday WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static Anketa Add(Anketa model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO Anketa ( Fam, Name, Otch, Pasport, KolYear, Email, Telephone, UserId, Birthday) VALUES(@Fam, @Name, @Otch, @Pasport, @KolYear, @Email, @Telephone, @UserId, @Birthday); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
