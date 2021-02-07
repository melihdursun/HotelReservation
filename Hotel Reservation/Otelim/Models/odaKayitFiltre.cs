using Otelim.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Otelim.Models
{
    public class odaKayitFiltre
    {
        public Oda oda { get; set; }

        public List<Müşteri> Müşteriler { get; set; }


        public int odaid { get; set; }
        public int musteriid { get; set; }

        public DateTime GirisTarihi { get; set; }
        public DateTime CikisTarihi { get; set; }
    }
}