using Bogus;
using Bogus.DataSets;
using Features.Clientes;
using Xunit;

namespace Features.Tests
{
    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteCollection : ICollectionFixture<ClienteTestsFixture> { }

    public class ClienteTestsFixture : IDisposable
    {
        public Cliente GerarClienteValido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();


            // Gera um email aleatório
            /*var email = new Faker().Internet.Email();
            var clienteFaker = new Faker<Cliente>();
            clienteFaker.RuleFor(c => c.Nome, (f, c) => f.Name.FirstName());*/


            var cliente = new Faker<Cliente>("pt_BR")
                .CustomInstantiator(f => new Cliente(
                    Guid.NewGuid(),
                    f.Name.FirstName(genero),
                    f.Name.LastName(genero),
                    f.Date.Past(80, DateTime.Now.AddYears(-18)),
                    "",
                    true,
                    DateTime.Now))
                .RuleFor(c => c.Email, (f, c) =>
                    f.Internet.Email(c.Nome.ToLower(), c.Sobrenome.ToLower()));

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