using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncurtaLinks.Core
{
    public interface IRandomizer
    {
        int Next(int comeco, int fim);
    }
}
