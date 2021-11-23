using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlingIntegration.Model
{
    public class NF
    {
        public string serie { get; set; }
        public string numero { get; set; }
        public string dataEmissao { get; set; }
        public string situacao { get; set; }
        public string valorNota { get; set; }
        public string chaveAcesso { get; set; }
    }
}
