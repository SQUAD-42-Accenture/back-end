using FluentValidation;
using SERVPRO.Models;

namespace SERVPRO.Validators
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {

        public ClienteValidator()
        {
            RuleFor(cliente => cliente.CPF)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Length(11).WithMessage("O CPF deve ter 11 dígitos.")
            .Matches("^[0-9]*$").WithMessage("O CPF deve conter apenas números.");

            RuleFor(cliente => cliente.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório");


            RuleFor(cliente => cliente.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O e-mail informado não é válido.");

            RuleFor(cliente => cliente.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MaximumLength(16).WithMessage("A senha deve conter no máximo 16 caracteres.");

            RuleFor(cliente => cliente.TipoUsuario)
            .NotEmpty().WithMessage("O tipo de usuario é obrigatorio")
            .Equal("Cliente").WithMessage("O tipo precisa ser 'Cliente'.");

            RuleFor(cliente => cliente.DataNascimento)
            .NotEmpty().WithMessage("A data de nascimento é obrigatoria")
            .Must(dataNascimento => dataNascimento <= DateTime.Now.AddYears(-16))
            .WithMessage("O Cliente deve ter pelo menos 16 anos.");

            RuleFor(cliente => cliente.Telefone)
            .NotEmpty().WithMessage("O telefone é obrigatório.")
            .Matches(@"^\d{10,11}$").WithMessage("O telefone deve conter apenas números e ter 10 ou 11 dígitos.");

            RuleFor(cliente => cliente.CEP)
            .NotEmpty().WithMessage("O CEP é obrigatorio")
            .Length(8).WithMessage("O CEP deve ter 8 dígitos.")
            .Matches("^[0-9]*$").WithMessage("O CEP deve conter apenas números.");

            RuleFor(cliente => cliente.Bairro)
            .NotEmpty().WithMessage("O Bairro é obrigatorio");

            RuleFor(cliente => cliente.Cidade)
            .NotEmpty().WithMessage("A cidade é obrigatoria");


        }
        
    }
}