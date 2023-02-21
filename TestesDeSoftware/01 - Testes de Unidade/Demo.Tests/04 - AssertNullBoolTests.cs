using Xunit;

namespace Demo.Tests
{
    public class AssertNullBoolTests
    {
        [Fact]
        public void Funcionario_Nome_NaoDeveSerNuloOuVazio()
        {
            var funcionario = new Funcionario("", 1000);

            Assert.False(string.IsNullOrEmpty(funcionario.Nome));
        }


        [Fact]
        public void Funcionario_Apelido_NaoDeveTerApelido()
        {
            // Arrange & Act
            var funcionario = new Funcionario("Anderson", 1000);


            //Assert
            Assert.Null(funcionario.Apelido);


            //Assert Bool
            Assert.True(string.IsNullOrEmpty(funcionario.Apelido));
            Assert.False(funcionario.Apelido?.Length > 0);
        }
    }
}