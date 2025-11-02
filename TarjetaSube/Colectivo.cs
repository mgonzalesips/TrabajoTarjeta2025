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

        public bool PagarCon(Tarjeta tarjeta)
        {
            if (tarjeta == null)
            {
                throw new ArgumentNullException(nameof(tarjeta));
            }

            bool pagoExitoso = tarjeta.Descontar(TARIFA_BASICA);

            if (!pagoExitoso)
            {
                return false;
            }

            Boleto boleto = new Boleto(
                tipoTarjeta: tarjeta.GetType().Name,
                lineaColectivo: Linea,
                totalAbonado: TARIFA_BASICA,
                saldoRestante: tarjeta.ObtenerSaldo()
            );

            return true;
        }
    }

}