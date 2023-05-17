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
    public class VacancyCrud
    {
        public static readonly string strConn = Ut.strConn;
        public static List<Vacancy> GetAll()
        {
            List<Vacancy> list = new List<Vacancy>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Vacancy>("SELECT Id, FirmaId, FirmaName, EducationId, EducationName, PositionId, PositionName, WorkSceduleId, WorkSceduleName, UsloviyWorkOplata, Trebovan, Priznak, Sex FROM VacancyView ").ToList();
            }

            return list;
        }

        public static List<Vacancy> GetAll(int EducationId, int PositionId, int AnketaId)
        {
            List<Vacancy> list = new List<Vacancy>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Vacancy>("SELECT Id, FirmaId, FirmaName, EducationId, EducationName, PositionId, PositionName, WorkSceduleId, WorkSceduleName, UsloviyWorkOplata, Trebovan, Priznak, Sex FROM VacancyView WHERE (EducationId = @EducationId) AND (PositionId = @PositionId) "
                    + " AND Id NOT IN ( SELECT VacancyId FROM AnketaVacancyLink WHERE AnketaId = @AnketaId ) ; ", new { EducationId, PositionId, AnketaId }).ToList();
            }

            return list;
        }

        public static List<Vacancy> GetAll(int EducationId, string Sex)
        {
            List<Vacancy> list = new List<Vacancy>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Vacancy>("SELECT Id, FirmaId, FirmaName, EducationId, EducationName, PositionId, PositionName, WorkSceduleId, WorkSceduleName, UsloviyWorkOplata, Trebovan, Priznak, Sex FROM VacancyView WHERE ((EducationId = @EducationId) OR (@EducationId = 0)) AND (Sex = @Sex OR (@Sex = 'Все'));", new { EducationId, Sex }).ToList();
            }

            return list;
        }
        public static Vacancy GetOne(int Id)
        {
            Vacancy model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<Vacancy>("SELECT Id,  FirmaId, FirmaName, EducationId, EducationName, PositionId, PositionName, WorkSceduleId, WorkSceduleName, UsloviyWorkOplata, Trebovan, Priznak, Sex FROM VacancyView WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM Vacancy WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(Vacancy model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE Vacancy SET FirmaId = @FirmaId, EducationId = @EducationId, PositionId = @PositionId, WorkSceduleId = @WorkSceduleId, UsloviyWorkOplata = @UsloviyWorkOplata, Trebovan = @Trebovan, Priznak = @Priznak, Sex = @Sex WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static Vacancy Add(Vacancy model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO Vacancy ( FirmaId, EducationId, PositionId, WorkSceduleId, UsloviyWorkOplata, Trebovan, Priznak, Sex ) VALUES(@FirmaId, @EducationId, @PositionId, @WorkSceduleId, @UsloviyWorkOplata, @Trebovan, @Priznak, @Sex); SELECT CAST(SCOPE_IDENTITY() as int )";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }

        public static void AddAnketaVacancyLink(int VacancyId, int AnketaId)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Query(" INSERT INTO AnketaVacancyLink ( VacancyId, AnketaId ) VALUES(@VacancyId, @AnketaId);  ", new { VacancyId, AnketaId });
            }
        }
    }
}
