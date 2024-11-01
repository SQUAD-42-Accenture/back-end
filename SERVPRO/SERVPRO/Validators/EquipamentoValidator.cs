using FluentValidation;
using SERVPRO.Data;
using SERVPRO.Models;

namespace SERVPRO.Validators
{
    public class EquipamentoValidator : AbstractValidator<Equipamento>
    {
        private readonly ServproDBContext _context;
        public EquipamentoValidator(ServproDBContext context)
        {
            _context = context;

            RuleFor(equipamento => equipamento.Serial)
            .NotEmpty().WithMessage("O Serial é obrigatório.");

            RuleFor(equipamento => equipamento.Marca)
            .NotEmpty().WithMessage("A Marca é obrigatório.");

            RuleFor(equipamento => equipamento.Modelo)
            .NotEmpty().WithMessage("O Modelo é obrigatório.");

            RuleFor(equipamento => equipamento.Descricao)
            .NotEmpty().WithMessage("A descrição do problema é obrigatória.");

            RuleFor(equipamento => equipamento.Serial)
            .NotEmpty().WithMessage("A data é obrigatória.");

            RuleFor(ordemdeservico => ordemdeservico.ClienteCPF)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Length(11).WithMessage("O CPF deve ter 11 dígitos.")
            .Matches("^[0-9]*$").WithMessage("O CPF deve conter apenas números.")
            .Must(ClienteExistente).WithMessage("O CPF informado não existe no banco de dados.");

            /*RuleFor(ordemdeservico => ordemdeservico.DataCadastro)
            .NotNull().WithMessage("A data de atualização é obrigatória.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("A data de atualização não pode ser futura.");
            */

        }

        private bool ClienteExistente(string clienteCPF)
        {
            return _context.Set<Cliente>().Any(cliente => cliente.CPF == clienteCPF);
        }
    }
}