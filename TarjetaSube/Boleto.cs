using System;

namespace TarjetaSube
{
    public class Boleto
    {
        public DateTime FechaHora { get; set; }
        public int LineaColectivo { get; set; }
        public int Monto { get; set; }

        public Boleto(DateTime fechaHora, int lineaColectivo, int monto)
        {
            FechaHora = fechaHora;
            LineaColectivo = lineaColectivo;
            Monto = monto;
        }
    }
}