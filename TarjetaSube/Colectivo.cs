using System;

namespace TarjetaSube
{
    public class Colectivo
    {
        private const int TARIFA_BASICA = 1580;
        public string Linea { get; private set; }

        public Colectivo(string linea)
        {
            Linea = linea;
        }

        public Boleto PagarCon(Tarjeta tarjeta)
        {
            if (tarjeta == null)
            {
                throw new ArgumentNullException(nameof(tarjeta));
            }

            bool pagoExitoso = tarjeta.Descontar(TARIFA_BASICA);
            if (!pagoExitoso)
            {
                return null; 
            }

            int saldoDespues = tarjeta.ObtenerSaldo();

            Boleto boleto = new Boleto(
                tipoTarjeta: "Normal",
                lineaColectivo: Linea,
                totalAbonado: TARIFA_BASICA,
                saldoRestante: saldoDespues
            );

            return boleto;
        }
    }
}
