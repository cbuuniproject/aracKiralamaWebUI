using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aracKiralamaWebUI.Models
{
    public class harcamaModel
    {
        public harcamaModel()
        {
            aciklama = "";
        }
        public int harcamaId { get; set; }
        public int harcamaTuruId { get; set; }
        public string harcamaTuru { get; set; }
        public string aciklama { get; set; }
        public DateTime tarih { get; set; }
        public int ucret { get; set; }
    }
}