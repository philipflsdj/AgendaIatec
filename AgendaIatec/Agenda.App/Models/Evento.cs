using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.App.Models
{
    public class Evento : Entity
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }
        [DisplayName("Descrição")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(5000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }
        [DisplayName("Data Inicial")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DtInicial { get; set; }
        [DisplayName("Data Final")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DtFinal { get; set; }
        [DisplayName("Status do Evento")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string StatusEvento { get; set; }
        [DisplayName("Local")]
        public Guid EnderecoId { get; set; }
        [DisplayName("Tipo do Evento")]
        public Guid TipoEventoId { get; set; }


        /*EF Relations*/
        public Endereco Endereco { get; set; }
        public TipoEvento TipoEvento { get; set; }
        public IEnumerable<Agendar> Agenda { get; set; }
    }
}
