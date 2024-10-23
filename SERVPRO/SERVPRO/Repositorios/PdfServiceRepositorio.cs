using iText.Html2pdf;
using SERVPRO.Models;
using System.IO;

namespace SERVPRO.Repositorios
{
    public class PdfServiceRepositorio
    {
        public byte[] GeneratePdf(OrdemDeServico ordemDeServico, Cliente cliente, Tecnico tecnico)
        {
            using (var memoryStream = new MemoryStream())
            {
                string htmlContent = $@"
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset='utf-8'>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    margin: 0;
                    padding: 0;
                }}
                header {{
                    background-color: #054D9D;
                    color: white;
                    padding: 10px;
                    display: grid;
                    grid-template-columns: 1fr 1fr;
                    align-items: center;
                    gap: 20px;
                }}
                header img {{
                    border-top:18px
                    padding-top: 15px;
                    max-width: 320px;
                    max-height: 100px;
                    height: auto;
                    width: auto;
                    object-fit: contain;
                }}
                header div {{
                    display: flex;
                    flex-direction: column;
                    justify-content: center;
                    text-align: right;
                    font-size: 15px;
                    row-gap: 5px; /* Adiciona espaço entre os parágrafos */
                }}
                h1 {{
                    text-align: center;
                    color: #054D9D;
                    margin-top: 10px;
                    font-size: 20px;
                }}
                section {{
                    margin: 15px;
                }}
                table {{
                    width: 100%;
                    border-collapse: collapse;
                    margin-bottom: 10px;
                }}
                th, td {{
                    border: 1px solid #ddd;
                    padding: 8px;
                    font-size: 15px;
                }}
                th {{
                    background-color: #054D9D;
                    color: white;
                }}
                .total {{
                    text-align: right;
                    font-size: 15px;
                    font-weight: bold;
                    padding: 5px;
                }}
                .observacoes, .consideracoes {{
                    background-color: #054D9D;
                    color: white;
                    padding: 5px;
                    font-size: 12px;
                    margin-bottom: 10px;
                }}
                .observacoes h3, .consideracoes h3 {{
                    margin-bottom: 5px;
                    font-size: 10px;
                }}
                p {{
                    margin: 5px 0;
                }}
            </style>
        </head>
        <body>
            <header>
                <img src='./LogoServPro.png' alt='Logo da Empresa' />
                <div>
                    <p>{cliente.Endereco}</p>
                    <p>Contato: {cliente.Telefone}</p>
                    <p>Responsável técnico: {tecnico.Nome}</p>
                </div>
            </header>

            <h1>ORDEM DE SERVIÇO</h1>

            <section>
                <h3>Dados Pessoais</h3>
                <table>
                    <tr>
                        <td><strong>Cliente:</strong></td>
                        <td>{cliente.Nome}</td>
                    </tr>
                    <tr>
                        <td><strong>E-mail:</strong></td>
                        <td>{cliente.Email}</td>
                    </tr>
                    <tr>
                        <td><strong>Telefone:</strong></td>
                        <td>{cliente.Telefone}</td>
                    </tr>
                    <tr>
                        <td><strong>Endereço:</strong></td>
                        <td>{cliente.Endereco}</td>
                    </tr>
                    <tr>
                        <td><strong>Cidade:</strong></td>
                        <td>add Cidade - add Estado</td>
                    </tr>
                    <tr>
                        <td><strong>Forma de Pagamento:</strong></td>
                        <td>add FormaDePagamento</td>
                    </tr>
                </table>
            </section>

            <section>
                <h3>Orçamento</h3>
                <table>
                    <tr>
                        <th>Item</th>
                        <th>Descrição</th>
                        <th>Quantidade</th>
                        <th>Valor Unitário</th>
                    </tr>
                    <tr>
                        <td>1</td>
                        <td>Computador Dell</td>
                        <td>1</td>
                        <td>R$ 300,00</td>
                    </tr>
                    <tr>
                        <td>2</td>
                        <td>Celular iPhone 14</td>
                        <td>1</td>
                        <td>R$ 500,00</td>
                    </tr>
                </table>
                <div class='total'>
                    <p>Total: R$ 800,00</p>
                </div>
            </section>

            <section class='observacoes'>
                <h3>Observações</h3>
                <p>DISCO RÍGIDO PODE TER SETORES DEFEITUOSOS OU ARQUIVOS DE SISTEMA CORROMPIDOS, O QUE IMPEDE O COMPUTADOR DE CARREGAR O SISTEMA OPERACIONAL CORRETAMENTE.</p>
            </section>

            <section class='consideracoes'>
                <h3>Considerações Finais</h3>
                <p>REALIZAMOS UMA VERIFICAÇÃO DO DISCO PARA IDENTIFICAR SETORES DEFEITUOSOS, EXECUTAMOS O CHKDSK PARA REPARAR ARQUIVOS CORROMPIDOS E CRIAMOS CÓPIAS DE SEGURANÇA DOS DADOS IMPORTANTES. EM CASO DE FALHA SEVERA, CONSIDERAMOS A SUBSTITUIÇÃO DO DISCO RÍGIDO, GARANTINDO A FUNCIONALIDADE DO SISTEMA E A PROTEÇÃO DAS INFORMAÇÕES.</p>
            </section>
        </body>
        </html>";

                HtmlConverter.ConvertToPdf(htmlContent, memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
