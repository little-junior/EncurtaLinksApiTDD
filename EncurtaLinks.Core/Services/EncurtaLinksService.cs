using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EncurtaLinks.Core.Exceptions;
using EncurtaLinks.Core.Models;

namespace EncurtaLinks.Core.Services
{
    public class EncurtaLinksService : IEncurtaLinksService
    {
        static readonly Dictionary<int, string> caracteresPossiveis = new()
        {
            {1, "0123456789"},
            {2, "abcdefghijklmnopqrstuvwxyz"},
            {3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ"}
        };

        const string UrlPadrao = "https://encurtalinks.com/";

        const int SegundosValido = 300;
        public LinkEncurtado EncurtarLink(string link, int tempoValidoSegundos)
        {
            LinkValidation(link);

            var complementoUrl = GerarUltimaParteLink();

            return new LinkEncurtado()
            {
                UltimaParteUrl = complementoUrl,
                UrlGerado = UrlPadrao + complementoUrl,
                UrlOriginal = link,
                TempoValidadeSegundos = tempoValidoSegundos,
                DataCriacao = DateTime.Now
            };
        }
        public LinkEncurtado EncurtarLink(string url)
        {
            return EncurtarLink(url, SegundosValido);
        }

        public bool IsValid(LinkEncurtado linkEncurtado)
        {
            var dataCriacao = linkEncurtado.DataCriacao;
            var tempoValidade = linkEncurtado.TempoValidadeSegundos;

            var dataValidade = dataCriacao.AddSeconds(tempoValidade);

            return DateTime.Now < dataValidade;
        }
        private string GerarUltimaParteLink()
        {
            var complementoUrl = new StringBuilder();

            var random = new Random();
            int numStringEscolhida, numPosicaoCaracter;

            for (int i = 0; i < 7; i++)
            {
                numStringEscolhida = random.Next(1, 4);

                numPosicaoCaracter = random.Next(0, caracteresPossiveis[numStringEscolhida].Length);

                complementoUrl.Append(caracteresPossiveis[numStringEscolhida].ElementAt(numPosicaoCaracter));
            }

            return complementoUrl.ToString();
        }
        private void LinkValidation(string link)
        {
            if (!Uri.TryCreate(link, UriKind.Absolute, out Uri? _))
            {
                throw new CustomException("Link inválido", 400);
            }
        }
    }
}
