using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncurtaLinks.Core
{
    public class Randomizer : IRandomizer
    {
        public Random Random { get; set; }
        public Randomizer()
        {
            Random = new Random();
        }
        
        public int Next(int comeco, int fim)
        {
            return Random.Next(comeco, fim);
        }
    }
}
