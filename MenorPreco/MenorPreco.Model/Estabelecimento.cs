using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenorPreco.Model
{
    public class Estabelecimento
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_ESTABELECIMENTO { get; set; }
        public string NME_ESTABELECIMENTO { get; set; }
        public string NME_LOGRADOURO { get; set; }
        public int COD_NUMERO_LOGRADOURO { get; set; }
        public string NME_COMPLEMENTO { get; set; }
        public string NME_BAIRRO { get; set; }
        public int COD_MUNICIPIO_IBGE { get; set; }
        public string NME_MUNICIPIO { get; set; }
        public string NME_SIGLA_UF { get; set; }
        public string COD_CEP { get; set; }
        public int NUM_LATITUDE { get; set; }
        public int NUM_LONGITUDE { get; set; }
        public List<Produto> PRODUTOS { get; set; }
        public List<Venda> VENDAS { get; set; }
    }
}
