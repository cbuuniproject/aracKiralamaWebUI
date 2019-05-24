using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aracKiralamaWebUI.Models
{
    public class aracModel
    {
        public aracModel()
        {

        }
        public int Id { get; set; }
        public int sirketId { get; set; }
        public string marka { get; set; }
        public string model { get; set; }
        public int minEhliyetYasi { get; set; }
        public short minYasSiniri { get; set; }
        public short gunlukMaxKmSiniri { get; set; }
        public int anlikKm { get; set; }
        public byte airbagSayisi { get; set; }
        public short bagajHacmi { get; set; }
        public byte koltukSayisi { get; set; }
        public int gunlukFiyat { get; set; }
    }
}