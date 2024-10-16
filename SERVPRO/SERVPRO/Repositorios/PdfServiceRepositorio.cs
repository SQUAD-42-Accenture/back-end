using iText.Html2pdf;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using SERVPRO.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using iText.Kernel.Geom;


namespace SERVPRO.Repositorios
{
    public class PdfServiceRepositorio
    {
        public byte[] GeneratePdf(OrdemDeServico ordemDeServico)
        {
            using (var memoryStream = new MemoryStream())
            {
                // HTML com conteúdo e estilos
                string htmlContent = $@"
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset='utf-8'>
            <style>
                * {{
                    margin: 0;
                    padding: 0;
                    box-sizing: border-box;
                }}
                html, body {{
                    height: 100%;
                    width: 100%;
                    margin: 0;
                    padding: 0;
                    font-family: Arial, sans-serif;
                }}
                body {{
                    background-color: rgb(19, 76, 150);
                }}
                header {{
                    text-align: right;
                    margin: 20px;
                }}
                img {{
                    width: 200px;
                }}
                .content {{
                    background-color: #134C96;
                    color: aliceblue;
                    padding: 40px;
                    height: calc(100% - 80px);
                    display: flex;
                    flex-direction: column;
                    justify-content: center;
                }}
                h1 {{
                    text-align: center;
                    font-size: 24px;
                    margin-bottom: 20px;
                }}
                .info {{
                    font-size: 18px;
                    margin-bottom: 10px;
                }}
                .info p {{
                    margin: 5px 0;
                }}
            </style>
        </head>
        <body>
            <header>
                <img src='./LogoServPro.png' alt='Logo' class='logo' />
            </header>
            <div class='content'>
                <h1>ORDEM DE SERVIÇO</h1>
                <div class='info'>
                    <p><strong>Data de Abertura:</strong> {ordemDeServico.dataAbertura}</p>
                </div>
                <div class='info'>
                    <p><strong>Status:</strong> {ordemDeServico.Status}</p>
                </div>
                <div class='info'>
                    <p><strong>Descrição:</strong> {ordemDeServico.Descricao}</p>
                </div>
                <div class='info'>
                    <p><strong>Cliente:</strong> {ordemDeServico.ClienteCPF}</p>
                </div>
                <div class='info'>
                    <p><strong>Técnico:</strong> {ordemDeServico.TecnicoCPF}</p>
                </div>
                <div class='info'>
                    <p><strong>Equipamento Serial:</strong> {ordemDeServico.SerialEquipamento}</p>
                </div>
            </div>
        </body>
        </html>";

                var pdfWriter = new PdfWriter(memoryStream);
                var pdfDocument = new PdfDocument(pdfWriter);

                var document = new Document(pdfDocument, PageSize.A4);
                document.SetMargins(0, 0, 0, 0);


                // Gera o PDF a partir do HTML e escreve no MemoryStream
                HtmlConverter.ConvertToPdf(htmlContent, memoryStream);

                // Retorna o PDF gerado como um array de bytes
                return memoryStream.ToArray();
            }
        }
    }
}
