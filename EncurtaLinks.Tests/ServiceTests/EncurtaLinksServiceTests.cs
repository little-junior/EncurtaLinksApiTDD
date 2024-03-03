using EncurtaLinks.Core.Exceptions;
using EncurtaLinks.Core.Models;
using EncurtaLinks.Core.Services;
using FluentAssertions;

namespace EncurtaLinks.Tests.ServiceTests
{
    public class EncurtaServiceTests
    {
        [Fact]
        public void EncurtarLink_LinkValido_RetornaLinkEncurtadoComUltimaParteUrlDeSeteCaracteres()
        {
            //Arrange
            var encurtaService = new EncurtaLinksService();
            var link = "https://globo.com.br";

            //Act
            var linkEncurtado = encurtaService.EncurtarLink(link);

            //Assert
            linkEncurtado.Should().NotBeNull();
            linkEncurtado.UltimaParteUrl.Length.Should().Be(7);
        }

        [Theory]
        [InlineData("https://globo.com.br")]
        [InlineData("https://ada.tech")]
        [InlineData("https://youtube.com")]
        public void EncurtarLink_LinkValido_RetornaLinkEncurtadoComUrlVinculado(string url)
        {
            //Arrange
            var encurtaService = new EncurtaLinksService();

            //Act
            var linkEncurtado = encurtaService.EncurtarLink(url);

            //Assert
            linkEncurtado.Should().NotBeNull();
            linkEncurtado.UrlOriginal.Should().NotBeNull().And.Be(url);
        }

        [Theory]
        [InlineData("//twitch.tv")]
        [InlineData("http://globo.com")]
        [InlineData("https://twitter.com")]
        public void EncurtarLink_LinkValido_RetornaLinkEncurtadoSemErro(string url)
        {
            //Arrange
            var encurtaService = new EncurtaLinksService();

            //Act + Assert
            encurtaService.Invoking(x => x.EncurtarLink(url)).Should().NotThrow<CustomException>("Link é válido");
        }

        [Theory]
        [InlineData("twitch.tv")]
        [InlineData("globo.com")]
        [InlineData("twitter.com")]
        public void EncurtarLink_LinkInvalido_RetornaErro(string url)
        {
            //Arrange
            var encurtaService = new EncurtaLinksService();

            //Act + Assert
            encurtaService.Invoking(x => x.EncurtarLink(url)).Should().ThrowExactly<CustomException>("Link é inválido");
        }
        
        [Theory]
        [MemberData(nameof(LinksValidosComTemposDiferentesData))]
        public async Task IsValid_LinkEncurtadoForaDoTempo_RetornaFalse(string link, int tempoValidoSegundos)
        {
            //Arrange
            var encurtaService = new EncurtaLinksService();
            var linkEncurtado = encurtaService.EncurtarLink(link, tempoValidoSegundos);

            //Act
            await Task.Delay(tempoValidoSegundos * 1000);
            var isValid = encurtaService.IsValid(linkEncurtado);

            //Assert
            isValid.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(LinksValidosComTemposDiferentesData))]
        public async Task IsValid_LinkEncurtadoDentroDoTempo_RetornaTrue(string link, int tempoValidoSegundos)
        {
            //Arrange
            var encurtaService = new EncurtaLinksService();
            var linkEncurtado = encurtaService.EncurtarLink(link, tempoValidoSegundos);

            //Act
            await Task.Delay((tempoValidoSegundos - 1) * 1000);
            var isValid = encurtaService.IsValid(linkEncurtado);

            //Assert
            isValid.Should().BeTrue();
        }

        public static TheoryData<string, int> LinksValidosComTemposDiferentesData =>
            new TheoryData<string, int>
            {
                {"https://udemy.com", 1 },
                {"https://globo.com", 2 },
                {"https://twitter.com", 3},
                {"https://ada.tech/", 4 },
                { "https://youtube.com", 5 }
            };
    }
}