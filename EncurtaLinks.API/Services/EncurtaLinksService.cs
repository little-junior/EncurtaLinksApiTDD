using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EncurtaLinks.Core;
using EncurtaLinks.Core.Exceptions;
using EncurtaLinks.Core.Models;
using EncurtaLinks.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EncurtaLinks.API.Services
{
    public class EncurtaLinksService : IEncurtaLinksService
    {
        public EncurtaLinksService(EncurtaLinksContext context, IRandomizer randomizer)
        {
            _context = context;
            _randomizer = randomizer;
        }

        private readonly IRandomizer _randomizer;
        private readonly EncurtaLinksContext _context;

        public async Task<LinkEncurtado> EncurtarLink(string link, int tempoValidoSegundos)
        {
            LinkValidation(link);

            var complementoUrl = await GerarUltimaParteLink();

            var linkEncurtado = new LinkEncurtado()
            {
                UltimaParteUrl = complementoUrl,
                UrlGerado = LinkEncurtadoConfigs.UrlPadrao + complementoUrl,
                UrlOriginal = link,
                TempoValidadeSegundos = tempoValidoSegundos,
                DataCriacao = DateTime.UtcNow
            };

            await _context.LinksEncurtados.AddAsync(linkEncurtado);
            await _context.SaveChangesAsync();

            return linkEncurtado;
        }
        public async Task<LinkEncurtado> EncurtarLink(string url)
        {
            return await EncurtarLink(url, LinkEncurtadoConfigs.SegundosValido);
        }
        public async Task<LinkEncurtado> GetLinkEncurtado(string url)
        {
            var linkObtido = await _context.LinksEncurtados.AsNoTracking().SingleOrDefaultAsync(l => l.UrlGerado == url)
                ?? throw new CustomException("Link não encontrado", "Not Found", 404);

            return linkObtido;
        }

        public bool IsValid(LinkEncurtado linkEncurtado)
        {
            var dataCriacao = linkEncurtado.DataCriacao;
            var tempoValidade = linkEncurtado.TempoValidadeSegundos;

            var dataValidade = dataCriacao.AddSeconds(tempoValidade);

            return DateTime.UtcNow < dataValidade;
        }
        private async Task<string> GerarUltimaParteLink()
        {
            var complementoBuilder = new StringBuilder();

            int numStringEscolhida, numPosicaoCaracter;

            int tentativas = 1;

            while (tentativas <= 10)
            {
                for (int i = 0; i < LinkEncurtadoConfigs.MaxCaracteres; i++)
                {
                    numStringEscolhida = _randomizer.Next(1, 4);

                    numPosicaoCaracter = _randomizer.Next(0, LinkEncurtadoConfigs.caracteresPossiveis[numStringEscolhida].Length);

                    complementoBuilder.Append(LinkEncurtadoConfigs.caracteresPossiveis[numStringEscolhida].ElementAt(numPosicaoCaracter));
                }

                var complementoUrl = complementoBuilder.ToString();

                if (!await UltimaParteUrlCheck(complementoUrl))
                {
                    return complementoUrl;
                }

                complementoBuilder.Clear();
                tentativas++;
            }
            throw new CustomException("Ocorreu um erro de processamento. Tente novamente", "Internal Server Error", 500);
        }
        private void LinkValidation(string link)
        {
            if (!Uri.TryCreate(link, UriKind.Absolute, out Uri? _))
            {
                throw new CustomException("Link inválido", "Bad Request", 400);
            }
        }
        private async Task<bool> UltimaParteUrlCheck(string ultimaParteUrl)
        {
            return await _context.LinksEncurtados.AsNoTracking().AnyAsync(l => l.UltimaParteUrl == ultimaParteUrl);
        }

        public async Task<LinkEncurtado> GetLinkEncurtadoByUltimaParte(string ultimaParteUrl)
        {
            var link = await _context.LinksEncurtados.AsNoTracking().SingleOrDefaultAsync(l => l.UltimaParteUrl == ultimaParteUrl);

            if (link is null)
            {
                throw new CustomException("Link não encontrado", "Not Found", 404);
            }

            return link;
        }
    }
}
