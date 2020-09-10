using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_lab1_fandom.Models
{
    public class Series
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Poster { get; set; }
        public int Premiere { get; set; }
        public bool IsEnded { get; set; }
        public string BackImage { get; set; }
        public string MainColor {get; set;}
        public string SecondColor { get; set; }
        public string Info { get; set; }
        public virtual ICollection<Casts> Cast { get; set; }
    }
}
