using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabourExchange.Model
{
    public class WorkScedule
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "";
        public int duration { get; set; } = 0;

        public override string ToString()
        {
            return Name;
        }
    }
}
