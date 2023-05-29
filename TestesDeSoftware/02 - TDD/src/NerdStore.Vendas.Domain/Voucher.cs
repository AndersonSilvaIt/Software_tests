using FluentValidation;
using FluentValidation.Results;

namespace NerdStore.Vendas.Domain
{
    public class Voucher
    {
        public string Codigo { get; private set; }
        public decimal? ValorDesconto { get; private set; }
        public decimal? PercentualDesconto { get; private set; }
        public TipoDescontoVoucher TipoDescontoVoucher { get; private set; }
        public int Quantidade { get; private set; }
        public DateTime DataValidade { get; private set; }
        public bool Ativo { get; private set; }
        public bool Utilizado { get; private set; }


        public Voucher(string codigo, 
                       decimal? percentualDesconto, 
                       decimal? valorDesconto,
                       int quantidade,
                       TipoDescontoVoucher tipoDescontoVoucher, 
                       DateTime dataValidade, 
                       bool ativo, bool utilizado)
        {
            Codigo = codigo;
            ValorDesconto = valorDesconto;
            PercentualDesconto = percentualDesconto;
            TipoDescontoVoucher = tipoDescontoVoucher;
            Quantidade = quantidade;
            DataValidade = dataValidade;
            Ativo = ativo;
            Utilizado = utilizado;
        }


        public ValidationResult ValidarSeAplicavel()
        {
            return new VoucherAplicadoValidation().Validate(this);
        }
    }

    public class VoucherAplicadoValidation : AbstractValidator<Voucher> 
    {
        public static string CodigoErroMsg => "Voucher sem código válido";
        public static string DataValidadeErroMsg => "Este voucher está expirado";
        public static string AtivoErroMsg => "Esse voucher nãó é mais válido";
        public static string UtilizadoErroMsg => "Esse voucher já foi utilizado";
        public static string QuantidadeErroMsg => "Este voucher não está mais disponível";
        public static string ValorDescontoErroMsg => "O valor de desconto precisa ser auperior a 0";
        public static string PercentualDescontoMsg => "O valor da porcentagem de desconto precisa ser superior a 0";

        public VoucherAplicadoValidation()
        {
            RuleFor(c => c.Codigo)
                .NotEmpty()
                .WithMessage(CodigoErroMsg);

            RuleFor(c => c.DataValidade)
                .Must(DataVencimentoSuperiorAtual)
                .WithMessage(DataValidadeErroMsg);

            RuleFor(c => c.Ativo)
                .Equal(true)
                .WithMessage(AtivoErroMsg);

            RuleFor(c => c.Utilizado)
                .Equal(false)
                .WithMessage(UtilizadoErroMsg);

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage(QuantidadeErroMsg);

            When(f => f.TipoDescontoVoucher == TipoDescontoVoucher.Valor, () =>
            {
                RuleFor(f => f.ValorDesconto)
                .NotNull()
                .WithMessage(ValorDescontoErroMsg)
                .GreaterThan(0)
                .WithMessage(ValorDescontoErroMsg);
            });

            When(f => f.TipoDescontoVoucher == TipoDescontoVoucher.Porcentagem, () =>
            {
                RuleFor(f => f.PercentualDesconto)
                .NotNull()
                .WithMessage(PercentualDescontoMsg)
                .GreaterThan(0)
                .WithMessage(PercentualDescontoMsg);
            });
        }

        protected static bool DataVencimentoSuperiorAtual(DateTime dataValidade)
        {
            return dataValidade >= DateTime.Now;
        }
    }
}
