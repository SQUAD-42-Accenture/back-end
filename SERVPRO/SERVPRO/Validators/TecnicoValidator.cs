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

            RuleFor(tecnico => tecnico.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório");


            RuleFor(tecnico => tecnico.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O e-mail informado não é válido.");


            RuleFor(tecnico => tecnico.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MaximumLength(16).WithMessage("A senha deve conter no máximo 16 caracteres.");

           RuleFor(tecnico => tecnico.TipoUsuario)
            .NotEmpty().WithMessage("O tipo de usuario é obrigatorio")
            .Equal("Tecnico").WithMessage("O tipo precisa ser 'Tecnico'.");

            RuleFor(tecnico => tecnico.Especialidade)
            .NotEmpty().WithMessage("A especialidade é obrigatório");

            RuleFor(tecnico => tecnico.Telefone)
           .NotEmpty().WithMessage("O telefone é obrigatório.")
           .Matches(@"^\d{10,11}$").WithMessage("O telefone deve conter apenas números e ter 10 ou 11 dígitos.");




        }
    }
}