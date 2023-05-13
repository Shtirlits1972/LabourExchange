using LabourExchange.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Dapper.SqlMapper;

namespace LabourExchange.Model
{
    public class Vacancy
    {
        public int Id { get; set; }
        public int FirmaId { get; set; }
        public string FirmaName { get; set; }
        public int EducationId { get; set; }
        public string EducationName { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public int WorkSceduleId { get; set; }
        public string WorkSceduleName { get; set; }
        public string UsloviyWorkOplata { get; set; }
        public string Trebovan { get; set; }
        public bool Priznak { get; set; }
        public string Sex { get; set; }

        public override string ToString()
        {
            return $"Id = {Id}, FirmaId = {FirmaId}, FirmaName = {FirmaName}, EducationId = {EducationId}, EducationName = {EducationName}, PositionId = {PositionId},  PositionName = {PositionName}, WorkSceduleId = {WorkSceduleId},"
                +$" WorkSceduleName = {WorkSceduleName}, UsloviyWorkOplata = {UsloviyWorkOplata}, Trebovan = {Trebovan}, Priznak = {Priznak}, Sex = {Sex}";
        }
    }
}