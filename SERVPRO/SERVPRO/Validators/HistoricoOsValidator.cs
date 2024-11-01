using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SERVPRO.Data;
using SERVPRO.Models;

namespace SERVPRO.Validators
{
    public class HistoricoOsValidator : AbstractValidator<HistoricoOS>
    {
        private readonly ServproDBContext _context;

        public HistoricoOsValidator(ServproDBContext context)
        {
            _context = context;

            RuleFor(historicoOS => historicoOS.OrdemDeServicoId)
            .NotEmpty().WithMessage("O ID da OS é obrigatório.")
            .Must(IdOsExistente).WithMessage("Id da OS informado não existe no banco de dados.");

            RuleFor(historicoOS => historicoOS.TecnicoCPF)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Length(11).WithMessage("O CPF deve ter 11 dígitos.")
            .Matches("^[0-9]*$").WithMessage("O CPF deve conter apenas números.")
            .Must(TecnicoExistente).WithMessage("O CPF informado não existe no banco de dados.");

            RuleFor(historicoOS => historicoOS.Comentario)
           .NotEmpty().WithMessage("O comentário é obrigatório.");

            RuleFor(historicoOS => historicoOS.DataAtualizacao)
            .NotNull().WithMessage("A data de atualização é obrigatória.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("A data de atualização não pode ser futura.")
            .When(historicoOS => historicoOS.DataAtualizacao.HasValue);
        }

        private bool TecnicoExistente(string tecnicoCPF)
        {
            return _context.Set<Tecnico>().Any(tecnico => tecnico.CPF == tecnicoCPF);
        }
        private bool IdOsExistente(int? id)
        {
            return _context.Set<OrdemDeServico>().Any(os => os.Id == id);
        }

    }
}