using MenorPreco.DAL.Estabelecimentos;
using MenorPreco.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MenorPreco.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/")]
    public class ProdutosController : ControllerBase
    {

        public static IWebHostEnvironment _environment;
        private readonly IRepository<Produto> _repoProduto;
        private readonly IRepository<Estabelecimento> _repoEstabelecimento;
        private readonly IRepository<Venda> _repoVenda;
        private readonly IRepository<VendaProduto> _repoVendaProduto;

        public ProdutosController(IWebHostEnvironment environment,
            IRepository<Produto> repository,
            IRepository<Estabelecimento> repoEstabelecimento,//, 
            IRepository<Venda> repoVenda,
            IRepository<VendaProduto> repoVendaProduto)
        {
            _environment = environment;
            _repoProduto = repository;
            _repoEstabelecimento = repoEstabelecimento;
            _repoVenda = repoVenda;
            _repoVendaProduto = repoVendaProduto;
        }



        //GET /api/v1/produtos/
        [HttpGet("produtos")]
        public IActionResult Recuperar()
        {
            return BadRequest(new { message = $"Consulte GTIN pela seguinte rota: GET /v1/produtos/1234567891011" });
        }

        //GET /api/v1/produtos/{GTIN}
        [HttpGet("produtos/{GTIN}")]
        public IActionResult Recuperar(string GTIN)
        {
            var model = _repoProduto.InclueEstabelecimento.ToList()
               .Where(x => x.COD_GTIN.ToString() == GTIN.ToString())
               .Select(p => p.ToApi())
               .OrderBy(p => p.vlr_unitario);

            if (model.Count() == 0)
            {
                return NotFound(new { message = $"Produto com GTIN={GTIN} não encontrado" });
            }

            return Ok(model);
        }



        [HttpPost("importar")]
        public async Task<string> RecebeArquivo([FromForm] IFormFile arquivo)
        {
            if (arquivo.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_environment + "\\Arquivos\\"))
                    {
                        Directory.CreateDirectory(_environment + "\\Arquivos\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment + "\\Arquivos\\" + arquivo.FileName))
                    {
                        await arquivo.CopyToAsync(filestream);
                        filestream.Flush();
                    }

                    var enderecoDoArquivo = _environment + "\\Arquivos\\" + arquivo.FileName;


                    using (var fluxoDeArquivo = new FileStream(enderecoDoArquivo, FileMode.Open))
                    using (var leitor = new StreamReader(fluxoDeArquivo))
                    {
                        int contador = 0;
                        while (!leitor.EndOfStream)
                        {
                            var linha = leitor.ReadLine();
                            contador++;
                            if (contador == 1) { continue; }

                            var sefaz = ConverterStringParaSefaz(linha);
                            if (sefaz == null)
                            {
                                continue;
                            }

                            var estabelecimento = new Estabelecimento();
                            var produto = new Produto();
                            var venda = new Venda();
                            var vendaProduto = new VendaProduto();

                            var estabelecimento_ = _repoEstabelecimento.ProcurarPeloEstabelecimento(int.Parse(sefaz.ID_ESTABELECIMENTO));

                            if (estabelecimento_ == null)
                            {
                                estabelecimento.ID_ESTABELECIMENTO = int.Parse(sefaz.ID_ESTABELECIMENTO);
                                estabelecimento.NME_ESTABELECIMENTO = sefaz.NME_ESTABELECIMENTO;
                                estabelecimento.NME_LOGRADOURO = sefaz.NME_LOGRADOURO;
                                estabelecimento.COD_NUMERO_LOGRADOURO = int.Parse(sefaz.COD_NUMERO_LOGRADOURO);
                                estabelecimento.NME_COMPLEMENTO = sefaz.NME_COMPLEMENTO;
                                estabelecimento.NME_BAIRRO = sefaz.NME_BAIRRO;
                                estabelecimento.COD_MUNICIPIO_IBGE = int.Parse(sefaz.COD_MUNICIPIO_IBGE);
                                estabelecimento.NME_MUNICIPIO = sefaz.NME_MUNICIPIO;
                                estabelecimento.NME_SIGLA_UF = sefaz.NME_SIGLA_UF;
                                estabelecimento.COD_CEP = sefaz.COD_CEP;
                                estabelecimento.NUM_LATITUDE = int.Parse(sefaz.NUM_LATITUDE.Replace(".", ""));
                                estabelecimento.NUM_LONGITUDE = int.Parse(sefaz.NUM_LONGITUDE.Replace(".", ""));

                                _repoEstabelecimento.IncluirEstabelecimento(estabelecimento);
                                _repoProduto.SalvaProduto();
                            }

                            var produto_ = _repoProduto.ProcurarPorProduto(long.Parse(sefaz.COD_PRODUTO));
                            if (produto_ == null)
                            {
                                produto.COD_GTIN = long.Parse(sefaz.COD_GTIN);
                                produto.COD_NCM = int.Parse(sefaz.COD_NCM);
                                produto.COD_PRODUTO = long.Parse(sefaz.COD_PRODUTO);
                                produto.COD_UNIDADE = sefaz.COD_UNIDADE;
                                produto.DSC_PRODUTO = sefaz.DSC_PRODUTO;
                                produto.ID_ESTABELECIMENTO = int.Parse(sefaz.ID_ESTABELECIMENTO);
                                produto.VLR_UNITARIO = decimal.Parse(sefaz.VLR_UNITARIO);

                                _repoProduto.InclueProduto(produto);
                                _repoProduto.SalvaProduto();
                            }

                            venda.DAT_EMISSAO = DateTime.Parse(sefaz.DAT_EMISSAO);
                            venda.COD_TIPO_PAGAMENTO = sefaz.COD_TIPO_PAGAMENTO;
                            venda.ID_ESTABELECIMENTO = int.Parse(sefaz.ID_ESTABELECIMENTO);

                            _repoVenda.InclueVenda(venda);
                            _repoProduto.SalvaProduto();

                            vendaProduto.COD_PRODUTO = produto.COD_PRODUTO;
                            vendaProduto.COD_PRODUTO = long.Parse(sefaz.COD_PRODUTO);
                            int qtd_vendas = _repoVenda.All.Count();
                            vendaProduto.ID_VENDA = qtd_vendas;

                            _repoVendaProduto.InclueVendaProduto(vendaProduto);
                            _repoProduto.SalvaProduto();


                        }
                    }
                    return "\\Arquivos\\" + arquivo.FileName;

                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            else
            {
                return "Ocorreu uma falha no envio do arquivo...";
            }


            static Sefaz ConverterStringParaSefaz(string linha)
            {
                string[] campos = linha.Split(';');

                var COD_GTIN = campos[0];
                var DAT_EMISSAO = campos[1];
                var COD_TIPO_PAGAMENTO = campos[2];
                var COD_PRODUTO = campos[3];
                var COD_NCM = campos[4];
                var COD_UNIDADE = campos[5];
                var DSC_PRODUTO = campos[6];
                var VLR_UNITARIO = campos[7].Replace('.', ',');
                var ID_ESTABELECIMENTO = campos[8];
                var NME_ESTABELECIMENTO = campos[9];
                var NME_LOGRADOURO = campos[10];
                var COD_NUMERO_LOGRADOURO = campos[11];
                var NME_COMPLEMENTO = campos[12];
                var NME_BAIRRO = campos[13];
                var COD_MUNICIPIO_IBGE = campos[14];
                var NME_MUNICIPIO = campos[15];
                var NME_SIGLA_UF = campos[16];
                var COD_CEP = campos[17];
                var NUM_LATITUDE = campos[18];
                var NUM_LONGITUDE = campos[19];


                if (ID_ESTABELECIMENTO == "" ||
                                NME_ESTABELECIMENTO == "" ||
                                COD_NUMERO_LOGRADOURO == "" ||
                                NME_LOGRADOURO == "" ||
                                NME_BAIRRO == "" ||
                                COD_MUNICIPIO_IBGE == "" ||
                                NME_MUNICIPIO == "" ||
                                COD_CEP == "" ||
                                NME_SIGLA_UF == "" ||
                                NUM_LATITUDE == "" ||
                                NUM_LONGITUDE == "")
                {
                    Console.WriteLine($"Produto não registrado corretamente: {COD_PRODUTO}");
                    return null;
                }

                if (COD_GTIN == "" ||
                                COD_NCM == "" ||
                                COD_PRODUTO == "" ||
                                COD_UNIDADE == "" ||
                                DSC_PRODUTO == "" ||
                                ID_ESTABELECIMENTO == "" ||
                                VLR_UNITARIO == "")
                {
                    Console.WriteLine($"Estabelecimento não registrado: {COD_GTIN}");
                    return null;
                }

                var sefaz = new Sefaz();

                sefaz.COD_GTIN = COD_GTIN;
                sefaz.DAT_EMISSAO = DAT_EMISSAO;
                sefaz.COD_TIPO_PAGAMENTO = COD_TIPO_PAGAMENTO;
                sefaz.COD_PRODUTO = COD_PRODUTO;
                sefaz.COD_NCM = COD_NCM;
                sefaz.COD_UNIDADE = COD_UNIDADE;
                sefaz.DSC_PRODUTO = DSC_PRODUTO;
                sefaz.VLR_UNITARIO = VLR_UNITARIO;
                sefaz.ID_ESTABELECIMENTO = ID_ESTABELECIMENTO;
                sefaz.NME_ESTABELECIMENTO = NME_ESTABELECIMENTO;
                sefaz.NME_LOGRADOURO = NME_LOGRADOURO;
                sefaz.COD_NUMERO_LOGRADOURO = COD_NUMERO_LOGRADOURO;
                sefaz.NME_COMPLEMENTO = NME_COMPLEMENTO;
                sefaz.NME_BAIRRO = NME_BAIRRO;
                sefaz.COD_MUNICIPIO_IBGE = COD_MUNICIPIO_IBGE;
                sefaz.NME_MUNICIPIO = NME_MUNICIPIO;
                sefaz.NME_SIGLA_UF = NME_SIGLA_UF;
                sefaz.COD_CEP = COD_CEP;
                sefaz.NUM_LATITUDE = NUM_LATITUDE;
                sefaz.NUM_LONGITUDE = NUM_LONGITUDE;

                return sefaz;
            }
        }
    }
}


