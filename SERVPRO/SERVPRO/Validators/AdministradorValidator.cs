using FluentValidation;
using SERVPRO.Models;

namespace SERVPRO.Validators
{
    public class AdministradorValidator : AbstractValidator<Administrador>
    {

        public AdministradorValidator()
        {
            RuleFor(administrador => administrador.CPF)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Length(11).WithMessage("O CPF deve ter 11 dígitos.")
            .Matches("^[0-9]*$").WithMessage("O CPF deve conter apenas números.");

            RuleFor(administrador => administrador.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MaximumLength(16).WithMessage("A senha deve conter no máximo 16 caracteres.");

            RuleFor(administrador => administrador.TipoUsuario)
            .NotEmpty().WithMessage("O tipo de usuario é obrigatorio")
            .Equal("Administrador").WithMessage("O tipo precisa ser 'Administrador'.");

        }
    }
}