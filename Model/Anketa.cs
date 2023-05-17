using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabourExchange.Model
{
    public class Anketa
    {
        public int Id { get; set; } = 0;
        public string Fam { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Otch { get; set; } = string.Empty;
        public string Pasport { get; set; } = string.Empty;
        public int KolYear { get; set; } = 0;
        public string Email { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public int UserId { get; set; } = 0;
        public DateTime Birthday { get; set; } = new DateTime(2000, 1, 1);

        public override string ToString()
        {
            return $"{Fam} {Name} {Otch}";
        }
    }
}
