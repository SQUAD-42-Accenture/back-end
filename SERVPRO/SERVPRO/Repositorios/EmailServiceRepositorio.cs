namespace SERVPRO.Repositorios;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailServiceRepositorio
{
    private readonly string _apiKey;
    private readonly string _senderEmail;
    private readonly string _senderName;

    public EmailServiceRepositorio(IConfiguration configuration)
    {
        _apiKey = "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";
        _senderEmail = "vinicius.jdsilva98@gmail.com";
        _senderName = "Servpro";
    }

    public async Task EnviarOrdemDeServicoPorEmail(string destinatarioEmail, string assunto, string conteudo, byte[] pdfBytes)
    {
        var client = new SendGridClient(_apiKey);
        var from = new EmailAddress(_senderEmail, _senderName);
        var to = new EmailAddress(destinatarioEmail);
        var msg = MailHelper.CreateSingleEmail(from, to, assunto, "", conteudo);

        if (pdfBytes != null)
        {
            var fileBase64 = Convert.ToBase64String(pdfBytes);
            msg.AddAttachment("ordem_servico.pdf", fileBase64);
        }

        var response = await client.SendEmailAsync(msg);
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            // Log ou tratamento de erro, se necessário
        }
    }
}
