using Xunit;

namespace Demo.Tests
{
    public class CalculadoraTests
    {
        [Fact]
        public void Calculadora_Somar_RetornarValorSoma()
        {
            // Arrange
            var calculadora = new Calculadora();

            //Act
            var resultador = calculadora.Somar(2, 2);

            // Primeiro parâmetro, o resultado esperado
            // o segundo, o valor obtido
            //Assert
            Assert.Equal(4, resultador);
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(2, 3, 5)]
        [InlineData(4, 4, 8)]
        [InlineData(5, 5, 10)]
        [InlineData(12, 12, 24)]
        public void Calculador_Somar_RetornarValoresSomaCorretos(double v1, double v2, double total)
        {
            // Arrange
            var calculadora = new Calculadora();

            //Act
            var resultador = calculadora.Somar(v1, v2);

            //Assert
            Assert.Equal(total, resultador);
        }
    }
}
