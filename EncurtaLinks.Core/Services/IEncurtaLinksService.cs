using EncurtaLinks.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncurtaLinks.Core.Services
{
    public interface IEncurtaLinksService
    {
        LinkEncurtado EncurtarLink(string link, int tempoValidoSegundos);
        LinkEncurtado EncurtarLink(string url);
        bool IsValid(LinkEncurtado linkEncurtado);
    }
}
