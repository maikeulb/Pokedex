using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Num { get; set; }
        public string Img { get; set; }
        public string Name { get; set; }
        [Keyword]
        public IEnumerable<string> Type { get; set; }
        public IEnumerable<string> PrevEvolution { get; set; }
        public IEnumerable<string> NextEvolution { get; set; }

        public Pokemon()
        {
            Type = new List<string>();
            NextEvolution = new List<string>();
            PrevEvolution = new List<string>();
        }

        public class RootObject
        {
            public int id { get; set; }
            public string num { get; set; }
            public string img { get; set; }
            public string name { get; set; }
            public List<string> type { get; set; }
            public List<string> prev_evolution { get; set; }
            public List<string> next_evolution { get; set; }
        }
    }
}
