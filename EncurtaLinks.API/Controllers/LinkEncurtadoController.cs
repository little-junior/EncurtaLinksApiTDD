using EncurtaLinks.API.RequestDtos;
using EncurtaLinks.Core.Exceptions;
using EncurtaLinks.Core.Models;
using EncurtaLinks.Core.Services;
using EncurtaLinks.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace EncurtaLinks.API.Controllers
{
    [ApiController]
    [Route("api/encurta")]
  
    public class LinkEncurtadoController : ControllerBase
    {
        private readonly IRepository<LinkEncurtado> _repository;
        private readonly IEncurtaLinksService _service;

        public LinkEncurtadoController(IRepository<LinkEncurtado> repository, IEncurtaLinksService service)
        {
            _repository = repository;
            _service = service;
        }
        /// <summary>
        /// Retorna o link original vinculado ao encurtado e sua validade
        /// </summary>
        /// <param name="linkDto"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        /// <response code="200">Retorna o link original</response>
        /// <response code="404">Link encurtado não encontrado</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery]LinkDto linkDto)
        {
            var linkObtido = await _repository.GetByLink(linkDto.Link);

            if (linkObtido == null)
            {
                throw new CustomException("Link não encontrado", 404);
            }

            return Ok(new
            {
                LinkOriginal = linkObtido.UrlOriginal,
                Valido = _service.IsValid(linkObtido)
            });
        }

        /// <summary>
        /// Cria um novo link encurtado
        /// </summary>
        /// <param name="linkDto"></param>
        /// <returns>Teste</returns>
        /// <response code="201">Cria e retorna o link encurtado</response>
        /// <response code="400">Link original inválido</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] LinkDto linkDto)
        {
            var linkGerado = _service.EncurtarLink(linkDto.Link);

            await _repository.Add(linkGerado);

            return Created($"api/encurta?link={linkGerado.UrlGerado}", new
            {
                Id = linkGerado.Id,
                LinkGerado = linkGerado.UrlGerado,
                DataCriacao = linkGerado.DataCriacao.ToString("G", CultureInfo.InvariantCulture),
                TempoValidoSegundos = linkGerado.TempoValidadeSegundos
            });
        }
    }
}
