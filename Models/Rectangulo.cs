using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DibujoTCP.Models
{
    public class Rectangulo
    {
        public string? Nombre { get; set; }
        public int Alto { get; set; } = 50;
        public int Ancho { get; set; } = 50;
        public int CoordenadaY { get; set; } = 10;
        public int CoordenadaX { get; set; } = 10;
        public string color { get; set; } = "#808080";

    }
}
