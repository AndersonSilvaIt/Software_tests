﻿using NerdStore.Core.DomainObjects;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    public class PedidoTests
    {
        [Fact(DisplayName = "Adicionar Item Novo Pedido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_NovoPedido_DeveAtualizarValor()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 2, 100);


            // Act
            pedido.AdicionarItem(pedidoItem);

            // Assert
            Assert.Equal(200, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Adicionar Item Pedido Existente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_ItemExistente_DeveIncrementarUnidadesSomarValores()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();

            var pedidoItem = new PedidoItem(produtoId, "Produto Teste", 2, 100);

            pedido.AdicionarItem(pedidoItem);

            var pedidoItem2 = new PedidoItem(produtoId, "Produto Teste", 1, 100);

            // Act
            pedido.AdicionarItem(pedidoItem2);


            // Assert
            Assert.Equal(300, pedido.ValorTotal);
            Assert.Equal(1, pedido.PedidoItems.Count);
            Assert.Equal(3, pedido.PedidoItems.FirstOrDefault(x => x.ProdutoId == produtoId).Quantidade);
        }


        [Fact(DisplayName = "Adicionar Item Pedido Acima do permitido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_UnidadesItemAcimaDoPermitido_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Produto Teste", Pedido.MAX_UNIDADES_ITEM + 1, 100);

            // Act & Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem));
        }

        [Fact(DisplayName = "Adicionar Item Pedido Existente Acima do permitido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_ItemExstenteSomaUnidadeAcimaDoPermitido_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Produto Teste", 1, 100);
            var pedidoItem2 = new PedidoItem(produtoId, "Produto Teste", Pedido.MAX_UNIDADES_ITEM + 1, 100);
            pedido.AdicionarItem(pedidoItem);
                    
            // Act & Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem2));
        }

        [Fact(DisplayName = " Atualizar Item Pedido Inexistente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_ItemNaoExistenteNaLista_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItemAtualizado = new PedidoItem(Guid.NewGuid(), "Produto Teste", 5, 100);

            // Act & Assert
            Assert.Throws<DomainException>(() => pedido.AtualizarItem(pedidoItemAtualizado));
        }

        [Fact(DisplayName = " Atualizar Item Pedido Valido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_ItemValido_DeveRetornarQuantidade()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = new Guid();

            var pedidoItem = new PedidoItem(produtoId, "Produto Teste", 2, 100);
            pedido.AdicionarItem(pedidoItem);
            var pedidoItemAtualizado = new PedidoItem(produtoId, "Produto Teste", 5, 100);

            var novaQuantidade = pedidoItemAtualizado.Quantidade;

            pedido.AtualizarItem(pedidoItemAtualizado);

            // Act & Assert
            Assert.Equal(novaQuantidade, pedido.PedidoItems.FirstOrDefault(p => p.ProdutoId == produtoId).Quantidade);
        }

        [Fact(DisplayName = " Atualizar Item Pedido Validar Total")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_PedidoComProdutosDiferentes_DeveAtualizarValorTotal()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = new Guid();

            var pedidoItemExistente1 = new PedidoItem(Guid.NewGuid(), "Produto xpto", 2, 100);
            var pedidoItemExistente2 = new PedidoItem(produtoId, "Produto Teste", 3, 15);

            pedido.AdicionarItem(pedidoItemExistente1);
            pedido.AdicionarItem(pedidoItemExistente2);

            var pedidoItemAtualizado = new PedidoItem(produtoId, "Produto Teste", 5, 15);
            var totalPedido = pedidoItemExistente1.Quantidade * pedidoItemExistente1.ValorUnitario +
                              pedidoItemAtualizado.Quantidade * pedidoItemAtualizado.ValorUnitario;

            // Act
            pedido.AtualizarItem(pedidoItemAtualizado);

            // Assert
            Assert.Equal(totalPedido, pedido.ValorTotal);
        }


        [Fact(DisplayName = " Atualizar Item Pedido Quantidade Acima do Permitido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_ItemUnidadesAcimaDoPermitido_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = new Guid();
            var pedidoItemExistente1 = new PedidoItem(produtoId, "Produto Teste", 15, 15);
            pedido.AdicionarItem(pedidoItemExistente1);

            var pedidoItemAtualizado = new PedidoItem(produtoId, "Produto Teste", Pedido.MAX_UNIDADES_ITEM + 1, 15);

            // Assert
            Assert.Throws<DomainException>(() => pedido.AtualizarItem(pedidoItemAtualizado));
        }
    }
}