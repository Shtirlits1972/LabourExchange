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

        public static CheckResult CanDelete(int AnketaId)
        {
            CheckResult checkResult = new CheckResult {flag = true, Reason = string.Empty };
            bool bezFlag = checkBezWork(AnketaId);
            bool benefitFlag = checkBenefit(AnketaId);

            if(!bezFlag)
            {
                checkResult.flag = false; 
                checkResult.Reason += "Необходимо удалить безработного\r\n";
            }

            if(!benefitFlag)
            {
                checkResult.flag = false;
                checkResult.Reason += "Необходимо удалить пособие\r\n";
            }

            return checkResult;
        }

        private static bool checkBenefit(int AnketaId)
        {
            bool flag = false;
            using (IDbConnection db = new SqlConnection(strConn))
            {
                int count = db.Query<int>(" SELECT * FROM Benefit WHERE AnketaId = @AnketaId; ", new { AnketaId }).FirstOrDefault();

                if (count == 0)
                {
                    return true;
                }
                else if (count > 0)
                {
                    return false;
                }
            }
            return flag;
        }

        private static bool checkBezWork(int AnketaId)
        {
            bool flag = false;
            using (IDbConnection db = new SqlConnection(strConn))
            {
                int count = db.Query<int>("select COUNT(*)q from BezWork where AnketaId = @AnketaId; ", new { AnketaId }).FirstOrDefault();

                if (count == 0)
                {
                    return true;
                }
                else if (count > 0)
                {
                    return false;
                }
            }
            return flag;
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

                try
                {
                    db.Execute(Query, model);
                }
                catch (Exception ex)
                {
                    string Error = ex.Message;
                }

            }
        }
        public static Anketa Add(Anketa model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO Anketa ( Fam, Name, Otch, Pasport, KolYear, Email, Telephone, UserId, Birthday) VALUES(@Fam, @Name, @Otch, @Pasport, @KolYear, @Email, @Telephone, @UserId, @Birthday); SELECT CAST(SCOPE_IDENTITY() as int)";

                try
                {
                    int Id = db.Query<int>(Query, model).FirstOrDefault();
                    model.Id = Id;
                }
                catch (Exception ex)
                {
                    string Error = ex.Message;
                }
            }
            return model;
        }

        #region check before Update
        public static bool checkPassportEdit(string Pasport, int Id)
        {
            bool flag = false;
            using (IDbConnection db = new SqlConnection(strConn))
            {
                int q = db.Query<int>(" SELECT COUNT(*)q from Anketa WHERE UPPER(Pasport) = UPPER(@Pasport)  AND Id != @Id ; ", new { Pasport, Id }).FirstOrDefault();

                if (q > 0)
                {
                    flag = true;
                }
            }

            return flag;
        }

        public static bool checkTelephoneEdit(string Telephone, int Id)
        {
            bool flag = false;
            using (IDbConnection db = new SqlConnection(strConn))
            {
                int q = db.Query<int>(" SELECT COUNT(*)q from Anketa WHERE UPPER(Telephone) = UPPER(@Telephone)   AND Id != @Id ; ", new { Telephone, Id }).FirstOrDefault();

                if (q > 0)
                {
                    flag = true;
                }
            }

            return flag;
        }

        public static bool checkUserIdEdit(int UserId, int Id)
        {
            bool flag = false;
            using (IDbConnection db = new SqlConnection(strConn))
            {
                int q = db.Query<int>(" SELECT COUNT(*)q from Anketa WHERE UserId = @UserId  AND Id != @Id ; ", new { UserId, Id }).FirstOrDefault();

                if (q > 0)
                {
                    flag = true;
                }
            }

            return flag;
        }
        #endregion

        #region check before Insert
        public static bool checkPassport(string Pasport)
        {
            bool flag = false;
            using (IDbConnection db = new SqlConnection(strConn))
            {
                int q = db.Query<int>(" SELECT COUNT(*)q from Anketa WHERE UPPER(Pasport) = UPPER(@Pasport) ; ", new { Pasport }).FirstOrDefault();

                if (q > 0)
                {
                    flag = true;
                }
            }

            return flag;
        }

        public static bool checkTelephone(string Telephone)
        {
            bool flag = false;
            using (IDbConnection db = new SqlConnection(strConn))
            {
                int q = db.Query<int>(" SELECT COUNT(*)q from Anketa WHERE UPPER(Telephone) = UPPER(@Telephone) ; ", new { Telephone }).FirstOrDefault();

                if (q > 0)
                {
                    flag = true;
                }
            }

            return flag;
        }

        public static bool checkUserId(int UserId)
        {
            bool flag = false;
            using (IDbConnection db = new SqlConnection(strConn))
            {
                int q = db.Query<int>(" SELECT COUNT(*)q from Anketa WHERE UserId = @UserId ; ", new { UserId }).FirstOrDefault();

                if (q > 0)
                {
                    flag = true;
                }
            }

            return flag;
        }
        #endregion

        public class CheckResult
        {
            public  bool flag { get; set; } = false;
            public  string Reason { get; set; } = string.Empty;
        }
    }
}
