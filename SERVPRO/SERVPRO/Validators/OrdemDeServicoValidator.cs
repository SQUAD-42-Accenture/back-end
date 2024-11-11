using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SERVPRO.Data;
using SERVPRO.Models;

namespace SERVPRO.Validators
{
    public class OrdemDeServicoValidator : AbstractValidator<OrdemDeServico>
    {
        private readonly ServproDBContext _context;

        public OrdemDeServicoValidator(ServproDBContext context)
        {
            _context = context;

            RuleFor(ordemdeservico => ordemdeservico.ClienteCPF)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Length(11).WithMessage("O CPF deve ter 11 dígitos.")
            .Matches("^[0-9]*$").WithMessage("O CPF deve conter apenas números.")
            .Must(ClienteExistente).WithMessage("O CPF informado não existe no banco de dados.");

            RuleFor(ordemdeservico => ordemdeservico.TecnicoCPF)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Length(11).WithMessage("O CPF deve ter 11 dígitos.")
            .Matches("^[0-9]*$").WithMessage("O CPF deve conter apenas números.")
            .Must(TecnicoExistente).WithMessage("O CPF informado não existe no banco de dados.");

            RuleFor(ordemdeservico => ordemdeservico.SerialEquipamento)
            .NotEmpty().WithMessage("O Serial é obrigatório.")
            .Must(SerialExistente).WithMessage("O Serial informado não existe no banco de dados.");


            RuleFor(ordemdeservico => ordemdeservico.Status)
             .NotEmpty().WithMessage("O status é obrigatório.")
             .Must(status => StatusPermitidos.Contains(status)).WithMessage("O status deve ser um dos seguintes: Concluido, " +
             "Em Andamento, Pendente, Cancelada, Aberta.");

            RuleFor(ordemdeservico => ordemdeservico.MetodoPagamento)
            .NotEmpty().WithMessage("O método de pagamento é obrigatório.")
            .Must(metodo => MetodosPagamentoPermitidos.Contains(metodo)).WithMessage("O método de pagamento deve ser um dos seguintes:" +
            " Cartão de Crédito, Cartão de Débito, Boleto, Transferência, Pix.");

            RuleFor(ordemdeservico => ordemdeservico.ValorTotal)
            .Must(valor => valor == null || (valor >= 0 && valor.GetType() == typeof(decimal))) // Permite nulo ou valor >= 0
            .WithMessage("O valor total deve ser um número positivo ou zero.");

            //RuleFor(ordemdeservico => ordemdeservico.dataAbertura)
            //.NotNull().WithMessage("A data de atualização é obrigatória.")
            //.LessThanOrEqualTo(DateTime.Now).WithMessage("A data de atualização não pode ser futura.");


        }
        private bool ClienteExistente(string clienteCPF)
        {
            return _context.Set<Cliente>().Any(cliente => cliente.CPF == clienteCPF);
        }

        private bool TecnicoExistente(string tecnicoCPF)
        {
            return _context.Set<Tecnico>().Any(tecnico => tecnico.CPF == tecnicoCPF);
        }
        private bool SerialExistente(string serial)
        {
            return _context.Set<Equipamento>().Any(equipamento => equipamento.Serial == serial);
        }



        private static readonly string[] MetodosPagamentoPermitidos =
            {
                "Cartão de Crédito",
                "Cartão de Débito",
                "Boleto",
                "Dinheiro",
                "Pix"
            };
        private static readonly string[] StatusPermitidos =
            {
                "Concluido",
                "Em Andamento",
                "Pendente",
                "Cancelada",
                "Aberta"
             };

    }
}