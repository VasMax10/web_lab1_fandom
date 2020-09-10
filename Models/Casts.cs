using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_lab1_fandom.Models
{
    public class Casts
    {
        public int ID { get; set; }
        public int SeriesID { get; set; }
        public int ActorID { get; set; }
        public int CharacterID { get; set; }
        public int FirstAppereance { get; set; } // first season
        public int LastAppereance { get; set; } // last season
        public virtual Series Series { get; set; }
        public virtual Actors Actor { get; set; }
        public virtual Characters Character { get; set; }
    }
}
