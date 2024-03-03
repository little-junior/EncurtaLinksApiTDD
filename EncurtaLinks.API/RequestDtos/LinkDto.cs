using System.ComponentModel.DataAnnotations;

namespace EncurtaLinks.API.RequestDtos
{
    public class LinkDto
    {
        [Required]
        public string Link { get; set; }
    }
}
