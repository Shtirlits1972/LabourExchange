using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using System.Xml.Linq;

namespace LabourExchange.Model
{
    public class Benefit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Val { get; set; }
        public DateTime Data_Vyplaty { get; set; }
        public DateTime Data_Postanovki { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}

