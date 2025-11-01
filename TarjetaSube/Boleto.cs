using System;

namespace TarjetaSube
{
    public class Boleto
    {
        public string TipoTarjeta { get; private set; }
        public string LineaColectivo { get; private set; }
        public decimal TotalAbonado { get; private set; }
        public decimal SaldoRestante { get; private set; }
        public DateTime Fecha { get; private set; }

        public Boleto(string tipoTarjeta, string lineaColectivo, decimal totalAbonado, decimal saldoRestante)
        {
            TipoTarjeta = tipoTarjeta;
            LineaColectivo = lineaColectivo;
            TotalAbonado = totalAbonado;
            SaldoRestante = saldoRestante;
            Fecha = DateTime.Now;
        }
    }
}
