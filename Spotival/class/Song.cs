using Spotival.visuals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotival.classes
{
    public class Song
    {
        public string Titre { get; set; }
        public string Artiste { get; set; }
        public TimeSpan Durée { get; set; }
        public string Type { get; set; }
    }
}
