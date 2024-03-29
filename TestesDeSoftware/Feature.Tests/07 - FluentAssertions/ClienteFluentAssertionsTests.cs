﻿using Xunit.Abstractions;
using Xunit;
using FluentAssertions;

namespace Features.Tests._07___FluentAssertions
{
    [Collection(nameof(ClienteAutoMockerCollection))]
    public class ClienteFluentAssertionsTests
    {
        private readonly ClientTestsAutoMockerFixture _clienteTestsFixture;
        readonly ITestOutputHelper _outputHelper;

        public ClienteFluentAssertionsTests(ClientTestsAutoMockerFixture clienteTestsFixture,
                                            ITestOutputHelper outputHelper)
        {
            _clienteTestsFixture = clienteTestsFixture;
            _outputHelper = outputHelper;
        }


        [Fact(DisplayName = "Novo Cliente Válido", Skip = "Pulou o teste por algum motivo. Atenção.")]
        [Trait("Categoria", "Cliente Fluent Assertion Testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteValido();

            // Act
            var result = cliente.EhValido();

            // Assert 
            //Assert.True(result);
            //Assert.Equal(0, cliente.ValidationResult.Errors.Count);

            // Assert 
            result.Should().BeTrue();
            cliente.ValidationResult.Errors.Should().HaveCount(0);
        }

        [Fact(DisplayName = "Novo Cliente Inválido")]
        [Trait("Categoria", "Cliente Fluent Assertion Testes")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteInvalido();

            // Act
            var result = cliente.EhValido();

            // Assert 
            //Assert.False(result);
            //Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);

            // Assert 
            result.Should().BeFalse();
            cliente.ValidationResult.Errors.Should().HaveCountGreaterOrEqualTo(1, "deve possuir erros de validação");

            // Gera uma saída de texto ...
            _outputHelper.WriteLine($"Foram encontrados {cliente.ValidationResult.Errors.Count} erros nesta validação");
        }
    }
}
