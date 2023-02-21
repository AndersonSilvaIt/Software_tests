using Xunit;

namespace Demo.Tests
{
    public class AssertStringTests
    {
        [Fact]
        public void StringsTools_UnirNomes_RetornarNomeCompleto()
        {
            // Arrange
            var sut = new StringTools();

            // Act
            var nomeCompleto = sut.Unir("Anderson", "Mota");


            // 3 Parametro, ele ignora o Camel Case
            // Assert
            Assert.Equal("Anderson Mota", nomeCompleto, true);
        }

        [Fact]
        public void StringTools_UnirNomes_DeveConterTrecho()
        {
            // Arrange
            var sut = new StringTools();

            // Act
            var nomeCompleto = sut.Unir("Anderson", "Mota");

            // Assert
            Assert.Contains("erson", nomeCompleto);
        }

        [Fact]
        public void StringsTools_UnirNomes_DeveComecarCom()
        {
            // Arrange
            var sut = new StringTools();

            // Act
            var nomeCompleto = sut.Unir("Anderson", "Mota");

            // Assert
            Assert.StartsWith("And", nomeCompleto);
        }


        [Fact]
        public void StringTools_UnirNomes_DeveAcabarCom()
        {
            // Arrange
            var sut = new StringTools();

            // Act
            var nomeCompleto = sut.Unir("Anderson", "Mota");

            // Assert
            Assert.EndsWith("ota", nomeCompleto);
        }


        [Fact]
        public void StringTools_UnirNomes_ValidarExpressaoRegular()
        {
            // Arrange
            var sut = new StringTools();

            // Act
            var nomeCompleto = sut.Unir("Anderson", "Mota");

            // Assert
            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", nomeCompleto);
        }

    }
}
