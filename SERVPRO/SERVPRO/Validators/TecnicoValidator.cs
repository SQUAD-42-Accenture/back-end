using FluentValidation;
using SERVPRO.Models;

namespace SERVPRO.Validators
{
    public class TecnicoValidator : AbstractValidator<Tecnico>
    {

        public TecnicoValidator()
        {
            RuleFor(tecnico => tecnico.CPF)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Length(11).WithMessage("O CPF deve ter 11 dígitos.")
            .Matches("^[0-9]*$").WithMessage("O CPF deve conter apenas números.");

            RuleFor(tecnico => tecnico.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MaximumLength(16).WithMessage("A senha deve conter no máximo 16 caracteres.");

           RuleFor(tecnico => tecnico.TipoUsuario)
            .NotEmpty().WithMessage("O tipo de usuario é obrigatorio")
            .Equal("Cliente").WithMessage("O tipo precisa ser 'Tecnico'.");

        }
    }
}