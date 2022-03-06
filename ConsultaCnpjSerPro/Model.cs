using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaCnpjSerPro
{
    public class Model
    {
        //verificar api para mais detalhes
        public class DataSerproApi
        {
            public string ni;
            public string tipoEstabelecimento;
            public string nomeEmpresarial;
            public string nomeFantasia;
            public string dataAbertura;
            public Socios[] socios;
        }
        public class Socios
        {
            public string tipoSocio;
            public string cpf;
            public string cnpj;
            public string nome;
            public string qualificacao;
            public string dataInclusao;
            public Pais pais;
            public RepresentanteLegal representanteLegal;
        }
        public class Pais
        {
            public string codigo;
            public string descricao;
        }
        public class RepresentanteLegal
        {
            public string cpf;
            public string nome;
            public string qualificacao;
        }
    }
}
