using System;

namespace TrabajoTarjeta
{
    public class Boleto
    {
        private readonly DateTime fechaHora;
        private readonly decimal tarifa;
        private readonly decimal saldoRestante;
        private readonly string linea;

        public Boleto(DateTime fechaHora, decimal tarifa, decimal saldoRestante, string linea)
        {
            this.fechaHora = fechaHora;
            this.tarifa = tarifa;
            this.saldoRestante = saldoRestante;
            this.linea = linea ?? throw new ArgumentNullException(nameof(linea));
        }

        public DateTime ObtenerFechaHora()
        {
            return fechaHora;
        }

        public decimal ObtenerTarifa()
        {
            return tarifa;
        }

        public decimal ObtenerSaldoRestante()
        {
            return saldoRestante;
        }

        public string ObtenerLinea()
        {
            return linea;
        }

        public override string ToString()
        {
            return $"Boleto - Línea: {linea}, Fecha: {fechaHora:dd/MM/yyyy HH:mm:ss}, Tarifa: ${tarifa}, Saldo restante: ${saldoRestante}";
        }
    }
}
