using Features.Clientes;
using Xunit;

namespace Features.Tests._02___Fixtures
{
    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteCollection : ICollectionFixture<ClienteTestsFixture>  { }

    public class ClienteTestsFixture : IDisposable
    {
        public Cliente GerarClienteValido()
        {
            var cliente = new Cliente(
                                        id: Guid.NewGuid(),
                                        nome: "Anderson",
                                        sobrenome: "Mota",
                                        dataNascimento: DateTime.Now.AddYears(-28),
                                        email: "anderson@gmail.com",
                                        ativo: true,
                                        dataCadastro: DateTime.Now);
            return cliente;
        }

        public Cliente GerarClienteInvalido()
        {
            var cliente = new Cliente(
                            id: Guid.NewGuid(),
                            nome: "",
                            sobrenome: "",
                            dataNascimento: DateTime.Now,
                            email: "anderson2gmail.com",
                            ativo: true,
                            dataCadastro: DateTime.Now);

            return cliente;
        }

        public void Dispose()
        {
            
        }
    }
}