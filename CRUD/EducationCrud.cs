using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabourExchange.Model;
using Dapper;

namespace LabourExchange.CRUD
{
    public class EducationCrud
    {
        public static readonly string strConn = Ut.strConn;

        public static bool NameIsFree(string Name)
        {
            bool flag = false;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                int count = db.Query<int>(" SELECT COUNT(*)q FROM Education WHERE UPPER([Name]) = UPPER(@Name) ; ", new { Name }).FirstOrDefault();

                if (count == 0)
                {
                    flag = true;
                }
                else if (count > 0)
                {
                    flag = false;
                }
                return flag;
            }
        }
        public static List<Education> GetAll()
        {
            List<Education> list = new List<Education>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Education>("SELECT Id, Name FROM Education").ToList();
            }

            return list;
        }
        public static Education GetOne(int Id)
        {
            Education model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<Education>("SELECT Id, Name FROM Education WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM Education WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(Education model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE Education SET Name = @Name WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static Education Add(Education model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO Education (Name) VALUES(@Name); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }

            return model;
        }
    }
}
