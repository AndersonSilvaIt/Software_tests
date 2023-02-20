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
    }
}
