using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Otelim.Models
{
    public class OdaBulFiltre
    {

        [Range(1, 10, ErrorMessage = "1 ile 10 arasında sayı olmak zorunda")]
        public int Tipi { get; set; }
        public DateTime GirisTarihi { get; set; }
        public DateTime CikisTarihi { get; set; }
        public string OrderBy { get; set; }
    }
}