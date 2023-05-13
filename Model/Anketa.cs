using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabourExchange.Model
{
    public class Anketa
    {
        public int Id { get; set; }
        public string Fam { get; set; }
        public string Name { get; set; }
        public string Otch { get; set; }
        public string Pasport { get; set; }
        public int KolYear { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }


        public override string ToString()
        {
            return $"{Fam} {Name} {Otch}";
        }
    }
}
