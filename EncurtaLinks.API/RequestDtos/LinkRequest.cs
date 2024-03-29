using System.ComponentModel.DataAnnotations;

namespace EncurtaLinks.API.RequestDtos
{
    public record LinkRequest()
    {
        [Required(ErrorMessage ="Link é necessário")]
        public required string Link { get; init; }
    };
}
