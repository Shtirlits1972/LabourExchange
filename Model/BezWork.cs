using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LabourExchange.Model
{
    public class BezWork
    {
        public int Id { get; set; }
        public int AnketaId { get; set; }
        public string AnketaName { get; set; }
        public int Stag { get; set; }
        public string Email { get; set; }
        public int EducationId { get; set; }
        public string EducationName { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public string Professional { get; set; }
        public string MestoWork { get; set; }
        public string PrichinaUvoln { get; set; }
        public int FamilyStatusId { get; set; }
        public string FamilyStatusName { get; set; }
        public string KontaktKoord { get; set; }
        public string Trebov_K_Work { get; set; }
        public DateTime Birthday { get; set; }
        public int UserId { get; set; }
        public bool Arhiv { get; set; }

        public override string ToString()
        {
            return AnketaName;
        }
    }
}
