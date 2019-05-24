using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aracKiralamaWebUI.Models
{
    public class kiralamaModel
    {
        public int sirketId { get; set; }
        public int musteriId { get; set; }
        public int aracId { get; set; }
        public DateTime verilisTarihi { get; set; }
        public DateTime? geriAlisTarihi { get; set; }
        public int verilisKm { get; set; }
        public int? sonKm { get; set; }
        public int? ucret { get; set; }
    }
}