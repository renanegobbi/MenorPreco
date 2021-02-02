using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenorPreco.Model
{
    public class Venda
    {
        public int ID_VENDA { get; set; }
        public DateTime DAT_EMISSAO { get; set; }
        public string COD_TIPO_PAGAMENTO { get; set; }
        public Estabelecimento ESTABELECIMENTO { get; set; }
        public List<VendaProduto> VENDAPRODUTO { get; set; }

        [NotMapped]
        public int ID_ESTABELECIMENTO { get; set; }
    }


    public class VendaApi
    {
        public int ID_VENDA { get; set; }
        public DateTime DAT_EMISSAO { get; set; }
        public string COD_TIPO_PAGAMENTO { get; set; }
        public int ID_ESTABELECIMENTO { get; set; }
    }
}

