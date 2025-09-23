using System;

namespace TrabajoTarjeta
{
    public class Colectivo
    {
        private const decimal TARIFA_BASICA = 1580;
        private readonly string linea;

        public Colectivo(string linea)
        {
            this.linea = linea ?? throw new ArgumentNullException(nameof(linea));
        }

        public string ObtenerLinea()
        {
            return linea;
        }

        public decimal ObtenerTarifaBasica()
        {
            return TARIFA_BASICA;
        }

        // Método original que lanza excepción (mantener compatibilidad)
        public Boleto PagarCon(Tarjeta tarjeta)
        {
            if (tarjeta == null)
            {
                throw new ArgumentNullException(nameof(tarjeta));
            }

            decimal tarifa = tarjeta.CalcularTarifa(TARIFA_BASICA);

            if (!tarjeta.PuedeDescontar(tarifa))
            {
                throw new InvalidOperationException("La tarjeta no tiene saldo suficiente para pagar el pasaje.");
            }

            tarjeta.Descontar(tarifa);
            return new Boleto(
                fechaHora: DateTime.Now,
                tarifa: tarifa,
                saldoRestante: tarjeta.ObtenerSaldo(),
                linea: linea
            );
        }

        // Nuevo método que retorna false en lugar de excepción
        public bool TryPagarCon(Tarjeta tarjeta, out Boleto boleto)
        {
            boleto = null;

            if (tarjeta == null)
            {
                return false;
            }

            decimal tarifa = tarjeta.CalcularTarifa(TARIFA_BASICA);

            if (!tarjeta.PuedeDescontar(tarifa))
            {
                return false;
            }

            tarjeta.Descontar(tarifa);
            boleto = new Boleto(
                fechaHora: DateTime.Now,
                tarifa: tarifa,
                saldoRestante: tarjeta.ObtenerSaldo(),
                linea: linea
            );

            return true;
        }
    }
}