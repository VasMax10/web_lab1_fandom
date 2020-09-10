using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_lab1_fandom.Models
{
    public class Characters
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Burthyear { get; set; }
        public string Photo { get; set; }
        public string Status { get; set; }
        public string Info { get; set; }

        public virtual ICollection<Casts> Cast { get; set; }
    }
}
