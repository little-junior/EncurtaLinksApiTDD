using EncurtaLinks.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncurtaLinks.API.Services
{
    public interface IEncurtaLinksService
    {
        Task<LinkEncurtado> EncurtarLink(string link, int tempoValidoSegundos);
        Task<LinkEncurtado> EncurtarLink(string url);
        Task<LinkEncurtado> GetLinkEncurtado(string url);
        Task<LinkEncurtado> GetLinkEncurtadoByUltimaParte(string ultimaParte);
        bool IsValid(LinkEncurtado linkEncurtado);
    }
}
