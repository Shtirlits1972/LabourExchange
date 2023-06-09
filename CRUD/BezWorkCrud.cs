﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using LabourExchange.Model;
using System.Windows;

namespace LabourExchange.CRUD
{
    public class BezWorkCrud
    {
        public static readonly string strConn = Ut.strConn;

        public static List<BezWork> GetAll(int Stag, string Profession)
        {
            List<BezWork> list = new List<BezWork>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<BezWork>("SELECT Id, AnketaId, AnketaName, Stag, Email, EducationId, EducationName, PositionId, PositionName, Professional,  MestoWork, PrichinaUvoln, FamilyStatusId, "
                    + " FamilyStatusName, KontaktKoord, Trebov_K_Work, Birthday, UserId, Arhiv  FROM BezWorkView WHERE Stag >= @Stag AND LOWER(Professional) LIKE LOWER('%" + Profession + "%')", new { Stag }).ToList();
            }

            return list;
        }

        public static List<BezWork> GetAll()
        {
            List<BezWork> list = new List<BezWork>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<BezWork>("SELECT Id, AnketaId, AnketaName, Stag, Email, EducationId, EducationName, PositionId, PositionName, Professional, "
                    + " MestoWork, PrichinaUvoln, FamilyStatusId, FamilyStatusName, KontaktKoord, Trebov_K_Work, Birthday, UserId, Arhiv  FROM BezWorkView").ToList();
            }

            return list;
        }

        public static BezWork GetOne(int Id)
        {
            BezWork model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<BezWork>("SELECT Id, AnketaId, AnketaName, Stag,  Email, EducationId, EducationName, PositionId, PositionName, Professional, "
                    + " MestoWork, PrichinaUvoln, FamilyStatusId, FamilyStatusName, KontaktKoord, Trebov_K_Work, Birthday, UserId, Arhiv FROM BezWorkView WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM BezWork WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(BezWork model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE BezWork SET AnketaId = @AnketaId,  EducationId = @EducationId,  PositionId = @PositionId,  Professional = @Professional, MestoWork = @MestoWork,  "
                    + "  PrichinaUvoln = @PrichinaUvoln, FamilyStatusId = @FamilyStatusId, KontaktKoord = @KontaktKoord, Trebov_K_Work = @Trebov_K_Work, Arhiv = @Arhiv WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static BezWork Add(BezWork model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO BezWork (AnketaId,  EducationId,  PositionId, Professional, MestoWork, PrichinaUvoln, FamilyStatusId,  KontaktKoord, Trebov_K_Work, Arhiv) " 
                    + " VALUES(@AnketaId,  @EducationId,  @PositionId, @Professional, @MestoWork, @PrichinaUvoln, @FamilyStatusId,  @KontaktKoord, @Trebov_K_Work, @Arhiv); SELECT CAST(SCOPE_IDENTITY() as int)";

                try
                {
                    int Id = db.Query<int>(Query, model).FirstOrDefault();
                    model.Id = Id;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            return model;
        }
    }
}
