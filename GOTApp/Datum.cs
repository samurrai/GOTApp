using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOTApp
{
    public class Datum
    {
        public string Gender { get; set; }
        public string House { get; set; }
        public string Name { get; set; }
        public string DateOfDeath { get; set; }
        public string DateOfBirth { get; set; }
        public string Image { get; set; }
        public string Actor { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
