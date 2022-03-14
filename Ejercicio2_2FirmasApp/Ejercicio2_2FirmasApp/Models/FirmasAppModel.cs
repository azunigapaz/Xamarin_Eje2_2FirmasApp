using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Ejercicio2_2FirmasApp.Models
{
    public class FirmasAppModel
    {
        [PrimaryKey, AutoIncrement]
        public int FirmaId { get; set; }

        [MaxLength(30)]
        public String FirmaNombre { get; set; }

        [MaxLength(160)]
        public String FirmaDescripcion { get; set; }

        public Byte[] FirmaImagen { get; set; }
    }
}
