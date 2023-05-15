﻿using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    public class VoucherTests
    {
        [Fact(DisplayName = "Validar Voucher Tipo Valor Válido")]
        [Trait("Categoria", "Vendas - Voucher")]
        public void Voucher_ValidarVoucherTipoValor_DeveEstarValido()
        {
            // Arrange
            var voucher = new Voucher("PROMO-15-REAIS", 15, null, TipoDescontoVoucher.Valor, 1, DateTime.Now.AddDays(15), true, false);

            // Act
            var result = voucher.ValidarSeAplicavel();

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Validar Voucher Tipo Valor Inválido")]
        [Trait("Categoria", "Vendas - Voucher")]
        public void Voucher_ValidarVoucherTipoValor_DeveEstarInValido()
        {
            // Arrange
            var voucher = new Voucher("", null, null, TipoDescontoVoucher.Valor, 0, DateTime.Now.AddDays(-1), false, true);
                
            // Act
            var result = voucher.ValidarSeAplicavel();

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(6, result.Errors.Count);
            Assert.Contains(VoucherAplicadoValidation.AtivoErroMsg, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VoucherAplicadoValidation.CodigoErroMsg, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VoucherAplicadoValidation.DataValidadeErroMsg, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VoucherAplicadoValidation.QuantidadeErroMsg, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VoucherAplicadoValidation.UtilizadoErroMsg, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VoucherAplicadoValidation.ValorDescontoErroMsg, result.Errors.Select(c => c.ErrorMessage));
        }
    }
}