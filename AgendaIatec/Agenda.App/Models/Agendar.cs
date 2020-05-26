using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.App.Models
{
    public class Agendar : Entity
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DtEntrada { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Evento")]
        [ForeignKey("Evento")]
        public Guid EventoId { get; set; }
        [DisplayName("Usuario")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }

        /*EF Relations */

        public Usuario Usuario { get; set; }
        public Evento Evento { get; set; }
    }
}
