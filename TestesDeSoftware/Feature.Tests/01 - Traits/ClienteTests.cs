using Features.Clientes;
using Xunit;

namespace Features.Tests._01___Traits
{
    public class ClienteTests
    {
        [Fact(DisplayName = "Novo Cliente Válido")]
        [Trait("Categoria", "Client Trati Testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            // Arrange
            var cliente = new Cliente(
                                        id: Guid.NewGuid(),
                                        nome: "Anderson",
                                        sobrenome: "Mota",
                                        dataNascimento: DateTime.Now.AddYears(-28),
                                        email: "anderson@gmail.com",
                                        ativo: true,
                                        dataCadastro: DateTime.Now);

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.True(result);
            Assert.Equal(0, cliente.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "Novo Cliente Inválido")]
        [Trait("Categoria", "Client Trati Testes")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            // Arrange
            var cliente = new Cliente(
                                        id: Guid.NewGuid(),
                                        nome: "",
                                        sobrenome: "",
                                        dataNascimento: DateTime.Now,
                                        email: "anderson2gmail.com",
                                        ativo: true,
                                        dataCadastro: DateTime.Now);

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.False(result);
            Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);
        }
    }
}
