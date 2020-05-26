using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.App.Models
{
    public class TipoEvento : Entity
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(90, ErrorMessage = "O campo {0} não pode ter mais de 90 caracteres ")]
        public string Nome { get; set; }

        /* EF Relations */
        public IEnumerable<Evento> Evento { get; set; }
    }
}
