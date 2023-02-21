using Features.Clientes;
using MediatR;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteServiceAutoMockTests
    {
        readonly ClienteTestsBogusFixture _clientTestsBogus;

        public ClienteServiceAutoMockTests(ClienteTestsBogusFixture clientTestsBogus)
        {
            _clientTestsBogus = clientTestsBogus;
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service Auto Mock Test")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clientTestsBogus.GerarClienteValido();
            var mocker = new AutoMocker();

            var clienteService = mocker.CreateInstance<ClienteService>();

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.True(cliente.EhValido());

            mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Once); // Verifica se foi chamado internamente esse método, e se chamou somente uma vez.
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service Auto Mock Test")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            // Arrange
            var cliente = _clientTestsBogus.GerarClienteValido();
            var mocker = new AutoMocker();
            var clienteService = mocker.CreateInstance<ClienteService>();

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.False(cliente.EhValido()); // Esse cara é desnecessário, pois já está sendo validado dentro de "Adicionar"

            mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never); // Verifica se foi chamado internamente esse método, e se chamou somente uma vez.
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service Auto Mock Test")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            var cliente = _clientTestsBogus.GerarClienteValido();
            var mocker = new AutoMocker();

            mocker.GetMock<IClienteRepository>().Setup(c => c.ObterTodos())
                .Returns(_clientTestsBogus.ObterClientesVariados());

            var clienteService = mocker.CreateInstance<ClienteService>();

            // Act
            var clientes = clienteService.ObterTodosAtivos();

            //Assert
            mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(clientes.Any());
            Assert.False(clientes.Count(c => !c.Ativo) > 0);
        }
    }
}