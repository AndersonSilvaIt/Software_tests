using Features.Clientes;
using MediatR;
using Moq;
using Xunit;

namespace Features.Tests._05___Mock
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteServiceTests
    {
        readonly ClienteTestsBogusFixture _clientTestsBogus;

        public ClienteServiceTests(ClienteTestsBogusFixture clientTestsBogus)
        {
            _clientTestsBogus = clientTestsBogus;
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service Mock Test")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clientTestsBogus.GerarClienteValido();

            // Faço o Mock da interface de repositório
            var clienteRepo = new Mock<IClienteRepository>();
            var mediatr = new Mock<IMediator>();

            var clienteService = new ClienteService(clienteRepo.Object, mediatr.Object);

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.True(cliente.EhValido()); // Esse cara é desnecessário, pois já está sendo validado dentro de "Adicionar"

            clienteRepo.Verify(r => r.Adicionar(cliente), Times.Once); // Verifica se foi chamado internamente esse método, e se chamou somente uma vez.
            mediatr.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service Mock Test")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {

        }

        /*[Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service Mock Test")]
        public void ClienteService_Obter_DeveObter*/

    }
}
