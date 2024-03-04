using EncurtaLinks.Core;
using EncurtaLinks.Core.Exceptions;
using EncurtaLinks.Core.Models;
using EncurtaLinks.Core.Services;
using FluentAssertions;
using NSubstitute;

namespace EncurtaLinks.Tests.ServiceTests
{
    public class EncurtaServiceTests
    {
        private readonly IEncurtaLinksService _sut;
        private readonly IRandomizer _randomizer;
        public EncurtaServiceTests()
        {
            _randomizer = Substitute.For<IRandomizer>();
            _sut = new EncurtaLinksService(_randomizer);
        }

        [Fact]
        public void EncurtarLink_LinkValido_RetornaLinkEncurtadoComUltimaParteUrlDeSeteCaracteres()
        {
            //Arrange
            _randomizer.Next(Arg.Any<int>(), Arg.Any<int>()).Returns(1);
            var link = "https://globo.com.br";

            //Act
            var linkEncurtado = _sut.EncurtarLink(link);

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
            _randomizer.Next(Arg.Any<int>(), Arg.Any<int>()).Returns(1);

            //Act
            var linkEncurtado = _sut.EncurtarLink(url);

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
            _randomizer.Next(Arg.Any<int>(), Arg.Any<int>()).Returns(1);

            //Act + Assert
            _sut.Invoking(x => x.EncurtarLink(url)).Should().NotThrow<CustomException>("Link é válido");
        }

        [Theory]
        [InlineData("twitch.tv")]
        [InlineData("globo.com")]
        [InlineData("twitter.com")]
        public void EncurtarLink_LinkInvalido_RetornaErro(string url)
        {
            //Arrange
            _randomizer.Next(Arg.Any<int>(), Arg.Any<int>()).Returns(1);

            //Act + Assert
            _sut.Invoking(x => x.EncurtarLink(url)).Should().ThrowExactly<CustomException>("Link é inválido");
        }
        
        [Theory]
        [MemberData(nameof(LinksValidosComTemposDiferentesData))]
        public async Task IsValid_LinkEncurtadoForaDoTempo_RetornaFalse(string link, int tempoValidoSegundos)
        {
            //Arrange
            _randomizer.Next(Arg.Any<int>(), Arg.Any<int>()).Returns(1);
            var linkEncurtado = _sut.EncurtarLink(link, tempoValidoSegundos);

            //Act
            await Task.Delay(tempoValidoSegundos * 1000);
            var isValid = _sut.IsValid(linkEncurtado);

            //Assert
            isValid.Should().BeFalse("Tempo esgotado");
        }

        [Theory]
        [MemberData(nameof(LinksValidosComTemposDiferentesData))]
        public async Task IsValid_LinkEncurtadoDentroDoTempo_RetornaTrue(string link, int tempoValidoSegundos)
        {
            //Arrange
            _randomizer.Next(Arg.Any<int>(), Arg.Any<int>()).Returns(1);
            var linkEncurtado = _sut.EncurtarLink(link, tempoValidoSegundos);

            //Act
            await Task.Delay((tempoValidoSegundos - 1) * 1000);
            var isValid = _sut.IsValid(linkEncurtado);

            //Assert
            isValid.Should().BeTrue("Tempo válido");
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