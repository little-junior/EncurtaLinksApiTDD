using EncurtaLinks.API.RequestDtos;
using EncurtaLinks.Core.Exceptions;
using EncurtaLinks.Core.Models;
using EncurtaLinks.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.AspNetCore.Cors;

namespace EncurtaLinks.API.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("api/[controller]")]
  
    public class EncurtaController : ControllerBase
    {
        private readonly IEncurtaLinksService _service;

        public EncurtaController(IEncurtaLinksService service)
        {
            _service = service;
        }
        /// <summary>
        /// Retorna o link original vinculado ao encurtado e sua validade
        /// </summary>
        /// <param name="linkRequest"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        /// <response code="200">Retorna o link original</response>
        /// <response code="404">Link encurtado não encontrado</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery]LinkRequest linkRequest)
        {
            var linkObtido = await _service.GetLinkEncurtado(linkRequest.Link);

            return Ok(new
            {
                LinkOriginal = linkObtido.UrlOriginal,
                Valido = _service.IsValid(linkObtido)
            });
        }

        [HttpGet("/{code}", Name ="GetRedirect")]
        public async Task<IActionResult> Get(string code)
        {
            var linkObtido = await _service.GetLinkEncurtadoByUltimaParte(code);

            return Redirect(linkObtido.UrlOriginal);
        }
        /// <summary>
        /// Cria um novo link encurtado
        /// </summary>
        /// <param name="linkRequest"></param>
        /// <returns>Teste</returns>
        /// <response code="201">Cria e retorna o link encurtado</response>
        /// <response code="400">Link original inválido</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] LinkRequest linkRequest)
        {
            var linkGerado = await _service.EncurtarLink(linkRequest.Link);

            return CreatedAtRoute("GetRedirect", new { code = linkGerado.UltimaParteUrl}, new
            {
                linkGerado.Id,
                LinkGerado = linkGerado.UrlGerado,
                linkGerado.DataCriacao,
                TempoValidoSegundos = linkGerado.TempoValidadeSegundos
            });
        }
    }
}
