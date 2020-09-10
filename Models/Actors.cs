using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_lab1_fandom.Models
{
    public class Actors
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public DateTime Birthdate { get; set; }
        public string Info { get; set; }
        public virtual ICollection<Casts> Cast { get; set; }
    }
}
