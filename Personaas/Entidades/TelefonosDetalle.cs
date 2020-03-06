using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Personaas.Entidades
{
    public class TelefonosDetalle
    {
        [Key]
        public int Id { get; set; }
        public string TipoTelefono { get; set; }
        public string Telefono { get; set; }

        public TelefonosDetalle()
        {
            Id = 0;
            TipoTelefono = string.Empty;
            Telefono = string.Empty;
        }
        public TelefonosDetalle(string telefono, string tipoTelefono)
        {
            Id = 0;
            Telefono = telefono;
            TipoTelefono = tipoTelefono;
        }
    }
}
