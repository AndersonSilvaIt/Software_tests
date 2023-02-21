using Xunit;

namespace Demo.Tests
{
    public class AssertingObjeectTypesTests
    {
        [Fact]
        public void FuncionarioFactory_Criar_DeveRetornarTupoFuncionario()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Anderson", 10000);

            //Assert
            Assert.IsType<Funcionario>(funcionario);
        }

        [Fact]
        public void FuncionarFactory_Criar_DeveRetornarTipoDerivadoPessoa()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Anderson", 10000);

            //Assert
            Assert.IsAssignableFrom<Pessoa>(funcionario); // Verifica se Herda de Pessoa ...
        }
    }
}