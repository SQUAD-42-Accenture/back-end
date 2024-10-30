using FluentValidation;
using SERVPRO.Models;

namespace SERVPRO.Validators
{
    public class UsuarioValidator : AbstractValidator<Cliente>
    {

        public UsuarioValidator()
        {
            RuleFor(cliente => cliente.CPF)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Length(11).WithMessage("O CPF deve ter 11 dígitos.")
            .Matches("^[0-9]*$").WithMessage("O CPF deve conter apenas números.");

            RuleFor(cliente => cliente.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .Length(16).WithMessage("A senha deve conter no máximo 16 caracteres.");

            RuleFor(cliente => cliente.TipoUsuario)
            .NotEmpty().WithMessage("O tipo de usuario é obrigatorio")
            .Equal("Cliente").WithMessage("O tipo precisa ser 'Cliente'.");

        }
    }
}