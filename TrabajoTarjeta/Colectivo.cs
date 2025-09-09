using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Boleto PagarCon(Tarjeta tarjeta)
        {
            if (tarjeta == null)
            {
                throw new ArgumentNullException(nameof(tarjeta));
            }

            if (!tarjeta.PuedeDescontar(TARIFA_BASICA))
            {
                throw new InvalidOperationException("La tarjeta no tiene saldo suficiente para pagar el pasaje.");
            }

            tarjeta.Descontar(TARIFA_BASICA);

            return new Boleto(
                fechaHora: DateTime.Now,
                tarifa: TARIFA_BASICA,
                saldoRestante: tarjeta.ObtenerSaldo(),
                linea: linea
            );
        }
    }
}
