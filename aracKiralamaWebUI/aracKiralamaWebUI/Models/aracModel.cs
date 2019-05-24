using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aracKiralamaWebUI.Models
{
    public class aracModel
    {
        public int aracId { get; set; }
        public int sirketId { get; set; }
        public string marka { get; set; }
        public string model { get; set; }
        public int minEhliyetYasi { get; set; }
        public int minYasSiniri { get; set; }
        public int gunlukMaxKm { get; set; }
        public int airbagSayisi { get; set; }
        public int anlikKm { get; set; }
        public int bagajHacmi { get; set; }
        public int koltukSayisi { get; set; }
        public decimal gunlukFiyat { get; set; }
    }
}